using System;
using System.Collections.Generic;
using System.Linq;
using ApplitoolsHackathonVisionAi.businessobjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApplitoolsHackathonVisionAi.pageobjects
{
    /// <summary>
    /// Object representing the applications homepage allowing for common user interactions
    /// </summary>
    public class HomePage : WebPage
    {
        private const string TransactionTableLocator = "table#transactionsTable";
        public static By TransactionTable = By.CssSelector(TransactionTableLocator);

        private const string TransactionTableCategoryHeaderLocator = "th#category";
        private const string TransactionTableAmountHeaderLocator = "th#amount";
        private const string TransactionTableStatusHeaderLocator = "th#status";
        private const string TransactionTableDateHeaderLocator = "th#date";
        private const string TransactionTableDescriptionHeaderLocator = "th#description";
        private const string CompareExpensesLinkLocator = "a#showExpensesChart";
        public static By TransactionTableCategoryHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableCategoryHeaderLocator}");
        public static By TransactionTableAmountHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableAmountHeaderLocator}");
        public static By TransactionTableStatusHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableStatusHeaderLocator}");
        public static By TransactionTableDateHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableDateHeaderLocator}");
        public static By TransactionTableDescriptionHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableDescriptionHeaderLocator}");
        public static By TransactionTableRow = By.CssSelector($"{TransactionTableLocator} > tbody > tr");
        public static By CompareExpensesLink = By.CssSelector(CompareExpensesLinkLocator);
        public static By GetTransactionTableRow(int index) => By.CssSelector($"{TransactionTableLocator} > tbody > tr:nth-child({index + 1}");
        public static By GetTransactionTableRowCell(int rowIndex, int colIndex) => By.CssSelector($"{TransactionTableLocator} > tbody > tr:nth-child({rowIndex + 1}) > td:nth-child({colIndex + 1})");
        
        private const string AdLocator = "div[id*=\"flashSale\"] > img[src*=\"gif\"]";
        public static By Ad = By.CssSelector(AdLocator);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="But">The WebDriver Instance to interact with the page</param>
        public HomePage(IWebDriver But) : base(But)
        {
            var wait = new WebDriverWait(But, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until( (browser) => {
                browser.FindElement(TransactionTable);
                browser.FindElement(TransactionTableAmountHeader);
                browser.FindElement(TransactionTableCategoryHeader);
                browser.FindElement(TransactionTableDescriptionHeader);
                browser.FindElement(TransactionTableStatusHeader);
                browser.FindElement(TransactionTableDateHeader);
                browser.FindElement(CompareExpensesLink);
                return true;
            });
        }

        /// <summary>
        /// Click the Compare Expenses Button on the Page
        /// </summary>
        /// <returns>The Chart page object</returns>
        public ChartPage ClickCompareExpenses()
        {
            _but.FindElement(CompareExpensesLink).Click();
            return new ChartPage(_but);
        }

        /// <summary>
        /// Counts the number of rows in the recent transactions table
        /// </summary>
        /// <returns>Number of Displayed Transactions</returns>
        public int GetNumberOfRecentTransactions()
        {
            return _but.FindElements(TransactionTableRow).Count;
        }

        /// <summary>
        /// Reads the Recent Transactions table data displayed to the user
        /// </summary>
        /// <returns>List of Transactions</returns>
        public IList<Transaction> GetRecentTransactions()
        {
            var transactionCount = GetNumberOfRecentTransactions();
            IList<Transaction> transactions = new List<Transaction>(transactionCount);
            var amountColIndex = int.Parse(_but.FindElement(TransactionTableAmountHeader).GetAttribute("cellIndex"));
            var dateTimeColIndex = int.Parse(_but.FindElement(TransactionTableDateHeader).GetAttribute("cellIndex"));
            var categoryColIndex = int.Parse(_but.FindElement(TransactionTableCategoryHeader).GetAttribute("cellIndex"));
            var descColIndex = int.Parse(_but.FindElement(TransactionTableDescriptionHeader).GetAttribute("cellIndex"));
            var statusColIndex = int.Parse(_but.FindElement(TransactionTableStatusHeader).GetAttribute("cellIndex"));
            
            for (var i = 0; i < transactionCount; i++)
            {
                transactions.Add(new Transaction
                {
                    transactionDateTime = GetTransactionCellContents(i, dateTimeColIndex),
                    amount = decimal.Parse(GetTransactionCellContents(i, amountColIndex).Replace("USD", "").Replace("+","").Replace(" ","")),
                    category = GetTransactionCellContents(i, categoryColIndex),
                    description = GetTransactionCellContents(i, descColIndex),
                    status = GetTransactionCellContents(i, statusColIndex)
                });
            }
            return transactions;
        }
        
        private string GetTransactionCellContents(int rowIndex, int colIndex)
        {
            return _but.FindElement(GetTransactionTableRowCell(rowIndex, colIndex)).Text;
        }

        /// <summary>
        /// Clicks the Recent Transactions header to sort the table by the values in the Amount column
        /// </summary>
        public void SortRecentTransactionsByAmount()
        { 
            _but.FindElement(TransactionTableAmountHeader).Click();
        }

        /// <summary>
        /// Scans the Homepage for Ads and counts them
        /// </summary>
        /// <returns>The number of Ads displayed on the Homepage</returns>
        public int GetNumberOfAds(bool displayed = true)
        {
            return displayed ? _but.FindElements(Ad).Count(ad => ad.Displayed): _but.FindElements(Ad).Count;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ApplitoolsHackathonTraditional.businessobjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApplitoolsHackathonTraditional.pageobjects
{
    /// <summary>
    /// Object representing the applications homepage allowing for common user interactions
    /// </summary>
    public class HomePage : WebPage
    {
        private const string TransactionTableLocator = "table#transactionsTable";
        private static By TransactionTable = By.CssSelector(TransactionTableLocator);

        private const string TransactionTableCategoryHeaderLocator = "th#category";
        private const string TransactionTableAmountHeaderLocator = "th#amount";
        private const string TransactionTableStatusHeaderLocator = "th#status";
        private const string TransactionTableDateHeaderLocator = "th#date";
        private const string TransactionTableDescriptionHeaderLocator = "th#description";
        private static By TransactionTableCategoryHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableCategoryHeaderLocator}");
        private static By TransactionTableAmountHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableAmountHeaderLocator}");
        private static By TransactionTableStatusHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableStatusHeaderLocator}");
        private static By TransactionTableDateHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableDateHeaderLocator}");
        private static By TransactionTableDescriptionHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableDescriptionHeaderLocator}");
        private static By TransactionTableRow = By.CssSelector($"{TransactionTableLocator} > tbody > tr");
        private static By GetTransactionTableRowCell(int rowIndex, int colIndex) => By.CssSelector($"{TransactionTableLocator} > tbody > tr:nth-child({rowIndex + 1}) > td:nth-child({colIndex + 1})");

        private const string AdLocator = "div[id*=\"flashSale\"] > img[src*=\"gif\"]";
        private static By Ad = By.CssSelector(AdLocator);
        
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
                return true;
            });
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

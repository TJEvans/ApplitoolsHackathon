using System;
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

        private const string TransactionTableAmountHeaderLocator = "th#amount";
        private const string CompareExpensesLinkLocator = "a#showExpensesChart";
        public static By TransactionTableAmountHeader = By.CssSelector($"{TransactionTableLocator} {TransactionTableAmountHeaderLocator}");
        public static By CompareExpensesLink = By.CssSelector(CompareExpensesLinkLocator);

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
        /// Clicks the Recent Transactions header to sort the table by the values in the Amount column
        /// </summary>
        public void SortRecentTransactionsByAmount()
        { 
            _but.FindElement(TransactionTableAmountHeader).Click();
        }
    }
}

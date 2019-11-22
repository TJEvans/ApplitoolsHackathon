using System.Linq;
using ApplitoolsHackathonTraditional.pageobjects;
using NUnit.Framework;

namespace ApplitoolsHackathonTraditional.testcases
{
    /// <summary>
    /// Execute the user behavior described in...
    /// https://applitools.com/hackathon-instructions#table-sort
    /// https://applitools.com/hackathon-instructions#chart-test
    /// https://applitools.com/hackathon-instructions#dynamic-content
    /// </summary>
    [TestFixture]
    public class HomepageTestcase : HackathonTestcase
    {
        /// <summary>
        /// Sort the Recent transactions by amount
        /// Ensure that the data has remained the same
        /// Ensure that the rows are in ascending order of amount
        /// </summary>
        [Test]
        public void RecentTransactionsAmountSorting()
        {
            var homePage = new LoginPage(But).Login("jgomez", "password");
            var tableData = homePage.GetRecentTransactions();
            homePage.SortRecentTransactionsByAmount();
            var sortedTableData = homePage.GetRecentTransactions();
            Assert.AreEqual(tableData.Count, sortedTableData.Count(transaction => tableData.Contains(transaction)), "Verify all transaction data remains the same after Sorting");
            var tempAmount = decimal.MinValue;
            foreach (var transaction in sortedTableData)
            {
                if(tempAmount > transaction.amount) Assert.True(false, "Recent Transactions Table is NOT sorted ascending by amount");
                tempAmount = transaction.amount;
            }
            Assert.True(true, "Recent Transactions Table is sorted ascending by amount");
        }

        [Test]
        [Explicit]
        public void TransactionsChartTest()
        {
            //TODO
            //Need to find an alternate solution to run this test as its data and functionality is buried in the Canvas element
            //We could utilize the JavascriptExecutor to read/interact with the Chart but this requires intimate application knowledge
            //We could request developers add a query param to the application that allows the canvas to render as SVGs, this support is built into some framework
            //We could pursue a Visual Testing application to compare the Look of the chart, this technology has come a long way since Sikuli, and I look forward to trying AppliTools Eyes
        }

        /// <summary>
        /// Verify that Ads are displayed when enabled
        /// </summary>
        [Test]
        public void MonitizationTest()
        {
            var homePage = LoginPage.NavigateToPage(But, true).Login("jgomez", "password");
            Assert.AreEqual(2, homePage.GetNumberOfAds(), "Verify Two Flash Sales are displayed when enabled");
        }
    }
}

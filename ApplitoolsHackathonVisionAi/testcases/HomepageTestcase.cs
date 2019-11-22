using ApplitoolsHackathonVisionAi.pageobjects;
using NUnit.Framework;

namespace ApplitoolsHackathonVisionAi.testcases
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

            //Sort Table of Transactions by Amount
            homePage.SortRecentTransactionsByAmount();

            //Check the Table after Sorting
            Eyes.CheckElement(HomePage.TransactionTable, "jgomez homepage transactions sorted by amount");
        }

        /// <summary>
        /// Login and Navigate to the Expenses Chart.  Verify the look and feel of the chart then add more data and verify again.
        /// NOTE: This element is a Canvas so we cannot verify it directly, instead rely on AppliTools VisualAI comparison to check the Chart between runs
        /// </summary>
        [Test]
        public void TransactionsChartTest()
        {
            var homePage = new LoginPage(But).Login("jgomez", "password");
            
            //Open Expenses Chart Page
            var chartPage = homePage.ClickCompareExpenses();

            //Visually compare the Expense Chart, this is great solution to validating canvas's when you control the test data
            Eyes.CheckElement(ChartPage.chartCanvas, "Default Expense Chart");
            
            //Add more data to the chart
            chartPage.AddAnotherYearOfData();
            
            //Visually compare the Expense Chart, this is great solution to validating canvas's when you control the test data
            Eyes.CheckElement(ChartPage.chartCanvas, "Expense Chart with 2019 Data");
        }

        /// <summary>
        /// Verify that Ads are displayed when enabled
        /// </summary>
        [Test]
        public void MonitizationTest()
        {
            var homePage = LoginPage.NavigateToPage(But, true).Login("jgomez", "password");
            
            //If our goal is to validate only the existence of two Gif Ads the above validation is recommended
            //Adding Visual checks though allow us to confirm there are no unintended consequences in other portions of the UI when enabling Ads
            Eyes.CheckWindow("Homepage with Ads");
        }
    }
}

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ApplitoolsHackathonTraditional.testcases
{
    [TestFixture]
    public class HackathonTestcase
    {
        public const string BaseUrl = "https://demo.applitools.com/hackathonV2.html";

        /// <summary>
        ///     The Browser Under Test
        /// </summary>
        protected IWebDriver But;

        /// <summary>
        /// Load the Driver instance and navigate to the base url for the test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var config = new ChromeConfig();
            new DriverManager().SetUpDriver(config);
            var options = new ChromeOptions();
            options.AddLocalStatePreference("download.prompt_for_download", false);
            But = new ChromeDriver(options);
            But.Url = BaseUrl;
        }

        /// <summary>
        /// Teardown the Driver after each test
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            But?.Quit();
        }
    }
}
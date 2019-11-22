using System.Drawing;
using Applitools;
using Applitools.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ApplitoolsHackathonVisionAi.testcases
{
    [TestFixture]
    public class HackathonTestcase
    {
        /// <summary>
        ///     The Browser Under Test
        /// </summary>
        protected IWebDriver But;
        
        //AppliTools Eyes Object
        protected Eyes Eyes;
        
        //ApplitToole Eyes Runner
        private EyesRunner _runner;

        /// <summary>
        /// Load the Driver instance and navigate to the base url for the test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            //Setup and Configure Selenium for Browser Automation
            var config = new ChromeConfig();
            new DriverManager().SetUpDriver(config);
            var options = new ChromeOptions();
            options.AddLocalStatePreference("download.prompt_for_download", false);
            But = new ChromeDriver(options);
            
            //Initialize the Runner for your test.
            _runner = new ClassicRunner();
            
            // Initialize the eyes SDK (IMPORTANT: make sure your API key is set in the APPLITOOLS_API_KEY env variable).
            Eyes = new Eyes(_runner);
            Eyes.SaveNewTests = true;
            Eyes.BranchName = "v2";
            
            //Open AppliTool's Eyes and start a test based on the method name
            Eyes.Open(But, "AppliToolsHackathon", TestContext.CurrentContext.Test.MethodName, new Size(1440,900));

            //Utilize the Batching Feature to group tests by their Class
            Eyes.Batch.Name = TestContext.CurrentContext.Test.ClassName;

            // Navigate to the end desired Url
            But.Url = "https://demo.applitools.com/hackathonV2.html";
        }

        /// <summary>
        /// Teardown the Driver after each test
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            Eyes?.CloseAsync();
            But?.Quit();
            _runner.GetAllTestResults(); //TODO Do something with this?
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            But?.Quit();
            Eyes?.AbortIfNotClosed();
        }
    }
}
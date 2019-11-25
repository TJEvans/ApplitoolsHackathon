using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApplitoolsHackathonVisionAi.pageobjects
{
    /// <summary>
    /// Object representing the applications chart page allowing for common user interactions
    /// </summary>
    public class ChartPage : WebPage
    {
        private const string addAnotherYearsDataButtonLocator = "button#addDataset";
        private const string chartCanvasLocator = "canvas#canvas";
        public static By addAnotherYearsDataButton = By.CssSelector(addAnotherYearsDataButtonLocator);
        public static By chartCanvas = By.CssSelector(chartCanvasLocator);

        public ChartPage(IWebDriver But) : base(But)
        {
            var wait = new WebDriverWait(But, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until<bool>((browser) => {
                browser.FindElement(addAnotherYearsDataButton);
                browser.FindElement(chartCanvas);
                return true;
            });
        }

        /// <summary>
        /// Click the Button to add more data
        /// </summary>
        public void AddAnotherYearOfData()
        {
            _but.FindElement(addAnotherYearsDataButton).Click();
        }
    }
}

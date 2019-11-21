using OpenQA.Selenium;

namespace ApplitoolsHackathonVisionAi.pageobjects
{
    public class WebPage
    {
        protected IWebDriver _but;

        public WebPage(IWebDriver But)
        {
            _but = But;
        }
    }
}

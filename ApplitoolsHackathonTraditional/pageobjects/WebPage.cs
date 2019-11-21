using OpenQA.Selenium;

namespace ApplitoolsHackathonTraditional.pageobjects
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

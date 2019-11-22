using System;
using ApplitoolsHackathonVisionAi.testcases;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApplitoolsHackathonVisionAi.pageobjects
{
    /// <summary>
    /// Object representing the applications login page allowing for common user interactions
    /// </summary>
    public class LoginPage : WebPage
    {
        public static readonly By AlertMessage = By.CssSelector("div.alert-warning");

        //The biggest advantage xPath has over css is inner text locators, but it makes globalization much harder
        public static readonly By UsernameField = By.XPath("//label[.='Username']/../input");
        public static readonly By PasswordField = By.XPath("//label[.='Pwd']/../input");
        public static readonly By LoginButton = By.XPath("//button[.='Log In']");
        public static readonly By RememberMeCheckBox = By.XPath("//label[.='Remember Me']/input");

        /// <summary>
        /// Navigate to the applications Login Page
        /// </summary>
        /// <param name="driver">The driver to do the navigation</param>
        /// <param name="enableAds">True to enable Ads on the client</param>
        /// <returns>Object representing the Login Page on successful navigation</returns>
        public static LoginPage NavigateToPage(IWebDriver driver, bool enableAds = false)
        {
            var sUrl = HackathonTestcase.BaseUrl;
            if (enableAds) sUrl += "?showAd=true";
            driver.Url = sUrl;
            return new LoginPage(driver);
        }

        public LoginPage(IWebDriver But) : base(But)
        {
            var wait = new WebDriverWait(But, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until<bool>( (browser) => {
                browser.FindElement(UsernameField);
                browser.FindElement(PasswordField);
                browser.FindElement(LoginButton);
                browser.FindElement(RememberMeCheckBox);
                return true;
            });
        }

        /// <summary>
        /// Fill in the Login form and click login
        /// </summary>
        /// <param name="username">username string</param>
        /// <param name="password">password string</param>
        /// <returns>HomePage object representing the page re-directed to after a successful login</returns>
        public HomePage Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
            return new HomePage(_but);
        }

        /// <summary>
        /// Send Keys to the Login Forms username field
        /// </summary>
        /// <param name="name">The username string to enter</param>
        public void EnterUsername(string name)
        {
            if(string.IsNullOrEmpty(name)) _but.FindElement(UsernameField).Clear();
            else _but.FindElement(UsernameField).SendKeys(name);
        }  

        /// <summary>
        /// Send Keys to the Login Forms password field
        /// </summary>
        /// <param name="pass">The password string to enter</param>
        public void EnterPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) _but.FindElement(PasswordField).Clear();
            else _but.FindElement(PasswordField).SendKeys(pass);
        }

        /// <summary>
        /// Click the Login Button
        /// </summary>
        public void ClickLogin()
        {
            _but.FindElement(LoginButton).Click();
        }

        /// <summary>
        /// Click on the Remember Me checkbox
        /// </summary>
        /// <param name="enabled">The desired state of the Remember Me checkbox</param>
        public void SetRememberMe(bool enabled)
        {
            if (_but.FindElement(RememberMeCheckBox).GetAttribute("checked")
                .Equals(enabled.ToString(), StringComparison.OrdinalIgnoreCase)) return;
            _but.FindElement(RememberMeCheckBox).Click();
        }

        /// <summary>
        /// Determines whether a warning message is displayed to the user
        /// </summary>
        /// <returns>True if a warning is displayed on the page</returns>
        public bool IsAlertPresent()
        {
            try
            {
                _but.FindElement(AlertMessage);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Reads the alert message
        /// </summary>
        /// <returns>The string representing the warning message displayed to the user</returns>
        public string GetAlertMessage()
        {
            return !IsAlertPresent() ? "" : _but.FindElement(AlertMessage).Text.Trim();
        }
    }
}

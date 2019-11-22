using ApplitoolsHackathonTraditional.pageobjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ApplitoolsHackathonTraditional.testcases
{
    /// <summary>
    /// Execute the user workflows described in https://applitools.com/hackathon-instructions#login-page & https://applitools.com/hackathon-instructions#data-driven-test
    /// </summary>
    [TestFixture]
    public class LoginTestcase : HackathonTestcase
    {
        private const string NoUserError = "Username must be present";
        private const string NoPassError = "Password must be present";
        private const string NoUserAndPassError = "Both Username and Password must be present";

        /// <summary>
        /// Attempts to Login with a valid username and password and verifies successful login
        /// </summary>
        [Test]
        public void SuccessfulLogin()
        {
            //Verify the Login Page
            var loginPage = new LoginPage(But);
            //Verify behavior after logging in 
            Assert.AreEqual(typeof(HomePage), loginPage.Login("jgomez", "password123").GetType(), "Logging in presents the user with the Homepage");
        }

        /// <summary>
        /// Attempts to Login with no username and verifies the expected error
        /// </summary>
        [Test]
        public void NoUserNameError()
        {
            //Verify the Login Page
            var loginPage = new LoginPage(But);

            //Login without a username
            Assert.Throws<WebDriverTimeoutException>(() => loginPage.Login("", "password123"), "Homepage does not load after unsuccessful login");
            Assert.AreEqual(NoUserError, loginPage.GetAlertMessage(), "Logging in without a user produced the expected error");
        }

        /// <summary>
        /// Attempts to Login with no password and verifies the expected error
        /// </summary>
        [Test]
        public void NoPasswordError()
        {
            //Verify the Login Page
            var loginPage = new LoginPage(But);

            //Login without a password
            Assert.Throws<WebDriverTimeoutException>(() => loginPage.Login("jgomez", ""), "Homepage does not load after unsuccessful login");
            Assert.AreEqual(NoPassError, loginPage.GetAlertMessage(), "Logging in without a password produced the expected error");
        }

        /// <summary>
        /// Attempts to Login with no username and password and verifies the expected error
        /// </summary>
        [Test]
        public void NoUserNameOrPassError()
        {
            //Verify the Login Page
            var loginPage = new LoginPage(But);

            //Login without a user or password
            Assert.Throws<WebDriverTimeoutException>(() => loginPage.Login("", ""), "Homepage does not load after unsuccessful login");
            Assert.AreEqual(NoUserAndPassError, loginPage.GetAlertMessage(), "Logging in without a user or password produced the expected error");
        }
    }
}

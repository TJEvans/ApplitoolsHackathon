using ApplitoolsHackathonVisionAi.pageobjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ApplitoolsHackathonVisionAi.testcases
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
            //Was previously only able to confirm the existence of the basic form elements, but with Eyes the whole look and feel can be checked for consistency
            Eyes.CheckWindow("Initial Login Page");
            
            //Verify behavior after logging in 
            Assert.AreEqual(typeof(HomePage), loginPage.Login("jgomez", "password123").GetType(), "Logging in presents the user with the Homepage");

            //Was previously only able to confirm the existence of a select few elements on the page, but with Eyes the whole look and feel can be checked for consistency
            Eyes.CheckWindow("jgomez homepage");
        }

        /// <summary>
        /// Attempts to Login with no username and verifies the expected error
        /// </summary>
        [Test]
        public void NoUsernameError()
        {
            //Verify the Login Page
            var loginPage = new LoginPage(But);
            
            //Login without a username
            Assert.Throws<WebDriverTimeoutException>(() => loginPage.Login("", "password123"), "Homepage does not load after unsuccessful login");
            Assert.AreEqual(NoUserError, loginPage.GetAlertMessage(), "Logging in without a user produced the expected error");

            //Was previously only able to confirm the raw text of the Message, but with Eyes I can now validate the look and feel
            Eyes.CheckElement(LoginPage.AlertMessage, "No Username Error");
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

            //Was previously only able to confirm the raw text of the Message, but with Eyes I can now validate the look and feel
            Eyes.CheckElement(LoginPage.AlertMessage, "No Password Error");
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

            //Was previously only able to confirm the raw text of the Message, but with Eyes I can now validate the look and feel
            Eyes.CheckElement(LoginPage.AlertMessage, "No Username or Password Error");
        }
    }
}

using AutomationExerciseDemo.Data.Models;
using AutomationExerciseDemo.Utilities;
using AutomationExerciseDemo.UI.Pages;
using Microsoft.Playwright;
using NUnit.Framework;

namespace AutomationExerciseDemo.UI.Tests
{

    public class LoginTests : BaseUiTest
    {
        
        //page objects:
        private LoginPage _loginPage;
        private HomePage _homePage;

        [SetUp]
        public void TestSetup()
        {
            _loginPage = new LoginPage(Page,Config);
            _homePage = new HomePage(Page, Config);
                        
        }

        [Test, Retry(2)]
        [TestCaseSource(nameof(LoginUsers))]
        public async Task UserLoginCorrectEmailandPwd_TestCase_2(UserData data)
        {

            //Navigate to Login pg
            await _loginPage.NavigateAsync();

            //Enter user credentials:
            await _loginPage.LoginAsync(data.Email, data.Password);

            //verify user is directed to the HomePage and is logegd in
            Assert.That(await _homePage.IsUserLoggedInAsync(), Is.True);

        } 

        [Test, Retry(2)]
        [TestCaseSource(nameof(LoginUsers))]
        public async Task UserLoginInvalidEmail_TestCase_3a(UserData data)
        {
           
            //navigate to Login pg
            await _loginPage.NavigateAsync();

            //Enter invalid email and correct password credentials:
            await _loginPage.LoginAsync(data.Email+"invalid", data.Password);

            //verify invalid email message is displayed
            Assert.That(await _loginPage.IsLoginErrorVisibleAsync(), Is.True);

            
        }

        [Test, Retry(2)]
        [TestCaseSource(nameof(LoginUsers))]
        public async Task UserLoginInvalidPassword_TestCase_3b(UserData data)
        {
            //navigate to Login pg
            await _loginPage.NavigateAsync();

            //Enter correct email and invlaid password credentials:
            await _loginPage.LoginAsync(data.Email, data.Password+"invalid");

            //verify invalid email message is displayed
            Assert.That(await _loginPage.IsLoginErrorVisibleAsync(), Is.True);

            

        }


        [Test, Retry(2)]
        [TestCaseSource(nameof(LoginUsers))]
        public async Task UserLogn_Logout_TestCase_4(UserData data)
        {
            
            //navigate to login pg
            await _loginPage.NavigateAsync();

            //login with valid user credentials
            await _loginPage.LoginAsync(data.Email, data.Password);

            //ensure user is logged in
            Assert.That(await _homePage.IsUserLoggedInAsync(), Is.True);

            //Click the Logout button
            await _homePage.ClickLogoutAsync();

            //verify the user is successfully logged out
            Assert.That(await _loginPage.IsUserLoggedOut(), Is.True);
            
        }


        //create LoginUsers data set:
        public static IEnumerable<UserData> LoginUsers =>
            CsvReader.ReadCsv<UserData>(Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "TestData",
                "Login",
                "users.csv"));

    }



}
using AutomationExerciseDemo.Data.Models;
using AutomationExerciseDemo.Utilities;
using AutomationExerciseDemo.UI.Pages;
using Microsoft.Playwright;
using NUnit.Framework;

namespace AutomationExerciseDemo.UI.Tests
{
    public class RegistrationTests : BaseUiTest
    {
        
        //variables/objects
        private LoginPage _loginPage;
        private SignUpPage _signUpPage;
        private AccountCreatedPage _accountCreatedPage;
        private AccountDeletedPage _accountDeletedPage;

        [SetUp]
        public void TestSetup()
        {
            _loginPage = new LoginPage(Page, Config);
            _signUpPage = new SignUpPage(Page, Config);
            _accountCreatedPage = new AccountCreatedPage(Page, Config);
            _accountDeletedPage = new AccountDeletedPage(Page, Config);

        }

        [Test, Retry(2)]
        [TestCaseSource((nameof(NewUserData)))]
        public async Task RegisterUserTest_TestCase_1(SignUpData data)
        {

            /*
            //load CSV test Data
            var csvPath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "TestData",
                "Registration",
                "RegistrationData.csv"
            );

            var testData = CsvReader.ReadCsv<SignUpData>(csvPath);

            //step thru CSV Test Data set and register users then delete account 
            foreach(var data in testData)
            {
            */

        
                //Navigate to Login:
                await _loginPage.NavigateAsync();


                //enter signup name and email, click SignUp button
                await _loginPage.RegisterAsync(data.Name, data.Email);

                //Fill out Signup data form:, click Submit button
                await _signUpPage.FillFormAsync(data);
                await _signUpPage.CreateAccountAsync();


                //Verify new user account is created
                Assert.That(await _accountCreatedPage.IsSuccessMessageVisibleAsync(), Is.True);

                //Click Continue:
                await _accountCreatedPage.ClickContinue();



                //Delete Account:
               await _accountCreatedPage.ClickDeleteAccount();

                //Verify account deleted message is displayed:
                Assert.That(await _accountDeletedPage.IsSuccessMessageVisibleAsync(), Is.True);


            //}
        }

        [Test, Retry(2)]
        [TestCaseSource(nameof(ExistingUserData))]
        public async Task RegisterExistingUser_TestCase_5(UserData data)
        {
            //load CSV data:
           /*
            var csvPath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "TestData",
                "Registration",
                "RegistrationExistingUsers.csv"
            );

            var testData = CsvReader.ReadCsv<UserData>(csvPath);


            // attempt to register an Existing User
            foreach(var data in testData)
            {
            */

                //Navigate to Login:
                await _loginPage.NavigateAsync();


                //enter signup name and email, click SignUp button
                await _loginPage.RegisterAsync(data.Name, data.Email);
                
            //}


            //verify Email Address already exist! error is displayed
            Assert.That(await _loginPage.IsEmailExistMsgVisible(), Is.True);



        }

        //create Registration New Users Data set

        public static IEnumerable<SignUpData> NewUserData =>
        CsvReader.ReadCsv<SignUpData>(Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "TestData",
                "Registration",
                "RegistrationData.csv"));


        //Create Existing Registered User Data set
        public static IEnumerable<UserData> ExistingUserData =>
        CsvReader.ReadCsv<UserData>(Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "TestData",
                "Registration",
                "RegistrationExistingUsers.csv"));

    }
}
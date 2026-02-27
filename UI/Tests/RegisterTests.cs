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

        [Test]
        public async Task RegisterUserTest()
        {

            //load CSV test Data
            var csvPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "TestData",
                "Registration",
                "RegistrationData.csv"
            );

            var testData = CsvReader.ReadCsv<SignUpData>(csvPath);

            //step thru CSV Test Data set and register users then delete account 
            foreach(var data in testData)
            {
                //Navigate to Login:
                await _loginPage.NavigateAsync();


                //enter signup name and email, click SignUp button
                await _loginPage.RegisterAsync(data.Name, data.Email);

                //Fill out Signup data form:, click Submit button
                await _signUpPage.FillFormAsync(data);
                await _signUpPage.CreateAccountAsync();


                //Verify new user account is created
                Assert.That(await _accountCreatedPage.IsSuccessMessageVisibleAsync(), Is.True);



                //Delete Account:
               await _accountCreatedPage.ClickDeleteAccount();

                //Verify account deleted message is displayed:
                Assert.That(await _accountDeletedPage.IsSuccessMessageVisibleAsync(), Is.True);


            }
        }

    }
}
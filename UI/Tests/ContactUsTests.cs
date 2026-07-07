using AutomationExerciseDemo.Data.Models;
using AutomationExerciseDemo.Utilities;
using AutomationExerciseDemo.UI.Pages;
using Microsoft.Playwright;
using NUnit.Framework;

namespace AutomationExerciseDemo.UI.Tests
{

    public class ContactUsTests : BaseUiTest
    {
        
        //page objects:
        LoginPage _loginPage;
        HomePage _homePage;
        ContactUsPage _contactUsPage;


        [SetUp]
        public void SetUp()
        {
            _loginPage = new LoginPage(Page,Config);
            _homePage = new HomePage(Page, Config);
            _contactUsPage = new ContactUsPage(Page, Config);
                        
        }

        [Test, Retry(2)]
        [TestCaseSource(nameof(ContactFormUsers))]
        public async Task CompleteContactUsForm_TestCase_6(ContactFormData data)
        {
           

            //Navigate to ContactUs page
            await _contactUsPage.NavigateAsync();

            //verify the Get In Touch banner is visible
            Assert.That(await _contactUsPage.GetInTouchBannerIsDisplayedAsync(), Is.True);

            //Complete the contact us form, FilloutForm, calls UploadFile(string filePath)
            await _contactUsPage.FillOutFormAsync(data);

            //Click Submit button, ClickSubmit() uses a dialog handler to click ok for the JS Dialog box
            await _contactUsPage.ClickSubmitAsync();

            //verify the success message is displayed
            Assert.That(await _contactUsPage.SuccessMessageIsDisplayedAsync(), Is.True);

            //Click the Home button
            await _contactUsPage.GoHomeAsync();

            //Verify user is back on the home page:
            Assert.That(await _homePage.IsHomePageVisibleAsync(), Is.True);

           

        }


        // create ContactUs Form data set
        public static IEnumerable<ContactFormData> ContactFormUsers =>
            CsvReader.ReadCsv<ContactFormData>(Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "TestData",
                "ContactUs",
                "ContactUsForm.csv"));




    }


}
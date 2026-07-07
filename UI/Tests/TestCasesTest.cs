using AutomationExerciseDemo.Data.Models;
using AutomationExerciseDemo.Utilities;
using AutomationExerciseDemo.UI.Pages;
using Microsoft.Playwright;
using NUnit.Framework;


namespace AutomationExerciseDemo.UI.Tests
{
    

    public class TestCasesTest : BaseUiTest
    {
        
        //page objects:
        private TestCasesPage _testCasePage;
        private HomePage _homePage;

        [SetUp]
        public void TestSetup()
        {
            _testCasePage = new TestCasesPage(Page, Config);
            _homePage = new HomePage(Page, Config);
        }

        [Test, Retry(2)]
        public async Task VerifyTestCasesPage_tc7()
        {
            //navigate to HomePage:
            await _homePage.NavigateAsync();

            //Verify HomePage is displayed
            Assert.That(await _homePage.IsUserLoggedInAsync(), Is.True);

            //Click the Test Cases link
            await _homePage.GoToTestCasesPageAsync();

            //Verify the Test Cases Page is displayed
            Assert.That(_testCasePage.IsTestCaseHeaderDisplayedAsync(), Is.True);
        }



    }

}
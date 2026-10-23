
using AutomationExerciseDemo.Data.Models;
using AutomationExerciseDemo.Utilities;
using AutomationExerciseDemo.UI.Pages;
using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomationExerciseDemo.UI.Tests
{
    
    public class SubscriptionTests : BaseUiTest
    {
        
        //page objects
        private HomePage _homePage;
        private ShoppingCartPage _cartPage;


        //Setup:
        [SetUp]
        public void TestSetup()
        {
            
            _homePage = new HomePage(Page, Config);
            _cartPage = new ShoppingCartPage(Page, Config);
                        
        }

        [Test, Retry(2)]
        public async Task VerifySubscriptionOnHomePage_TestCase_10()
        {
            
            //navigate to Home Page:
            await _homePage.NavigateAsync();

            
            
            //Verify Subscription H2 text is visible:
            Assert.That(await _homePage.IsSubscriptionTextDisplayed(), Is.True);

            //Enter email and click subscribe button
            await _homePage.SubscribeByEmail("johndcrawford530@gmail.com");

            //Verify success msg is displayed:
            Assert.That(await _homePage.IsSubscriptionSuccessMsgDisplayed(), Is.True);

        } 


        [Test, Retry(2)]
        public async Task VerifySubscriptionOnCartPage_TestCase_11()
        {
            //navigate to home Page:
            await _homePage.NavigateAsync();

            //verify Home page is displayed
            Assert.That(await _homePage.IsHomePageVisibleAsync(), Is.True);

            //Click Cart button/link
            await _homePage.GoToCartPageAsync();

                        
            
            //Verify Subscription H2 text is visible:
            Assert.That(await _cartPage.IsSubscriptionTextDisplayed(), Is.True);

            //Enter email and click subscribe button
            await _cartPage.SubscribeByEmail("johndcrawford530@gmail.com");

            //Verify success msg is displayed:
            Assert.That(await _cartPage.IsSubscriptionSuccessMsgDisplayed(), Is.True);
            
        }



    }



}




        //TC10 - Verify subscription in home page

        //TC11 - Verify subscription in Cart Page
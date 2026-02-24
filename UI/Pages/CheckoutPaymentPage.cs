using Microsoft.Playwright;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.Data.Models;
using AutomationExerciseDemo.Config;



namespace AutomationExerciseDemo.UI.Pages
{

    public class CheckoutPaymentPage : BasePage
    {
        
        //locators:
        private const string NameOnCard = "input[data-qa='name-on-card']";
        private const string CardNumber = "input[data-qa='card-number']";
        private const string CVC = "input[data-qa='cvc']";
        private const string ExpiryMonth = "input[data-qa='expiry-month']";
        private const string ExpiryYear = "input[data-qa='expiry-year']";
        private const string PayButton = "button[data-qa='pay-button']";


        //Constructor:
        public CheckoutPaymentPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            
        }


        //fill out card payment info:
        public async Task FillInPaymentData(PaymentData cardInfo)
        {
            //fill in card data form fields:
            await TypeAsync(NameOnCard, cardInfo.Name);
            await TypeAsync(CardNumber, cardInfo.CardNumber);
            await TypeAsync (CVC, cardInfo.CVC);
            await TypeAsync(ExpiryMonth, cardInfo.ExpMonth);
            await TypeAsync(ExpiryYear, cardInfo.ExpYear);
        }


        //click the pay and confirm button:
        public async Task ClickPayButton()
        {
            await Page.Locator(PayButton).ClickAsync();
        }



    }



}




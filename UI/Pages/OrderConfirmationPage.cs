using Microsoft.Playwright;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.Config;


namespace AutomationExerciseDemo.UI.Pages
{

    public class OrderConfirmationPage : BasePage
    {
        

        //locators:
        private const string OrderPlacedMessage = "h2[data-qa='order-placed']";
        private const string DownloadInvoiceButton = "a.check_out";
        private const string ContinueButton ="a[data-qa='continue-button']";

        //constructor:
        public OrderConfirmationPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            
        }

        //verify Order Placed header is displayed:
        public async Task<bool> OrderPlacedSuccess()
        {
            await WaitForVisibleAsync(OrderPlacedMessage);
            return await IsVisibleAsync(OrderPlacedMessage);
        }


        // Click the DownloadInvoice button:
        public async Task ClickDownloadInvoice()
        {
            await Page.Locator(DownloadInvoiceButton).ClickAsync();
        }

        //Click Continue button:
        public async Task ClickContinue()
        {
            await Page.Locator(ContinueButton).ClickAsync();
        }

    }    




}
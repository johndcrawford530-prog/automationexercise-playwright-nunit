using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;



namespace AutomationExerciseDemo.UI.Pages
{
    
    public class AccountCreatedPage : BasePage 
    {
        
        //locators:
        private const string AccountCreatedMsg = "[data-qa='account-created']";
        private const string DeleteAccountLink = "a[href='/delete_account']";
        private const string ContinueButton = "a[data-qa='continue-button']";



        //Constructor:
        public AccountCreatedPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            
        }

        public async Task<bool> IsSuccessMessageVisibleAsync()
        {
           return await IsVisibleAsync(AccountCreatedMsg);
        }

        //delete user account once created:
        public async Task ClickDeleteAccount()
        {
            await Page.Locator(DeleteAccountLink).ClickAsync();
        }

        //Click Continue button:
        public async Task ClickContinue()
        {
            await Page.Locator(ContinueButton).ClickAsync();
        }


    }


}
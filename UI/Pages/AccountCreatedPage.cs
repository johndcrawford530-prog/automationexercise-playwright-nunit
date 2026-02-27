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


    }


}
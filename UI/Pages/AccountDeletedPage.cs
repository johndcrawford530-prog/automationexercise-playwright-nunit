using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;


namespace AutomationExerciseDemo.UI.Pages
{
    
    public class AccountDeletedPage : BasePage 
    {
        
        //locators:
        private const string AccountDeletedMsg = "[data-qa='account-deleted']";



        //Constructor:
        public AccountDeletedPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            
        }

        public async Task<bool> IsSuccessMessageVisibleAsync()
        {
           return await IsVisibleAsync(AccountDeletedMsg);
        }


    }


}
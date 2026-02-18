using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;



namespace AutomationExerciseDemo.UI.Pages
{
    
    public class AccountCreatedPage : BasePage 
    {
        
        //locators:
        private const string AccountCreatedMsg = "[data-qa='account-created']";



        //Constructor:
        public AccountCreatedPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            
        }

        public async Task<bool> IsSuccessMessageVisibleAsync()
        {
           return await IsVisibleAsync(AccountCreatedMsg);
        }


    }


}
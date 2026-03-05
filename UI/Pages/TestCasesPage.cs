using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.Config;


namespace AutomationExerciseDemo.UI.Pages
{
    
    public class TestCasesPage : BasePage
    {

        //locators:

        private const string TestCasesHeader = "h2:has-text('Test Cases')";


        //constructor:
        public TestCasesPage(IPage page, EnvironmentConfig config) : base(page,config){}


        //methods:

        //Is Test Case header visible:
        public async Task<bool> IsTestCaseHeaderDisplayedAsync()
        {
            return await IsVisibleAsync(TestCasesHeader);
        }



        
    }


}
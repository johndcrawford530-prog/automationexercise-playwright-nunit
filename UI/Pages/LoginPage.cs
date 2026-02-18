using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;
using System.Net.Quic;


namespace AutomationExerciseDemo.UI.Pages
{
    public class LoginPage : BasePage
    {
        
        //locators
        private const string EmailInput = "input[data-qa='login-email']";
        private const string PasswordInput = "input[data-qa='login-password']";
        private const string LoginButton = "button[data-qa='login-button']";
        private const string LoginError = "p:has-text('Your email or password is incorrect!')";

        private const string SignUpName = "input[data-qa='signup-name']";
        private const string SignUpEmail = "input[data-qa='signup-email']";
        private const string SignUpButton = "button[data-qa='signup-button']";


        //constructor:
        public LoginPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            
        }


        //navigate to login:
        public async Task NavigateAsync()
        {
            await NavigateToAsync(Config.LoginPath);

        }

        //login Action
        public async Task LoginAsync(string email, string password)
        {
            await TypeAsync(EmailInput, email);
            await TypeAsync(SignUpEmail, email);
            await ClickAsync(LoginButton);
        }


        //Register action
        public async Task RegisterAsync(string name, string email)
        {
            await TypeAsync(SignUpName, name);
            await TypeAsync(SignUpEmail, email);
            await ClickAsync(SignUpButton);
            
        }


        //Check for login error
        public async Task<bool> IsLoginErrorVisibleAsync()
        {
            return await IsVisibleAsync(LoginError);

        }


















    }


}
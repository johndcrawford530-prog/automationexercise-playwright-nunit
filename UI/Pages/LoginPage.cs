using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;
using System.Net.Quic;
using static Microsoft.Playwright.Assertions;


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
        private const string EmailExistsMsg = "p:has-text('Email Address already exist!')";
        private const string SignupLoginLink = "a[href='/login']";




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
            //check page loadstate:
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await ClearAdsAsync();

            //enter login data:
            await TypeAsync(EmailInput, email);
            await TypeAsync(PasswordInput, password);

            //wait for button to be visible:
            await WaitForVisibleAsync(LoginButton);
            await Expect(Page.Locator(LoginButton)).ToBeEnabledAsync();

            //scroll into view if needed:
           await Page.Locator(LoginButton).ScrollIntoViewIfNeededAsync();

            //try normal Click:
            try
            {
                //check page loadstate:
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                
                //click Login               
                await ClickAsync(LoginButton);

                //Wait for Navigation:
                await Page.WaitForURLAsync(url => !url.ToString().Contains("/login"));
               

                
            }
            catch
            {
                //force click:
                await Page.Locator(LoginButton).ClickAsync(new() {Force=true});
            }
          /*  

            //if page fails to navigate to home page, try clearing ads and clicking Login again:

                //wait a second for page ad-Hijack:
                await Page.WaitForTimeoutAsync(3000);

            //if URL is still on /Login, then re-click
            if (Page.Url.Contains("/login"))
            {
                await ClearAdsAsync();

                //ensure LoginButton is enabled
                await WaitForVisibleAsync(LoginButton);

                //force click:
                await Page.Locator(LoginButton).ClickAsync(new() {Force=true});

                //wait for possible hijack again
                await Page.WaitForTimeoutAsync(3000);
            }
            */

            //if page gets hijacked by Google Vignette ad, recover:
            if (Page.Url.Contains("google_vignette"))
            {
                //navigate to baseURL: and wait for DOM content to load
                await Page.GotoAsync(Config.BaseUrl);
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

                //clear ads
                await ClearAdsAsync();
            }


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
             //wait for error to display:
             await WaitForVisibleAsync(LoginError);

            return await IsVisibleAsync(LoginError);

        }

        //check for Email already exisits message:
        public async Task<bool> IsEmailExistMsgVisible()
        {
            return await IsVisibleAsync(EmailExistsMsg);
        }

        //confirm signup-login link is displayed, indicating user has been looged out
        public async Task<bool> IsUserLoggedOut()
        {
            return await IsVisibleAsync(SignupLoginLink);
        } 


















    }


}
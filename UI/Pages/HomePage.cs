using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.Config;


namespace AutomationExerciseDemo.UI.Pages
{

    public class HomePage : BasePage
    {


    //locators:
    private const string HomePageText = "div.features_items h2.title:has-text('FEATURES ITEMS')";
    private const string SignupLoginLink = "a[href='/login']";
    private const string ProductsLink = "a[href='/products']";
    private const string CartLink = "div.shop-menu.pull-right a[href='/view_cart']";
    private const string ContactUsLink = "a[href='/contact_us']";
    private const string TestCasesLink = "div.shop-menu.pull-right a[href='/test_cases']";
    private const string LogoutLink = "a[href='/logout']";
    private const string LoggedInUserLabel = "a:has-text('Logged in as')";
    /*
    private const string SubscriptionHeaderText = "h2:has-text('Subscription')";
    private const string SubscribeEmailInput = "input[id='susbscribe_email']";
    private const string SubscribeButton = "button[id='subscribe']";
    private const string SuccessAlertMsg = ".alert-success.alert";
    */
    private string ExpectedUrl => Config.BaseUrl;
    

    //constructor
    public HomePage(IPage page, EnvironmentConfig config) : base(page,config){}


    //navigate to home page
    public async Task NavigateAsync()
    {
        await NavigateToAsync(""); //Base URL only
    }


    // Check if home page loaded
    public async Task<bool> IsHomePageVisibleAsync()
    {
        var currentUrl = Page.Url.TrimEnd("/");
        var urlMatches = currentUrl.Equals(ExpectedUrl.TrimEnd("/"), StringComparison.OrdinalIgnoreCase);
        var bannerVisible = await IsVisibleAsync(HomePageText);

        return urlMatches && bannerVisible;
    }

    //navigate to Login/Signup page
    public async Task GoToLoginPageAsync()
    {
        //wait for header nav to display:
        await Page.WaitForSelectorAsync("div.shop-menu.pull-right", new(){ State = WaitForSelectorState.Visible});

        
        await ClickAsync(SignupLoginLink);
    }

    //navigate to Products page
    public async Task GoToProductsPageAsync()
    {
        //wait for header nav to display:
        await Page.WaitForSelectorAsync("div.shop-menu.pull-right", new(){ State = WaitForSelectorState.Visible});

        
        await ClickAsync(ProductsLink);

        //google vignette fix:
         // allow time for the google vignette highjack:
            await Page.WaitForTimeoutAsync(300);
            await GoogleVignetteFixAsync($"{Config.BaseUrl}/products");


        
    }


    //navigate to Cart page
    public async Task GoToCartPageAsync()
    {
        //wait for header nav to display:
        await Page.WaitForSelectorAsync("div.shop-menu.pull-right", new(){ State = WaitForSelectorState.Visible});


        await ClickAsync(CartLink);
    } 

    //navigate to contact page
    public async Task GoToContactUsPageAsync()
    {
        
        //wait for header nav to display:
        await Page.WaitForSelectorAsync("div.shop-menu.pull-right", new(){ State = WaitForSelectorState.Visible});

        await ClickAsync(ContactUsLink);
    }

    //navigate to Test Cases page:
    public async Task GoToTestCasesPageAsync()
    {
        //wait for header nav to display:
        await Page.WaitForSelectorAsync("div.shop-menu.pull-right", new(){ State = WaitForSelectorState.Visible});
        
        await ClickAsync(TestCasesLink);


        

        //google vignette fix:
         // allow time for the google vignette highjack:
            await Page.WaitForTimeoutAsync(300);
            await GoogleVignetteFixAsync($"{Config.BaseUrl}/test_cases");

       
    }

    //Check if user is logged in
    public async Task<bool> IsUserLoggedInAsync()
    {
        //wait for login page to navigate to Home page.
        await WaitForVisibleAsync(LoggedInUserLabel);


        return await IsVisibleAsync(LoggedInUserLabel);
    }


    //Click Logout link:
    public async Task ClickLogoutAsync()
    {
        await ClickAsync(LogoutLink);
    }

/*

    //verify Subscription Header text is displayed:
    public async Task<bool> IsSubscriptionTextDisplayed()
        {
            
            return await IsVisibleAsync(SubscriptionHeaderText);
        }


    //enter email to subscribe to site email list.
    public async Task SubscribeByEmail(string email)
        {
            
            await HighlightAsync(SubscriptionHeaderText);
            await HighlightAsync(SubscribeEmailInput);
            await HighlightAsync(SubscribeButton);

            
            //enter email into subscription Email input field
            await TypeAsync(SubscribeEmailInput, email);

            //Click the subscribe button:
            await ClickAsync(SubscribeButton);

        }

    // verify the subscription success message is displayed:
    public async Task<bool> IsSubscriptionSuccessMsgDisplayed()
        {
            return await IsVisibleAsync(SuccessAlertMsg);
        }
*/

    }
}
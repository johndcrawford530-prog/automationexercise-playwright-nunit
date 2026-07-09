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
    private const string CartLink = "a[href='/view_cart']";
    private const string ContactUsLink = "a[href='/contact_us']";
    private const string TestCasesLink = "div.shop-menu.pull-right a[href='/test_cases']";
    private const string LogoutLink = "a[href='/logout']";
    private const string LoggedInUserLabel = "a:has-text('Logged in as')";
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


        await ClickAsync(ContactUsLink);
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

    }
}
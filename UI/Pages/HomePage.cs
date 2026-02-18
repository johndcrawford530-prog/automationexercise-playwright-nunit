using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.Config;


namespace AutomationExerciseDemo.UI.Pages
{

    public class HomePage : BasePage
    {


    //locators:
    private const string HomePageText = "h2:has-text('Full-Fledged practice website for Automation Engineers')";
    private const string SignupLoginLink = "a[href='/login']";
    private const string ProductsLink = "a[href='/products']";
    private const string CartLink = "a[href='/view_cart']";
    private const string ContactUsLink = "a[href='/contact_us']";
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
        var currentUrl = Page.Url;
        var urlMatches = currentUrl.Equals(ExpectedUrl, StringComparison.OrdinalIgnoreCase);
        var bannerVisible = await IsVisibleAsync(HomePageText);

        return urlMatches && bannerVisible;
    }

    //navigate to Login/Signup page
    public async Task GoToLoginPageAsync()
    {
        await ClickAsync(SignupLoginLink);
    }

    //navigate to Products page
    public async Task GoToProductsPageAsync()
    {
        await ClickAsync(ProductsLink);
        
    }


    //navigate to Cart page
    public async Task GoToCartPageAsync()
    {
        await ClickAsync(ContactUsLink);
    } 

    //navigate to contact page
    public async Task GoToContactUsPageAsync()
    {
        await ClickAsync(ContactUsLink);
    }

    //Check if user is logged in
    public async Task<bool> IsUserLoggedInAsync()
    {
        return await IsVisibleAsync(LoggedInUserLabel);
    }

    }
}
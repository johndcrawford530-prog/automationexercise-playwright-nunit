using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.Config;
using AutomationExerciseDemo.Data.Models;


namespace AutomationExerciseDemo.UI.Pages
{
    
    public class ContactUsPage : BasePage
    {
        
        //locators:
        private const string GetInTouchBanner = "h2:has-text('Get In Touch')";
        private const string Name = "input[data-qa='name']";
        private const string Email = "input[data-qa='email']";
        private const string Subject = "input[data-qa='subject']";
        private const string Message = "#message";
        private const string FileUpload = "input[name='upload_file']";
        private const string SubmitButton = "input[data-qa='submit-button']";
        private const string SuccessMessage = "div.status.alert.alert-success";
        private const string HomeLink = "ul.navbar-nav a:has-text('Home')";


        //Constructor:
        public ContactUsPage(IPage page, EnvironmentConfig config) : base(page,config){}




        //Methods:

        //Navigate to Contact Us page:
        public async Task NavigateAsync()
        {
            await NavigateToAsync("/contact_us"); //Base URL only

            //clear ads:
            await ClearAdsAsync();
        }

        // Fill out form fields:
        public async Task FillOutFormAsync(ContactFormData data)
        {



            //wait for page to fully load before continuing with the form fields:
            
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await WaitForVisibleAsync(Name);
            await WaitForVisibleAsync(Message);

            //clear ads:
            await ClearAdsAsync();


            //Enter data in form fields:
            await TypeAsync(Name,data.Name);
            await TypeAsync(Email,data.Email);
            await TypeAsync(Subject, data.Subject);
            await TypeAsync(Message,data.Message);

            //upload file:
            if(!string.IsNullOrWhiteSpace(data.FilePath)){
                await UploadFileAsync(data.FilePath);
            }
           
        }


        // upload file
        public async Task UploadFileAsync(string filePath)
        {
            //clear ads:
            await ClearAdsAsync();


            //build full filepath:
            var projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            filePath = Path.Combine(projectRoot, filePath);

            string resolvedFilePath = $"resolved Path: {filePath}";

            // Safety check
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            // Ensure the file input is present and ready
            //await WaitForVisibleAsync(FileUpload);
            var uploadInput = Page.Locator(FileUpload);
            await uploadInput.WaitForAsync();


            // Upload
            await Page.SetInputFilesAsync(FileUpload, filePath);
        }


        //Click Submit:
        public async Task ClickSubmitAsync()
        {
           

            // Handle the JS alert
            Page.Dialog += async (_, dialog) =>
            {
                await dialog.AcceptAsync();
            };

            // Ensure the form is fully stable
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            //clear ads:
            await ClearAdsAsync();

            // Re-locate the button to avoid stale DOM issues
            var submitButton = Page.Locator(SubmitButton);

            // Ensure the final DOM node is attached and visible
            await submitButton.WaitForAsync();

            // Click the fresh DOM node
            await submitButton.ClickAsync();
        







        }

        //Get In Touch Banner is displayed:
        public async Task<bool> GetInTouchBannerIsDisplayedAsync()
        {
            return await IsVisibleAsync(GetInTouchBanner);
        }


        // Success message displays
        public async Task<bool> SuccessMessageIsDisplayedAsync()
        {
            return await IsVisibleAsync(SuccessMessage);

        }

        //click Home link to return to home page:
        public async Task GoHomeAsync()
        {
            // wait for page load and clear ads:
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await ClearAdsAsync();

            //Click the Home link:
            await WaitForVisibleAsync(HomeLink);
            await ClickAsync(HomeLink);


            // allow time for the google vignette highjack:
            await Page.WaitForTimeoutAsync(300);

            //confirm if vignette has highjacked the URL, if true recover:
            if (Page.Url.Contains("google_vignette"))
            {
                //force navigation to home page:
                await Page.GotoAsync(Config.BaseUrl);
                
                //wait for Features title to be visible on Home page:
                await WaitForVisibleAsync("div.features_items h2.title:has-text('FEATURES ITEMS')");

                 //clear ads:
                await ClearAdsAsync();
                
            }            
        }




    }



}
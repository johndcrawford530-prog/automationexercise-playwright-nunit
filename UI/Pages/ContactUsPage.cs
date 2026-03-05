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
        private const string Name = "input[data-qa='name']";
        private const string Email = "input[data-qa='email']";
        private const string Subject = "input[data-qa='subject']";
        private const string Message = "input[data-qa='message']";
        private const string FileUpload = "input[name='upload_file']";
        private const string SubmitButton = "input[data-qa='submit-button']";
        private const string SuccessMessage = "div.status.alert.alert-success";


        //Constructor:
        public ContactUsPage(IPage page, EnvironmentConfig config) : base(page,config){}




        //Methods:

        // Fill out form fields:
        public async Task FillOutFormAsync(ContactFormData data)
        {
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
            await Page.SetInputFilesAsync(FileUpload, filePath);
        }


        //Click Submit:
        public async Task ClickSubmitAsync()
        {
            //register dialog handler:
            Page.Dialog += async (_, dialog) =>
            {
                await dialog.AcceptAsync();
            };


            await ClickAsync(SubmitButton);
        }


        // Success message displays
        public async Task<bool> SuccessMessageIsDisplayedAsync()
        {
            return await IsVisibleAsync(SuccessMessage);

        }




    }



}
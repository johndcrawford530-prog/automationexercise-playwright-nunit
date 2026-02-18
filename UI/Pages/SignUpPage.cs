using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;
using AutomationExerciseDemo.Data.Models;


namespace AutomationExerciseDemo.UI.Pages
{
    
    public class SignUpPage : BasePage{
        
        //locators:

        private const string TitleRadio_Mr = "input[id='id_gender1']";
        private const string TitleRadioMrs = "input[id='id_gender2']";
        private const string Name = "input[data-qa='name']";
        private const string Email = "input[data-qa='email']";
        private const string Password = "input[data-qa='password']";
        private const string DOB_day = "select[data-qa='days']";
        private const string DOB_month = "select[data-qa='months']";
        private const string DOB_year = "select[data-qa='years']";
        private const string Newsletter_ckbox = "input[id='newsletter']";
        private const string SpecialOffer_ckbox = "input[id='optin']";
        private const string FirstName = "input[data-qa='first_name']";
        private const string LastName = "input[data-qa='last_name']";
        private const string Company = "input[data-qa='company']";
        private const string Address1 = "input[data-qa='address']";
        private const string Address2 = "input[data-qa='address2']";
        private const string Country = "[data-qa='country']";
        private const string State = "[data-qa='state']";
        private const string City = "input[data-qa='city']";
        private const string ZipCode = "input[data-qa='zipcode']";
        private const string MobileNumber = "input[data-qa='mobile_number']";
        private const string CreateAccountButton = "button[data-qa='create-account']";
        

        //constructor
        public SignUpPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            
        }

        //complete signup form
        public async Task FillFormAsync(SignUpData data)
        {
            //if data.Title is Mr, then select the MR title radio button, if Mrs then select the Mrs radio button

            if(data.Title == "Mr.")
            {
                await Page.Locator(TitleRadio_Mr).CheckAsync();
            }
            else if(data.Title == "Mrs.")
            {
                await Page.Locator(TitleRadioMrs).CheckAsync();
            }
            else if(data.Title == null)
            {
                //skip selecting field, it is not a required 
            }



            //fill out remaining fields:

            //name and email fields are pre-filled.

            
            await TypeAsync(Password, data.Password);
        
            // parse out the Date of Birth values            
           
            if(DateTime.TryParseExact(data.DOB, "MM-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dob))
            {
                             
                //select the DOB - Day
            await Page.Locator(DOB_day).SelectOptionAsync(dob.Day.ToString());
            //select month
            await Page.Locator(DOB_month).SelectOptionAsync(dob.Month.ToString());
            //select year
            await Page.Locator(DOB_year).SelectOptionAsync(dob.Year.ToString());

            }
            else
            {
                // DOB format is not correct
                throw new Exception($"Invalid date format provided: {data.DOB}. Expected Format = MM-dd-yyyy ");
            }

            


            //Enter first name, last name, company, address1, address2 (if available)

            await TypeAsync(FirstName, data.FirstName);
            await TypeAsync(LastName, data.LastName);
            await TypeAsync(Company, data.Company);
            await TypeAsync(Address1, data.Address1);

            //check to see if Address2 is null
            if(data.Address2 == null)
            {
                //skip the field
            }
            else
            {
                await TypeAsync(Address2, data.Address2);
            }

            //select the country from the country dropdown
            await Page.Locator(Country).SelectOptionAsync(data.Country);


            // complete the State, City, zip code and mobile number
            await TypeAsync(State, data.State);
            await TypeAsync(City, data.City);
            await TypeAsync(ZipCode, data.ZipCode);
            await TypeAsync(MobileNumber, data.MobileNumber);



        }

        public async Task CreateAccountAsync()
        {
            // Click create account button 
            await Page.Locator(CreateAccountButton).ClickAsync();
        }

    }
}
using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.Config;
using System.Net.Quic;
using Microsoft.Playwright.Transport.Protocol;

namespace AutomationExerciseDemo.UI.Pages
{
    
    public class SignUpPage : BasePage{
        
        //locators:

        private const string TitleRadio_Mr = "input[id='id_gender1']";
        private const string TitleRadioMrs = "input[id='id_gender2']";
        private const string Name = "input[data-qa='name']";
        private const string Email = "input[data-qa='email']";
        private const string Password = "input[data-qa='passward']";
        private const string DOB_day = "selector[id='uniform-days']";
        private const string DOB_month = "selector[id='uniform-months']";
        private const string DOB_year = "selector[id='uniform-years']";
        private const string Newsletter_ckbox = "checker[id='uniform-newsletter']";

        

        //constructor


        //





    }
}
using Microsoft.Playwright;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.UI.Models;
using AutomationExerciseDemo.Config;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata;


namespace AutomationExerciseDemo.UI.Pages
{
    public class CheckoutPage : BasePage
    {
        
        //locators
        private readonly ILocator _cartRows;
        private const string DeliveryAddress = "#address_delivery";
        private const string BillingAddress = "#address_billing";
        private const string TotalPrice = "cart_total_price";
        private const string Comments = "textarea[name='message']";


        //constructor:
        public CheckoutPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            _cartRows = page.Locator("table.table.table-condensed tbody tr");
        }


        //methods:

        //getDeliveryAdddress

        //get Billing Address

        //parseAddress




    }



}
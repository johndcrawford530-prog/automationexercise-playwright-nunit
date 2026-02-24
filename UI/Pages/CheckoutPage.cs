using Microsoft.Playwright;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.UI.Models;
using AutomationExerciseDemo.Config;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata;
using System.Linq.Expressions;


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
        private const string CheckOutButton = "a.check_out";


        //constructor:
        public CheckoutPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            _cartRows = page.Locator("table.table.table-condensed tbody tr");
        }


        //methods:

        //getDeliveryAdddress
        public async Task<CustomerAddress> GetDeliveryAddress()
        {
            var addressBox = Page.Locator(DeliveryAddress);
            var data = await addressBox.Locator("li").AllInnerTextsAsync();

            //parse data:
            CustomerAddress deliveryAddress = ParseAddress(data);

            return deliveryAddress;
        }


        //get Billing Address
        public async Task<CustomerAddress> GetBillingAddress()
        {
            var addressBox = Page.Locator(BillingAddress);
            var data = await addressBox.Locator("li").AllInnerTextsAsync();

            //parse data
            CustomerAddress billing_address = ParseAddress(data);

            return billing_address;
            
        }


        //parseAddress
        private CustomerAddress ParseAddress(IReadOnlyList<string> data)
        {
            
            var address = new CustomerAddress
            {
              FullName = data[1].Trim(),
              Company = data[2].Trim(),
              Address1 = data[3].Trim(),
              Address2 = data[4].Trim(),
              CityStateZip = data[5].Trim(),
              Country = data[6].Trim(),
              Phone = data[7].Trim()
            };

            return address;
        }


        //get cart item(s)
        public async Task<IReadOnlyList<CartItem>> GetCartItemsAsync()
        {
            var items = new List<CartItem>();
            int count = await _cartRows.CountAsync();

            for(int i = 0; i < count; i++)
            {
                var row = _cartRows.Nth(i);

                //get detaisl from item row
                var name = await row.Locator(".cart_description h4 a").InnerTextAsync();
                var description = await row.Locator(".cart_description p").InnerTextAsync();
                var priceText = await row.Locator(".cart_price p").InnerTextAsync();
                var quantityText = await row.Locator(".cart_quantity input").InputValueAsync();
                var totalText = await row.Locator(".cart_total_price").InnerTextAsync();                

                //add data to items
                items.Add(new CartItem
                {
                    Name = name.Trim(),
                    Description = description.Trim(),
                    Price = ParsePrice(priceText),
                    Quantity = int.Parse(quantityText),
                    Total = ParsePrice(totalText)
                });
            }

            return items;
        }


         //Find specific item in cart:
        public async Task<CartItem> GetCartItemAsync(string productName)
        {
            var cartItems = await GetCartItemsAsync();
            var product = cartItems.FirstOrDefault(i => i.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

            return product;

        }


        // parse price data:
        private decimal ParsePrice(string text)
        {
            return decimal.Parse(text.Replace("Rs.", "").Trim());
        }


        //Add comments:
        public async Task AddComments(string commentText)
        {
            await TypeAsync(Comments, commentText);
        }


        //Click Place Order:
        public async Task ClickPlaceOrder()
        {
            await Page.Locator(CheckOutButton).ClickAsync();
            
        }






    }



}
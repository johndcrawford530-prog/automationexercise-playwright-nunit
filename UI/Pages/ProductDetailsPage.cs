using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.UI.Models;
using AutomationExerciseDemo.Config;


namespace AutomationExerciseDemo.UI.Pages
{
    
    public class ProductDetailsPage : BasePage
    
    {
        
        //locators:
        protected const string ProductName = "div.product-information h2";
        protected const string Category = "div.product-information p:has-text('Category')";
        protected const string Price = "div.product-information span > span";
        protected const string Availability = "div.product-information p:has-text('Availability')";
        protected const string Condition = "div.product-information p:has-text('Condition')";
        protected const string Brand = "div.product-information p:has-text('Brand')";
        protected const string QuantityInput = "input[id='quantity']";
        protected const string AddToCartButton = "button[class='btn btn-default cart']";
        protected const string ReviewNameInput = "input[id='name']";
        protected const string ReviewEmailAddressInput = "input[id='email']";
        protected const string ReviewTextBoxInput = "textarea[id='review']";
        protected const string ReviewSubmitButton = "button[id='button-review']";



        //constructor
        public ProductDetailsPage(IPage page, EnvironmentConfig config) : base(page, config){}

        //navigate to page:
        public async Task NavigateAsync(int productID)
        {
            await NavigateToAsync($"/product_details/{productID}"); 
        }

        //Get product details:

        //Product Name
        public async Task<string> GetProductNameAsync()
        {
            return await GetTextAsync(ProductName);
        }

        //Price Text:
        public async Task<string> GetPriceTextAsync()
        {
            return  await GetTextAsync(Price);
        }


        //Price Value:
        public async Task<decimal> GetPriceValueAsync()
        {
            //get price string
             var priceString = await GetPriceTextAsync();
            //parse priceString to only digits
            var priceNum = new string(priceString.Where(char.IsDigit).ToArray());

            //return priceNum as a decimal
            return decimal.Parse(priceNum);

        }


        //adjust quantity
        public async Task SetQuantityAsync(int qty)
        {
            await TypeAsync(QuantityInput, qty.ToString());
        }


        // get Category
        public async Task<string> GetCategoryAsync()
        {
            return await GetTextAsync(Category);
        }


        //GetAvaibability
        public async Task<string> GetAvailbiltiyAsync()
        {
            return await GetTextAsync(Availability);
        }


        //get Condition
        public async Task<string> GetConditionAsync()
        {
            return await GetTextAsync(Condition);
        }



        //Get Brand
        public async Task<string> GetBrand()
        {

            
            return await GetTextAsync(Brand);
        }



        //Add to cart
        public async Task AddToCartAsync()
        {
            await ClickAsync(AddToCartButton);
        }



        //write review
        public async Task WriteReviewAsync(string name, string email, string reviewText)
        {
            await TypeAsync(ReviewNameInput, name);
            await TypeAsync(ReviewEmailAddressInput, email);
            await TypeAsync(ReviewTextBoxInput, reviewText);

            //click submit button
            await ClickAsync(ReviewSubmitButton);

        }





    }


}
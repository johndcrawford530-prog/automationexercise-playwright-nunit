using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.UI.Models;
using AutomationExerciseDemo.Config;


namespace AutomationExerciseDemo.UI.Pages
{
    
    public class ProductsPage : BasePage
    {
        //locators:
        protected const string SearchInputBox = "input[id='search_product']";
        protected const string SearchButton = "button[id='submit_search']";
        protected const string ProductsList = "div.product-image-wrapper";



        //constructor:
        public ProductsPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            
        }


       //navigate to Products page:

        public async Task NavigateAsync()
        {
            await NavigateToAsync("/products"); //base URL
        }

       //Search for product:
       public async Task SearchForProductAsync(string productName)
        {

            await TypeAsync(SearchInputBox, productName);
            await ClickAsync(SearchButton);

           
            
        }


        //get all products
        public async Task<IEnumerable<ProductItem>> GetAllProductsAsync()
        {
            var items = Page.Locator(ProductsList);
            int count =  await items.CountAsync();

            var products = new List<ProductItem>();

            //loop thru products and add all product items to a list as a new productItem getting the name, price, addToCart button and ViewDetails link and the root element.

            for(int i = 0; i < count; i++)
            {
                var item = items.Nth(i);

                products.Add(new ProductItem
                {
                    Name = await item.Locator(".productinfo p").InnerTextAsync(),
                    Price = await item.Locator(".productinfo h2").InnerTextAsync(),
                    AddToCartButton = item.Locator(".productinfo a.add-to-cart"),
                    ViewDetailsLink = item.Locator(".choose a[href*='product_details']"),
                    RootElement = item

                });

            }

             return products;
            
        }

        //get the first product on the Page:
        public async Task<ProductItem> GetFirstProductAsync()
        {
            var products = await GetAllProductsAsync();
            
            
            return products.FirstOrDefault();
        }

       //Find product, input: product name, return could be null if item not found.
       public async Task<ProductItem?> FindProductAsync(string productName)
        {
            var products = await GetAllProductsAsync();
             

            return products.FirstOrDefault(p=> p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase));


        }

        //Is Products page displayed:
        public async Task<bool> IsProductsPageDisplayedAsync()
        {
            var url = Page.Url;
            bool isVisible = false;

            if (url.Contains("/products") && await Page.IsVisibleAsync(SearchInputBox) )
            {
                isVisible = true;
            }

            return isVisible;

        }


    }



}
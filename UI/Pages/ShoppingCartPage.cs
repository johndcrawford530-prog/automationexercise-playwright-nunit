using Microsoft.Playwright;
using System.Threading.Tasks;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.UI.Models;
using AutomationExerciseDemo.Config;
using System.Security.Cryptography;


namespace AutomationExerciseDemo.UI.Pages
{
    public class ShoppingCartPage : BasePage
    {
        //locators:
        private readonly ILocator _cartRows;

        //constructor:
        public ShoppingCartPage(IPage page, EnvironmentConfig config) : base(page, config)
        {
            _cartRows = page.Locator("table.table.table-condensed tbody tr");
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
        public async Task<CartItem> getCartItem(string productName)
        {
            var productRow = _cartRows.Filter(new() { HasTextString = productName});

            var name = await productRow.Locator(".cart_description h4 a").InnerTextAsync();
            var description = await productRow.Locator(".cart_description p").InnerTextAsync();
            var priceText = await productRow.Locator(".cart_price p").InnerTextAsync();
            var quantityText = await productRow.Locator(".cart_quantity input").InputValueAsync();
            var totalText = await productRow.Locator(".cart_total_price").InnerTextAsync();

            var product = new CartItem
            {
                Name = name.Trim(),
                Description = description.Trim(),
                Price = ParsePrice(priceText),
                Quantity = int.Parse(quantityText),
                Total = ParsePrice(totalText)
            };

            return product;

        }

        // parse price data:
        private decimal ParsePrice(string text)
        {
            return decimal.Parse(text.Replace("Rs.", "").Trim());
        }

        // remove item from cart

        //proceed to checkout





    }
}
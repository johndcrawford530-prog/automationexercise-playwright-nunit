using Microsoft.Playwright;
using AutomationExerciseDemo.UI.Pages;
using AutomationExerciseDemo.UI.Models;
using AutomationExerciseDemo.Config;
using System.Linq;


namespace AutomationExerciseDemo.UI.Pages
{
    public class ShoppingCartPage : BasePage
    {
        //locators:
        private readonly ILocator _cartRows;
        private const string CheckoutButton = "a.check_out";

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
        public async Task<CartItem> getCartItemAsync(string productName)
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

        // remove item from cart
        public async Task RemoveItemAsync(string productName)
        {
            //find Item row:
            var itemRow = _cartRows.Filter(new(){HasTextString = productName});

            // if item not found, fail
            if(await itemRow.CountAsync() == 0)
            {
                throw new Exception($"product '{productName}' not found in cart");
            }

            //click remove button
            await itemRow.Locator(".cart_quantity_delete").ClickAsync();

            //ensure row is removed.
            await Assertions.Expect(itemRow).ToHaveCountAsync(0);
        }

        //proceed to checkout
        public async Task ProceedtoCheckoutAsync()
        {
            await Page.Locator(CheckoutButton).ClickAsync();
        }




    }
}
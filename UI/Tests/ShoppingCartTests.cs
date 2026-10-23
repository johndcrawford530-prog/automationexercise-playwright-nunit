
using AutomationExerciseDemo.Data.Models;
using AutomationExerciseDemo.Utilities;
using AutomationExerciseDemo.UI.Pages;
using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomationExerciseDemo.UI.Tests
{
    
    public class ShoppingCartTests : BaseUiTest
    {
        //Page Objects:
        private HomePage _homePage;
        private ProductsPage _productsPage;
        private ProductDetailsPage _productDetailPage;
        private ShoppingCartPage _cartPage;


        //Setup:
        [SetUp]
        public void TestSetup()
        {
            
            _homePage = new HomePage(Page, Config);
            _productsPage = new ProductsPage(Page, Config);
            _productDetailPage = new ProductDetailsPage(Page, Config);
            _cartPage = new ShoppingCartPage(Page, Config);
                        
        }




        //Test Cases to incldue:  12, 13, 17

        [Test, Retry(2)]
        public async Task AddProductToCart_TestCase_12()
        {
            //navigate to Products Page:
            await _productsPage.NavigateAsync();

            //get first product in list, and click Add to Cart
            var productItem = await _productsPage.GetFirstProductAsync();

            //click add to cart link:
            //await productItem.AddToCartButton.ClickAsync();
            await _productsPage.ClickAddToCartAsync(productItem);

            //Go to CartPage, verify the product is successfully added to the cart.  Ensure price, quantity and total price is correct
            await _cartPage.NavigateAsync();

            //Get cart Item using productItem.Name
            var cartItem = await _cartPage.GetCartItemAsync(productItem.Name);

            if(cartItem != null)
            {
                //verify price matches:
                Assert.That(productItem.Price == cartItem.Price, Is.True);
            }

        }


        


    }


}



//Test Cases to incldue:  12, 13, 14, 15, 16, 17
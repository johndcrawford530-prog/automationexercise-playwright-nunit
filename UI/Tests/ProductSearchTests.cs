using AutomationExerciseDemo.Data.Models;
using AutomationExerciseDemo.Utilities;
using AutomationExerciseDemo.UI.Pages;
using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomationExerciseDemo.UI.Tests
{

    public class ProductSearchTests : BaseUiTest
    {
        
        //page objects:
        private LoginPage _loginPage;
        private HomePage _homePage;
        private ProductsPage _productsPage;
        private ProductDetailsPage _productDetailsPage;
        private ShoppingCartPage _cartPage;
        

        [SetUp]
        public void TestSetup()
        {
            _loginPage = new LoginPage(Page,Config);
            _homePage = new HomePage(Page, Config);
            _productsPage = new ProductsPage(Page, Config);
            _productDetailsPage = new ProductDetailsPage(Page, Config);
            _cartPage = new ShoppingCartPage(Page, Config);
                        
        }


        //TC08 - Verify all Products and Product Detail page
        [Test, Retry(2)]
        public async Task VerifyAllProductsAndProductDetailsPage_TestCase_8()
        {

            //Navigate to home page
            await _homePage.NavigateAsync();

            //Verify Home page is displayed
            Assert.That(await _homePage.IsHomePageVisibleAsync(), Is.True);

            //Click on Products pg
            await _homePage.GoToProductsPageAsync();

            //Verify All Products page is displayed
            Assert.That(await _productsPage.IsProductsPageDisplayedAsync(), Is.True);

            //Ensure the Product list is visible and grab the first item in the list
           
            var firstProduct = await _productsPage.GetFirstProductAsync();

            
            // ensure first product exist and click on View Product for the first product in the list
            Assert.IsNotNull(firstProduct);
            await firstProduct!.ViewDetailsLink.ClickAsync();

            // Verify the Product Detail page is displayed and contains the following data points:  Product Name, Category, Price, Availability, Condition, Brand
            Assert.That(await _productDetailsPage.IsProductDetailsPageDisplayedAsync(), Is.True);

                
        }

         


        //TC09- Search Product
        [Test, Retry(2)]
        [TestCaseSource(nameof(SearchData))]
        public async Task VerifyProductSearch_TestCase_8(ProductSearchData data)
        {
            
             //Navigate to home page
            await _homePage.NavigateAsync();

            //Verify Home page is displayed
            Assert.That(await _homePage.IsHomePageVisibleAsync(), Is.True);

            //Click on Products pg
            await _homePage.GoToProductsPageAsync();
            
            var searchTerm = data.SearchTerm;
            var keywords = data.Keywords.Split(';');

            await _productsPage.SearchForProductAsync(searchTerm);

            var results = await _productsPage.GetAllProductsAsync();

            Assert.That(results.Any(), "Search returned no products.");

            Assert.That(results.All(p => SearchRelevanceHelper.IsRelevant(p, keywords)),
                $"One or more products were not related to '{searchTerm}'.");
            
        }

        //TC10 - Verify subscription in home page

        //TC11 - Verify subscription in Cart Page

        //TC12 - Verify products in Cart

        //TC13 - Verify product quantity in Cart


        
/*************************************************************
*******  Data sets asnd helpers ******************************
*************************************************************/
        //create LoginUsers data set:
        public static IEnumerable<UserData> LoginUsers =>
            CsvReader.ReadCsv<UserData>(Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "TestData",
                "Login",
                "users.csv"));


                 //create product search data data set:
    public static IEnumerable<ProductSearchData> SearchData =>
        CsvReader.ReadCsv<ProductSearchData>(Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "TestData",
                "TestFiles",
                "ProductSearchData.csv"));


    }


   


}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SDETInterviewChallenge.PageObjectModels;
using SDETInterviewChallenge.TestData;
using SDETInterviewChallenge.Utilities;

namespace SDETInterviewChallenge.TestCases
{
    class InterviewTests : TestFixture
    {
        [TestCaseSource(typeof(InterviewTestData), nameof(InterviewTestData.TestData))]
        public void InterviewTest(List<string> menuItems, string menuToNavigate,
            string menuToPrint, string searchValue, int elementsToPrint, int numberOfItems)
        {
            try
            {
                HomePage homePage = new HomePage(Driver, DriverWait);
                WindowsPage windowsPage = new WindowsPage(Driver, DriverWait);
                SearchResultsPage searchResultsPage = new SearchResultsPage(Driver, DriverWait);
                ProductDetailsPage productDetailsPage = new ProductDetailsPage(Driver, DriverWait);
                ShoppingCartPage shoppingCartPage = new ShoppingCartPage(Driver, DriverWait);
                string storedPrice;

                //Step 1. Go to https://www.microsoft.com/en-us/
                homePage.NavigateTo();

                //Step 2. Validate all menu items are present (Office - Windows - Surface - Xbox - Deals - Support)
                var menuNames = homePage.GetMenuNames();
                Assert.AreEqual(menuItems, menuNames);

                //Step 3. Go to Windows
                homePage.ClickMenu(menuToNavigate);
                Assert.IsTrue(Driver.Url.ToUpper().Contains(menuToNavigate.ToUpper()));

                //Step 4. Once in Windows page, click on Windows 10 Menu
                windowsPage.ClickMenu(menuToPrint);

                //Step 5. Print all Elements in the dropdown
                var itemsToPrint = windowsPage.GetMenuDropDownList();
                foreach(var item in itemsToPrint)
                {
                    Console.WriteLine(item);
                }

                //Step 6. Go to Search next to the shopping cart
                windowsPage.ClickSearchButton();

                //Step 7. Search for Visual Studio
                windowsPage.PerformSearch(searchValue);

                //Step 8. Print the price for the 3 first elements listed in Software result list
                searchResultsPage.ValidateStoreLanguage();
                var priceList= searchResultsPage.GetPriceList();
                if (priceList.Count < elementsToPrint)
                {
                    Console.WriteLine("Not enough elements to print");
                    Assert.Fail();
                }
                else
                {
                    for (int i = 0; i < elementsToPrint; i++)
                    {
                        Console.WriteLine(priceList[i]);
                    }
                }

                //Step 9. Store the price of the first one
                storedPrice = priceList.First();

                //Step 10. Click on the first one to go to the details page
                searchResultsPage.ClickProductByPrice(storedPrice);

                //Step 11. Once in the details page, validate both prices are the same
                productDetailsPage.DenyNewsletter();
                Assert.AreEqual(storedPrice, productDetailsPage.GetProductPrice());

                //Step 12. Click Add To Cart
                productDetailsPage.ClickAddToCartButton();

                //Step 13. Verify all 3 price amounts are the same
                var priceAmounts = shoppingCartPage.GetProductPriceList();
                foreach(var price in priceAmounts)
                {
                    Assert.AreEqual(storedPrice, price);
                }

                //Step 14. On the # of items dropdown select 20 and validate the Total amount is Unit Price * 20
                shoppingCartPage.SetNumberOfItems(numberOfItems);
                var expectedTotalAmount = CurrencyConverter.FromCurrency(storedPrice) * numberOfItems;
                var actualTotalAmount = CurrencyConverter.FromCurrency(shoppingCartPage.GetProductGrossPrice());
                Assert.AreEqual(expectedTotalAmount, actualTotalAmount);

            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"Assertion Failed with message: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Fatal error with message: {ex.Message}");
                Assert.Fail();
            }
        }
    }
}

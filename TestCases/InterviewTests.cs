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

namespace SDETInterviewChallenge.TestCases
{
    class InterviewTests : TestFixture
    {
        [TestCaseSource(typeof(InterviewTestData), nameof(InterviewTestData.TestData))]
        public void InterviewTest(List<string> menuItems, string menuToNavigate,
            string menuToPrint, string searchValue, int elementsToPrint)
        {
            try
            {
                LandingPage landingPage = new LandingPage(Driver, DriverWait);
                WindowsPage windowsPage = new WindowsPage(Driver, DriverWait);
                string price;

                landingPage.NavigateTo();

                var menuNames = landingPage.GetMenuNames();
                Assert.AreEqual(menuItems, menuNames);

                landingPage.ClickMenu(menuToNavigate);
                Assert.IsTrue(Driver.Url.ToUpper().Contains(windowsPage.PageName.ToUpper()));

                windowsPage.ClickMenu(menuToPrint);
                var itemsToPrint = windowsPage.GetMenuDropDownList();
                foreach(var item in itemsToPrint)
                {
                    Console.WriteLine(item);
                }


            }
            catch(AssertionException ex)
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

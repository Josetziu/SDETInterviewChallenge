using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace SDETInterviewChallenge.PageObjectModels
{
    public class LandingPage : IPageNavigation
    {
        public string URL { get { return ConfigurationManager.AppSettings.Get("url"); } }
        public string PageName { get { return "Microsoft"; } }

        private readonly IWebDriver _Driver;
        private readonly WebDriverWait _DriverWait;
        //h3[contains(text(), 'Visual Studio')]/following-sibling::div[@class='c-channel-placement-price']//span[@itemprop='price']
        [FindsBy(How = How.XPath, Using = "//nav[contains(@aria-label, 'menu')]//a")]
        private IList<IWebElement> _MenuList { get; set; }

        public LandingPage(IWebDriver driver, WebDriverWait driverWait)
        {
            _Driver = driver;
            _DriverWait = driverWait;
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Navigate to the page's URL.
        /// </summary>
        public void NavigateTo()
        {
            _Driver.Url = URL;
        }
        
        public List<string> GetMenuNames()
        {
            List<string> output = new List<string>();
            foreach(var element in _MenuList)
            {
                output.Add(element.Text);
            }
            return output;
        }

        public void ClickMenu(string menuToClick)
        {
            _DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//nav[contains(@aria-label, 'menu')]//a[text()='Windows']")));
            var elementToClick = _MenuList.First(x => x.Text == menuToClick);
            elementToClick.Click();
        }
    }
}

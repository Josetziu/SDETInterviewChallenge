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
    public class WindowsPage : IPageNavigation
    {
        public string URL { get { return ConfigurationManager.AppSettings.Get("url") + "windows/"; } }
        public string PageName { get { return "Windows"; } }

        private readonly IWebDriver _Driver;
        private readonly WebDriverWait _DriverWait;

        [FindsBy(How = How.XPath, Using = "//nav[contains(@aria-label, 'menu')]//button[@aria-expanded]")]
        private IList<IWebElement> _MenuList { get; set; }

        [FindsBy(How = How.XPath, Using = "//nav[contains(@aria-label, 'menu')]//ul[@aria-hidden='false']//a")]
        private IList<IWebElement> _MenuDropdownList { get; set; }

        public WindowsPage(IWebDriver driver, WebDriverWait driverWait)
        {
            _Driver = driver;
            _DriverWait = driverWait;
            PageFactory.InitElements(driver, this);
        }

        public void NavigateTo()
        {
            _Driver.Url = URL;
        }

        public List<string> GetMenuDropDownList()
        {
            List<string> output = new List<string>();

            _DriverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//nav[contains(@aria-label, 'menu')]//ul[@aria-hidden='false']//a")));

            try
            {
                foreach (var dropdown in _MenuDropdownList)
                {
                    output.Add(dropdown.Text);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return output;
        }

        public void ClickMenu(string menuToClick)
        {
            _DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//nav[contains(@aria-label, 'menu')]//button[@aria-expanded]")));
            var elementToClick = _MenuList.First(x => x.Text == menuToClick);
            elementToClick.Click();
        }
    }
}

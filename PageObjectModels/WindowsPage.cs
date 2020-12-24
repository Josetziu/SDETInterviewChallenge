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
    public class WindowsPage : PageUtilities
    {
        #region LOCATORS
        private const string MENULIST_XPATH = "//nav[contains(@aria-label, 'menu')]//button[@aria-expanded]";
        private const string MENUDROPDOWNLIST_XPATH = "//nav[contains(@aria-label, 'menu')]//ul[@aria-hidden='false']//a";
        private const string SEARCHBUTTON_ID = "search";
        private const string SEARCHBOX_ID = "cli_shellHeaderSearchInput";
        #endregion

        public WindowsPage(IWebDriver driver, WebDriverWait driverWait)
        {
            _Driver = driver;
            _DriverWait = driverWait;
            URL = ConfigurationManager.AppSettings.Get("url") + "windows/";
            PageTitle = "Explore Windows 10 OS, Computers, Apps, & More | Microsoft";
            PageFactory.InitElements(driver, this);
        }

        #region ELEMENT DEFINITIONS
        [FindsBy(How = How.XPath, Using = MENULIST_XPATH)]
        private IList<IWebElement> _MenuList { get; set; }

        [FindsBy(How = How.XPath, Using = MENUDROPDOWNLIST_XPATH)]
        private IList<IWebElement> _MenuDropdownList { get; set; }

        [FindsBy(How = How.Id, Using = SEARCHBUTTON_ID)]
        private IWebElement _SearchButton { get; set; }

        [FindsBy(How = How.Id, Using = SEARCHBOX_ID)]
        private IWebElement _SearchBox { get; set; }
        #endregion

        #region PUBLIC METHODS
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

        public void ClickSearchButton()
        {
            _SearchButton.Click();
        }

        public void PerformSearch(string searchValue)
        {
            _DriverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("cli_shellHeaderSearchInput")));
            _SearchBox.SendKeys(searchValue);
            _SearchBox.Submit();
        }
        #endregion
    }
}

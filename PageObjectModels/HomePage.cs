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
    public class HomePage : PageUtilities
    {
        #region LOCATORS
        private const string MENULIST_XPATH = "//nav[contains(@aria-label, 'menu')]//a";
        #endregion

        public HomePage(IWebDriver driver, WebDriverWait driverWait)
        {
            _Driver = driver;
            _DriverWait = driverWait;
            URL = ConfigurationManager.AppSettings.Get("url");
            PageTitle = "Microsoft - Official Home Page";
            PageFactory.InitElements(driver, this);
        }

        #region ELEMENT DEFINITIONS
        [FindsBy(How = How.XPath, Using = MENULIST_XPATH)]
        private IList<IWebElement> _MenuList { get; set; }
        #endregion

        #region PUBLIC METHODS
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
            _DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(MENULIST_XPATH)));
            var elementToClick = _MenuList.First(x => x.Text == menuToClick);
            elementToClick.Click();
        }
        #endregion
    }
}

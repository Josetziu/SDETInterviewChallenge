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
    public class SearchResultsPage : PageUtilities
    {
        #region LOCATORS
        private const string PRICELIST_XPATH = 
            "//h3[contains(text(), 'Visual Studio')]/following-sibling::div[@class='c-channel-placement-price']//span[@itemprop='price']";
        private const string DIALOGBOX_ID = "R1MarketRedirect-1";
        private const string DIALOGCONFIRM_ID = "R1MarketRedirect-close";
        #endregion

        public SearchResultsPage(IWebDriver driver, WebDriverWait driverWait)
        {
            _Driver = driver;
            _DriverWait = driverWait;
            URL = ConfigurationManager.AppSettings.Get("url") + "search?q=";
            PageTitle = "";
            PageFactory.InitElements(driver, this);
        }

        #region ELEMENT DEFINITIONS
        [FindsBy(How = How.XPath, Using = PRICELIST_XPATH)]
        private IList<IWebElement> _PriceList { get; set; }

        [FindsBy(How = How.Id, Using = DIALOGBOX_ID)]
        private IWebElement _DialogBox { get; set; }

        [FindsBy(How = How.Id, Using = DIALOGCONFIRM_ID)]
        private IWebElement _DialogConfirmButton { get; set; }
        #endregion

        #region PUBLIC METHODS
        public void ValidateStoreLanguage()
        {
            if (IsElementPresent(By.Id(DIALOGBOX_ID)))
                _DialogConfirmButton.Click();
        }
        public List<string> GetPriceList()
        {
            List<string> output = new List<string>();
            foreach(var price in _PriceList)
            {
                output.Add(price.Text);
            }
            return output;
        }
        public void ClickProductByPrice(string price)
        {
            var elementToClick = _PriceList.First(x => x.Text == price);
            elementToClick.Click();
        }
        #endregion
    }
}

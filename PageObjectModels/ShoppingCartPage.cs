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
    public class ShoppingCartPage : PageUtilities
    {
        #region LOCATORS
        private const string PRODUCTUNITPRICE_XPATH = "//div[@class='item-price']//span[@itemprop='price']/span";
        private const string PRODUCTGROSSPRICE_XPATH = "//span[contains(text(), 'Items')]/following-sibling::span//span[@itemprop='price']/span";
        private const string PRODUCTNETPRICE_XPATH = "//*[contains(text(), 'Total')]/parent::span/following-sibling::span//span[@itemprop='price']";
        private const string ITEMQUANTITYDROPDOWN_XPATH = "//div[@class='item-quantity']/select";
        private const string ITEMQUANTITYDROPDOWNOPTIONS_XPATH = "//div[@class='item-quantity']/select/option";
        #endregion

        public ShoppingCartPage(IWebDriver driver, WebDriverWait driverWait)
        {
            _Driver = driver;
            _DriverWait = driverWait;
            URL = ConfigurationManager.AppSettings.Get("url") + "store/cart";
            PageTitle = "Shopping - Microsoft Store";
            PageFactory.InitElements(driver, this);
        }

        #region ELEMENT DEFINITIONS
        [FindsBy(How = How.XPath, Using = PRODUCTUNITPRICE_XPATH)]
        private IWebElement _ProductUnitPrice { get; set; }

        [FindsBy(How = How.XPath, Using = PRODUCTGROSSPRICE_XPATH)]
        private IWebElement _ProductGrossPrice { get; set; }

        [FindsBy(How = How.XPath, Using = PRODUCTNETPRICE_XPATH)]
        private IWebElement _ProductNetPrice { get; set; }

        [FindsBy(How = How.XPath, Using = ITEMQUANTITYDROPDOWN_XPATH)]
        private IWebElement _ItemQuantityDropdown { get; set; }

        [FindsBy(How = How.XPath, Using = ITEMQUANTITYDROPDOWNOPTIONS_XPATH)]
        private IList<IWebElement> _ItemQuantityDropdownOptions { get; set; }
        #endregion

        #region PUBLIC METHODS
        public string GetProductGrossPrice()
        {
            return _ProductGrossPrice.Text;
        }

        public List<string> GetProductPriceList()
        {
            List<string> output = new List<string>();
            _DriverWait.Until(ExpectedConditions.ElementExists(By.XPath(PRODUCTUNITPRICE_XPATH)));
            output.Add(_ProductUnitPrice.Text);
            output.Add(_ProductGrossPrice.Text);
            output.Add(_ProductNetPrice.Text);

            return output;
        }

        public void SetNumberOfItems(int numberOfItems)
        {
            _ItemQuantityDropdown.Click();
            if(_ItemQuantityDropdownOptions.Count <= numberOfItems)
            {
                _ItemQuantityDropdownOptions.First(x => x.Text == numberOfItems.ToString()).Click();

                //Wait for the checkout button to be enabled and price to be updated
                _DriverWait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath($"//section[@class='_3LWrsBIG']//button")));
            }
            else
            {
                Console.WriteLine("Invalid number of Items");
            }
        }
        #endregion
    }
}

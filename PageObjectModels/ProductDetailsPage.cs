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
    public class ProductDetailsPage : PageUtilities
    {
        #region LOCATORS
        private const string PRODUCTPRICE_XPATH = "//div[@id='productPrice']//span";
        private const string ADDTOCARTBUTTON_XPATH = "//div[@id='buttonPanel']//button";
        private const string DIALOGBOX_XPATH = "//div[@id='email-newsletter-dialog' and @aria-hidden='false']";
        private const string CLOSEDIALOGBOXBUTTON_XPATH = "//div[@aria-label='Close']";
        #endregion

        public ProductDetailsPage(IWebDriver driver, WebDriverWait driverWait)
        {
            _Driver = driver;
            _DriverWait = driverWait;
            URL = ConfigurationManager.AppSettings.Get("url") + "p/";
            PageTitle = "";
            PageFactory.InitElements(driver, this);
        }

        #region ELEMENT DEFINITIONS
        [FindsBy(How = How.XPath, Using = PRODUCTPRICE_XPATH)]
        private IWebElement _ProductPrice { get; set; }

        [FindsBy(How = How.XPath, Using = ADDTOCARTBUTTON_XPATH)]
        private IWebElement _AddToCartButton { get; set; }

        [FindsBy(How = How.XPath, Using = DIALOGBOX_XPATH)]
        private IWebElement _DialogBox { get; set; }

        [FindsBy(How = How.XPath, Using = CLOSEDIALOGBOXBUTTON_XPATH)]
        private IWebElement _CloseDialogBoxButton { get; set; }
        #endregion

        #region PUBLIC METHODS
        public void DenyNewsletter()
        {
            _DriverWait.Until(ExpectedConditions.ElementExists(By.XPath(DIALOGBOX_XPATH)));
            if (IsElementPresent(By.XPath(DIALOGBOX_XPATH)))
                _CloseDialogBoxButton.Click();
        }
        public string GetProductPrice()
        {
            _DriverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PRODUCTPRICE_XPATH)));
            return _ProductPrice.Text;
        }

        public void ClickAddToCartButton()
        {
            //Scroll to the AddToCartButton in case it is blocked by other elements
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)_Driver;
            scriptExecutor.ExecuteScript("arguments[0].scrollIntoView();", _AddToCartButton);
            _AddToCartButton.Click();
        }
        #endregion
    }
}

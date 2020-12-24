using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SDETInterviewChallenge.PageObjectModels
{
    public abstract class PageUtilities
    {
        public string URL;
        public string PageTitle;

        protected IWebDriver _Driver;
        protected WebDriverWait _DriverWait;

        public void NavigateTo()
        {
            _Driver.Url = URL;
        }

        protected bool IsElementPresent(By by)
        {
            try
            {
                _Driver.FindElement(by);
                return true;
            }
            catch(NoSuchElementException)
            {
                return false;
            }
        }
    }
}

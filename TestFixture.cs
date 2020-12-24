using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace SDETInterviewChallenge
{
    public class TestFixture
    {
        public IWebDriver Driver;
        public WebDriverWait DriverWait;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [SetUp]
        public void SetUp()
        {
            if(Driver == null)
            {
                Driver = new ChromeDriver();
                Driver.Manage().Window.Maximize();
                DriverWait = new WebDriverWait(Driver, new TimeSpan(0, 1, 0)); //Wait up to 1 minute.
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.Quit();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }
    }
}

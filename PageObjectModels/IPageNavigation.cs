using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SDETInterviewChallenge.PageObjectModels
{
    interface IPageNavigation
    {
        string URL { get; }
        string PageName { get; }

        void NavigateTo();
    }
}

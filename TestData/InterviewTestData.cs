using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SDETInterviewChallenge.TestData
{
    public class InterviewTestData
    {
        public static IEnumerable TestData
        {
            get
            {
                yield return new TestCaseData(new List<string>()
                {
                    "Microsoft 365",
                    "Office",
                    "Windows",
                    "Surface",
                    "Xbox",
                    "Deals",
                    "Support"
                }, ConfigurationManager.AppSettings.Get("menuToNavigate"),
                ConfigurationManager.AppSettings.Get("menuToPrint"),
                ConfigurationManager.AppSettings.Get("searchValue"),
                Convert.ToInt32(ConfigurationManager.AppSettings.Get("elementsToPrint")),
                Convert.ToInt32(ConfigurationManager.AppSettings.Get("numberOfItems")))
                    .SetName("Default Validations");
            }
        }
    }
}

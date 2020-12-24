using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDETInterviewChallenge.Utilities
{
    public static class CurrencyConverter
    {
        public static int FromCurrency(string stringToConvert)
        {
            return int.Parse(stringToConvert, NumberStyles.Currency);
        }

        public static string ToCurrency(int integerToConvert)
        {
            return integerToConvert.ToString("C2");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5ERAT11.Services
{
    public static class StringExtensions
    {
        public static string ConvertOrderSizeToRoboForexFormat(this string amount)
        {
            string expectedOrderSize;
            if (amount.Contains(','))
            {
                if (amount.Substring(amount.IndexOf(',')).Length == 1)
                    expectedOrderSize = amount + '0';
                else
                    expectedOrderSize = amount;
            }
            else
            {
                expectedOrderSize  = amount + ",00";
            }
            return expectedOrderSize;
        }
    }
}

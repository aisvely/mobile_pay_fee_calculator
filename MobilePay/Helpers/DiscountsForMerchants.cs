using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MobilePay.Controllers.DiscountCalculationRules.DiscountsForMerchants
{
    public static class DiscountsForMerchants
    {

        /// <summary>
        /// Gets discounts list for merchants
        /// </summary>
        /// <returns>discounts list for merchants</returns>
        public static Dictionary<string, decimal> GetDiscounts()
        {
            return (ConfigurationManager.GetSection("discountSettings") as System.Collections.Hashtable)
                 .Cast<System.Collections.DictionaryEntry>()
                 .ToDictionary(n => n.Key.ToString(), n => Convert.ToDecimal(n.Value));
        }
    }
}

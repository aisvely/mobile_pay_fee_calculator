using MobilePay.Controllers.DiscountCalculationRules;
using System.Collections.Generic;

namespace MobilePay.Controllers.FeesCalculationRules
{
    class CalculateDiscount : IDiscountCalculationRule
    {
        /// <summary>
        /// Calculates discount for merchant depending of total transaction amount per month
        /// </summary>
        /// <param name="tranctionsAmount">transaction amount per month</param>
        /// <returns>fee model that contains discount amount</returns>
        public decimal CalculateDiscountForMerchant(decimal feeAmount, string merchantName, Dictionary<string, decimal> discountsForMerchants)
        {
            if (discountsForMerchants.ContainsKey(merchantName))
            {
                var discount = discountsForMerchants[merchantName];
                return feeAmount * discount / 100;
            }
            return 0;
        }
    }
}

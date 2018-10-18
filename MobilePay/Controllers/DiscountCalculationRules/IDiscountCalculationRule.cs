using System.Collections.Generic;

namespace MobilePay.Controllers.DiscountCalculationRules
{
    public interface IDiscountCalculationRule
    {
        decimal CalculateDiscountForMerchant(decimal feeAmount, string merchantName, Dictionary<string, decimal> discountsForMerchants);
    }
}

using MobilePay.Models;
using System.Configuration;

namespace MobilePay.Controllers.FeesCalculationRules
{
    public class CalculateAdditionalFees : IFeesCalculationRule
    {
        /// <summary>
        /// Calculates additional fee per month
        /// </summary>
        /// <param name="tranctionsAmount">transaction amount per month</param>
        /// <returns>fee model that contains additional fee amount</returns>
        public FeeModel CalculateFee(decimal tranctionsAmount)
        {
            decimal additionalFeeAmount = 0;
            decimal.TryParse(ConfigurationManager.AppSettings["additionalFeeAmount"], out additionalFeeAmount);
            return new FeeModel() { additionalFeesAmount = additionalFeeAmount };
        }
    }
}

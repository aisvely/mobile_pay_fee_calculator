using MobilePay.Models;
using System.Configuration;

namespace MobilePay.Controllers.FeesCalculationRules
{
    public class CalculateFeesByPercentage : IFeesCalculationRule
    {
        /// <summary>
        /// Calculates fees depending on transaction amount per month
        /// </summary>
        /// <param name="tranctionsAmount">transaction amount per month</param>
        /// <returns>fee model that contains fee amount</returns>
        public FeeModel CalculateFee(decimal tranctionsAmount)
        {
            var percentage = 0;
            int.TryParse(ConfigurationManager.AppSettings["feesPercentageOfTransactionsAmount"], out percentage);
            var fee = tranctionsAmount * percentage / 100;
            return new FeeModel() { feesAmount = fee};
        }
    }
}

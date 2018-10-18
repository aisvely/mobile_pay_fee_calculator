using MobilePay.Controllers.DiscountCalculationRules;
using MobilePay.Controllers.FeesCalculationRules;
using MobilePay.Models;
using System.Collections.Generic;
using System.Linq;

namespace MobilePay.Controllers
{
    public static class CalculateFees
    {
        /// <summary>
        /// Calculates fee, additional fee and discount for merchant per month
        /// </summary>
        /// <param name="merchantTransactions">list of merchant's transactions per month</param>
        /// <param name="feesCalculationInstances">instances of fees calculation classes</param>
        /// <param name="discountCalculationInstances">instances of discount calculation classes</param>
        /// <param name="discountsList">list of discounts for merchants</param>
        /// <returns>fee model that contains fee amount, additional fee amount and discount</returns>
        public static FeeModel CalculateFee(
            IEnumerable<TransactionModel> merchantTransactions,
            IEnumerable<IFeesCalculationRule> feesCalculationInstances, 
            IEnumerable<IDiscountCalculationRule> discountCalculationInstances,
            Dictionary<string, decimal> discountsList)
        {
            var feeModel = new FeeModel();
            var transactionAmount = merchantTransactions.Sum(x => x.transactionAmount);
            var merchantName = merchantTransactions.First().merchantName;

            //get fee amount
            foreach (IFeesCalculationRule instance in feesCalculationInstances)
            {
                var fee = instance.CalculateFee(transactionAmount);
                if (fee.feesAmount > 0)
                {
                    feeModel.feesAmount += fee.feesAmount;
                }

                if (fee.additionalFeesAmount > 0)
                {
                    feeModel.additionalFeesAmount += fee.additionalFeesAmount;
                }
            }

            var feeBeforeDiscount = feeModel.feesAmount;
            //get discount amount
            foreach (IDiscountCalculationRule instance in discountCalculationInstances)
            {
                feeModel.discountAmount += instance.CalculateDiscountForMerchant(feeBeforeDiscount, merchantName, discountsList);
            }

            return feeModel;
        }
    }
}

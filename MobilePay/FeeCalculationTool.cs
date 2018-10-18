using MobilePay.Controllers;
using MobilePay.Controllers.DiscountCalculationRules;
using MobilePay.Controllers.DiscountCalculationRules.DiscountsForMerchants;
using MobilePay.Controllers.FeesCalculationRules;
using MobilePay.Data_access;
using MobilePay.Helpers;
using MobilePay.Models.Errors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobilePay
{
    class FeeCalculationTool
    {
        static void Main(string[] args)
        {
            try
            {
                IEnumerable<IFeesCalculationRule> feesCalculationInstances = GetInstancesOfFeesCalculationRules.GetRulesInstances();
                IEnumerable<IDiscountCalculationRule> discountCalculationInstances = GetInstancesOfDiscountCalculationRules.GetRulesInstances();

                //get all transactions
                var transactions = TransactionRepository
                    .GetAllTransactions()
                    .OrderByDescending(x => x.date)
                    .ThenBy(x => x.merchantName)
                    .ToList();

                // get list of discounts for merchants
                Dictionary<string, decimal> discountsForMerchants = DiscountsForMerchants.GetDiscounts();

                while (transactions.Any())
                {
                    var firstTransaction = transactions.First();

                    //get transactions by merchant's name and month
                    var filteredTransactionsByFirstTransaction =
                        TransactionRepository.GetFilteredTransactionsByDateAndMerchant(firstTransaction.merchantName, firstTransaction.date);

                    //get fee model
                    var feeModel = CalculateFees.CalculateFee(
                        filteredTransactionsByFirstTransaction,
                        feesCalculationInstances,
                        discountCalculationInstances,
                        discountsForMerchants);

                    feeModel.merchantName = firstTransaction.merchantName;
                    feeModel.date = firstTransaction.date;

                    var feeWithDiscount = feeModel.feesAmount - feeModel.discountAmount + feeModel.additionalFeesAmount;

                    Console.WriteLine($"{feeModel.date.ToString("yyyy-MM"),10}" +
                        $"{feeModel.merchantName,15}" +
                        $"{string.Format("{0:0.00}", feeWithDiscount).Replace(',', '.'),10}");

                    // remove processed transactions from list
                    transactions = transactions.Except(filteredTransactionsByFirstTransaction).ToList();
                }
                Console.ReadLine();
            }
            catch (InvalidFileException)
            {
                Console.WriteLine("Error reading from file...");
                Console.ReadLine();

            }
            catch (Exception)
            {
                Console.WriteLine("Error...");
                Console.ReadLine();
            }
        }
    }
}

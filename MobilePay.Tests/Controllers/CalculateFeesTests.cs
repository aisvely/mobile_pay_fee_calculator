using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePay.Controllers.DiscountCalculationRules;
using MobilePay.Controllers.DiscountCalculationRules.DiscountsForMerchants;
using MobilePay.Controllers.FeesCalculationRules;
using MobilePay.Helpers;
using MobilePay.Models;
using System.Collections.Generic;
using System.Configuration;

namespace MobilePay.Controllers.Tests
{
    [TestClass()]
    public class CalculateFeesTests
    {
        IEnumerable<IFeesCalculationRule> feesCalculationInstances;
        IEnumerable<IDiscountCalculationRule> discountCalculationInstances;
        Dictionary<string, decimal> discountsForMerchants = new Dictionary<string, decimal>();

        [TestInitialize]
        public void TestInitialize()
        {
            ConfigurationManager.AppSettings["feesPercentageOfTransactionsAmount"] = "1";
            ConfigurationManager.AppSettings["additionalFeeAmount"] = "29";
            feesCalculationInstances = GetInstancesOfFeesCalculationRules.GetRulesInstances();
            discountCalculationInstances = GetInstancesOfDiscountCalculationRules.GetRulesInstances();
            discountsForMerchants.Add("TELIA", 10);
            discountsForMerchants.Add("CIRCLE_K", 10);
        }

        [TestMethod()]
        public void CalculateFeeTest_WithoutDiscount()
        {
            //Arange
            var merchantName = "7-ELEVEN";
            List<TransactionModel> transactions = new List<TransactionModel>()
            {
                new TransactionModel() { date = new System.DateTime(2018,06,02), merchantName = merchantName, transactionAmount =  120},
                new TransactionModel() { date = new System.DateTime(2018,06,03), merchantName = merchantName, transactionAmount =  200}

            };

            //Act
            var feeModel = CalculateFees.CalculateFee(transactions, feesCalculationInstances, discountCalculationInstances, discountsForMerchants);

            //Assert
            Assert.AreEqual(feeModel.feesAmount, 3.2m);
            Assert.AreEqual(feeModel.additionalFeesAmount, 29m);
            Assert.AreEqual(feeModel.discountAmount, 0m);
        }

        [TestMethod()]
        public void CalculateFeeTest_WithDiscount()
        {
            //Arange
            var merchantName = "TELIA";
            List<TransactionModel> transactions = new List<TransactionModel>()
            {
                new TransactionModel() { date = new System.DateTime(2018,10,02), merchantName = merchantName, transactionAmount =  300},
                new TransactionModel() { date = new System.DateTime(2018,10,03), merchantName = merchantName, transactionAmount =  150}

            };

            //Act
            var feeModel = CalculateFees.CalculateFee(transactions, feesCalculationInstances, discountCalculationInstances, discountsForMerchants);

            //Assert
            Assert.AreEqual(feeModel.feesAmount, 4.5m);
            Assert.AreEqual(feeModel.additionalFeesAmount, 29m);
            Assert.AreEqual(feeModel.discountAmount, 0.45m);
        }
    }
}
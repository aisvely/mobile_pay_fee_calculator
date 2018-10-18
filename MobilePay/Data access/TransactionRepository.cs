using MobilePay.Helpers;
using MobilePay.Models;
using MobilePay.Models.Errors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobilePay.Data_access
{
    public static class TransactionRepository
    {
        static List<TransactionModel> allTransactions { get; set; }

        public static List<TransactionModel> GetAllTransactions()
        {
            List<TransactionModel> transactionsList = new List<TransactionModel>();

            string filePath = ConstructInputFilePath.GetInputFilePath();
            string[] inputLines = ReadFromFile.GetAllTextLines(filePath);

            foreach (string line in inputLines)
            {
                string[] lineValues = ParseLineValues(line);

                transactionsList.Add(new TransactionModel()
                {
                    date = Convert.ToDateTime(lineValues[0]),
                    merchantName = lineValues[1],
                    transactionAmount = Convert.ToDecimal(lineValues[2])
                });
            }

            allTransactions = transactionsList;
            return transactionsList;
        }

        private static string[] ParseLineValues(string line)
        {
            string[] lineValues = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (lineValues.Length != 3)
            {
                lineValues = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (lineValues.Length != 3)
                {
                    throw InvalidFileException.InvalidFile();
                }
            }

            return lineValues;
        }

        public static List<TransactionModel> GetFilteredTransactionsByDateAndMerchant(string merchantName, DateTime transactionDate)
        {
            return allTransactions
                .Where(x => x.date.Year == transactionDate.Year &&
                x.date.Month == transactionDate.Month &&
                x.merchantName == merchantName).ToList();
        }
    }
}

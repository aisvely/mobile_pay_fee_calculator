using System;
using System.Configuration;

namespace MobilePay.Helpers
{
    public static class ConstructInputFilePath
    {
        /// <summary>
        /// Gets input file directory
        /// </summary>
        /// <returns>input file directory</returns>
        public static string GetInputFilePath()
        {
            var currenctDirectory = Environment.CurrentDirectory;
            return System.IO.Directory.GetParent(System.IO.Directory.GetParent(currenctDirectory).ToString())
                + "\\"
                + ConfigurationManager.AppSettings["inputFileFolderName"]
                + "\\"
                + ConfigurationManager.AppSettings["inputFileName"];
        }
    }
}

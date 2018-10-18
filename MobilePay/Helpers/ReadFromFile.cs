using System.Linq;

namespace MobilePay.Helpers
{
    public static class ReadFromFile
    {
        /// <summary>
        /// Gets all text lines from file
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns>text lines from file</returns>
        public static string[] GetAllTextLines(string path)
        {
            return System.IO.File
                .ReadAllLines(path)
                .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); ;

        }
    }
}

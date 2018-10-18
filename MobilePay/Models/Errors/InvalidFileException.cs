using System;

namespace MobilePay.Models.Errors
{
    public class InvalidFileException : Exception
    {
        public InvalidFileException()
        {
        }

        public InvalidFileException(string message)
            : base(message)
        {
        }

        public InvalidFileException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public static InvalidFileException InvalidFile()
        {
            return new InvalidFileException();
        }
    }
}

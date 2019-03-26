namespace Domain.Utilities
{
    using System;

    public static class Ensure
    {
        public static void IsNonNegativeInteger(int value, string argumentName)
        {
            if(value < 0)
            {
                throw new ArgumentException(
                    $"'{argumentName}' must be a non-negative integer.",
                    argumentName);
            }
        }

        public static void IsNotNullOrEmpty(string value, string argumentName)
        {
            IsNotNull(value, argumentName);

            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    $"'{argumentName}' must be a non-empty string.",
                    argumentName);
            }
        }

        public static void IsNotNull(object value, string argumentName)
        {
            if(value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IsPositiveInteger(int value, string argumentName)
        {
            if(value < 1)
            {
                throw new ArgumentException(
                    $"'{argumentName}' must be a positive integer.",
                    argumentName);
            }
        }
    }
}

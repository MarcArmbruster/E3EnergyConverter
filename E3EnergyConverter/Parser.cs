namespace E3EnergyConverter
{
    internal static class Parser
    {
        /// <summary>
        /// Parse a string value into a decimal value.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <returns>The decimal value; null if string is not convertable.</returns>
        internal static decimal? ToDecimal(string? input)
        {
            if (input == null)
            {
                return null;
            }

            if (decimal.TryParse(input, out var result))
            {
                return result;
            }

            return null;
        }
    }
}

namespace typeAttempt
{
    /// <summary>
    /// Used To Try To Convert To (x) Type
    /// </summary>
    public static class TypeAttempt
    {
        // No STR (string) check as it's feasible to put in main.cs when needed

        /// <summary>
        /// Try To Convert To INT
        /// </summary>
        /// <returns>true If Successful Conversion Attempt; false If Failed Conversion Attempt
        ///</returns>
        public static bool Int(dynamic input)
        {
            try
            {
                int.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Try To Convert To FLT
        /// </summary>
        /// <returns>true If Successful Conversion Attempt; false If Failed Conversion Attempt
        ///</returns>
        public static bool Float(dynamic input)
        {
            try
            {
                float.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

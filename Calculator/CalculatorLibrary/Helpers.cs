namespace CalculatorLibrary
{
    internal class Helpers
    {
        /// <summary>
        /// Validates number and return double type number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static double NumberValidation(string? num)
        {
            double validatedNumber = 0;
            while (!double.TryParse(num, out validatedNumber))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                num = Console.ReadLine();
            }
            return validatedNumber;
        }
        /// <summary>
        /// Validates number and returns int type number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int NumberValidation1(string? num)
        {
            int parsedNumber;
            while (!int.TryParse(num, out parsedNumber))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                num = Console.ReadLine();
            }

            return parsedNumber;
        }
    }
}

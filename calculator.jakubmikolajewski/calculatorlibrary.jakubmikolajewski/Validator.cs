namespace CalculatorLibrary
{
    public class Validator
    {
        public static double ValidateDoubleInput()
        {
            double input = 0.0d;
            string? readResult = Console.ReadLine();

            while (!double.TryParse(readResult, out input))
            {
                Console.WriteLine($"Please input a valid decimal number (e.g. 1,53). Your input was {readResult}.\n");
                readResult = Console.ReadLine();
            }
            return input;
        }
        public static string ValidateMenuChoice()
        {
            List<string> choiceList = new List<string> { "a", "s", "m", "d", "p", "r", "sin", "cos" };
            string? choice = Console.ReadLine();
            while (choice is null || !choiceList.Contains(choice.ToLower().Trim()))
            {
                Console.WriteLine($"Please choose a valid menu option (a, s, m, d, p, r, sin, cos).\n");
                choice = Console.ReadLine();
            }
            return choice.ToLower().Trim();
        }
        public static double ValidateDivisorNonZero(double number2)
        {
            while (number2 == 0)
            {
                Console.WriteLine("The divisor is 0. Please input a non-zero divisor: \n");
                number2 = Validator.ValidateDoubleInput();
            }
            return number2;
        }
        public static bool ValidateResultFinite(double result)
        {
            if (double.IsFinite(result))
                return true;
            else
                return false;
        }
    }
}
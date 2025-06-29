namespace CalculatorLibrary
{
    public class HelperMethods
    {
        public static double GetNumberInput(string prompt)
        {
            double number;
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
            }
            return number;
        }

        public static string GetOperatorInput()
        {
            string? op;
            string[] validOperators = { "a", "s", "m", "d", "sqrt", "pow", "10x", "sin", "cos", "tan" };

            do
            {
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tsqrt - Square Root");
                Console.WriteLine("\tpow - Power");
                Console.WriteLine("\t10x - 10^x");
                Console.WriteLine("\tsin - Sine");
                Console.WriteLine("\tcos - Cosine");
                Console.WriteLine("\ttan - Tangent");

                Console.Write("Your option? ");
                op = Console.ReadLine()?.Trim().ToLower();

                if (op == null || !validOperators.Contains(op))

                {
                    Console.WriteLine("Invalid input. Please choose a valid operator.");
                    op = null; 
                }

            } while (op == null);

            return op;
        }
    }
}

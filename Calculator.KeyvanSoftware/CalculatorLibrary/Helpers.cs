
namespace CalculatorLibrary
{
    public static class Helpers
    {
        private const string ValidOperators = "asmdrtx";
        private const string BinaryOperatorSymbols = "+-*/^";
        public const string BinaryOperatorLetters = "asmdt";

        private const string ReuseFirstOperand = "1";
        private const string ReuseSecondOperand = "2";
        private const string ReuseResult = "3";
        public static bool GetYesOrNo(string message)
        {
            while (true)
            {
                Console.WriteLine(message + " (Y/N)");
                string? answer = Console.ReadLine().ToUpper();

                if (answer == "Y") return true;
                if (answer == "N") return false;

                Console.Write("Please enter 'Y' or 'N'");
            }
        }

        public static double GetNumber(string message)
        {
            Console.Write(message);
            string? numInput = Console.ReadLine();

            double cleanNum = 0;
            while (string.IsNullOrWhiteSpace(numInput) || !double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput = Console.ReadLine();
            }
            return cleanNum;
        }

        public static int GetListNumber(string message, Calculator calculator)
        {
            int cleanNum;
            var length = calculator.PreviousCalculations.Count;
            
            Console.Write(message);
            string? numInput = Console.ReadLine();

            while (!int.TryParse(numInput, out cleanNum) || cleanNum <= 0 || cleanNum > length)
            {
                Console.Write("This is not valid input. Please enter a numeric value displayed in the list: ");
                numInput = Console.ReadLine();
            }
            return cleanNum;
        }

        public static string? GetOperator()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tt - Taking the Power");
            Console.WriteLine("\tx - 10x");
            Console.Write("Your option? ");

            string? op = Console.ReadLine()?.Trim().ToLower();

            while (op is null || !ValidOperators.Contains(op))
            {
                Console.WriteLine("Error: Unrecognized input. Choose an operator from the list.");
                op = Console.ReadLine();
            }
            return op;
        }

        public static string GetInput(string message, params string[] validOptions)
        {
            string? choice;
            do
            {
                Console.WriteLine(message);
                choice = Console.ReadLine();
            } while (!validOptions.Contains(choice));
            return choice;
        }

        public static double ReusePreviousCalculation(Calculator calculator)
        {
            var listNumber = GetListNumber("Which calculation would you like to use? Use list number to make a selection\n", calculator);
            var previous = calculator.PreviousCalculations[listNumber - 1];
            string? choice;
            string? operation = GetOperator();
            
            if (BinaryOperatorSymbols.Contains(previous.Operation))
            {
                if (BinaryOperatorLetters.Contains(operation))
                {
                    choice = GetInput("Would you like to reuse the same two operands (1) or the result (2)?", ReuseFirstOperand, ReuseSecondOperand);
                    return choice == ReuseFirstOperand
                        ? ReuseBothOperands(listNumber, operation, calculator, previous)
                        : ReuseResultWithNewOperand(listNumber, operation, calculator, previous);
                }
                else
                {
                    double result;
                    double num2 = 0;
                    choice = GetInput("Would you like to reuse the first operand (1), the second operand (2) or the result (3)?", ReuseFirstOperand, ReuseSecondOperand, ReuseResult);

                    return (choice) switch
                    {
                        ReuseFirstOperand => calculator.PerformOperationWithCheck(previous.FirstOperand, num2, operation),
                        ReuseSecondOperand => calculator.PerformOperationWithCheck(previous.SecondOperand, num2, operation),
                        ReuseResult => calculator.PerformOperationWithCheck(previous.Result, num2, operation),
                        _ => throw new InvalidOperationException("Invalid input")
                    };
                }
            }
            else
            {
                return ReuseUnary(listNumber, operation, calculator, previous);
            }
        }

        private static double ReuseUnary(int listNumber, string operation, Calculator calculator, Calculation previous)
        {
            double num2 = 0;
            string choice = GetInput("Would you like to reuse the operand (1) or the result (2)?", ReuseFirstOperand, ReuseSecondOperand);
            if (choice == ReuseFirstOperand)
            {
                if (BinaryOperatorLetters.Contains(operation))
                    num2 = GetNumber("Type the second number, then press Enter: ");
                return calculator.PerformOperationWithCheck(previous.FirstOperand, num2, operation);
            }
            return ReuseResultWithNewOperand(listNumber, operation, calculator, previous);
        }

        private static double ReuseBothOperands(int listNumber, string operation, Calculator calculator, Calculation previous)
        {
            return calculator.PerformOperationWithCheck(previous.FirstOperand, previous.SecondOperand, operation);
        }

        private static double ReuseResultWithNewOperand(int listNumber, string operation, Calculator calculator, Calculation previous)
        {
            double num2 = 0;
            if (BinaryOperatorLetters.Contains(operation))
                num2 = GetNumber("Type the second number, then press Enter: ");
            return calculator.PerformOperationWithCheck(previous.Result, num2, operation);
        }
    }
}

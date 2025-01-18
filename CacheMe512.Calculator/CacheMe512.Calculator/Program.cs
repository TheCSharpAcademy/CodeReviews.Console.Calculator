using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int usageCount = 0;
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();

            while (!endApp)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Perform a new calculation");
                Console.WriteLine("2. Use previous result as operand");
                Console.WriteLine("3. View previous calculations");
                Console.WriteLine("4. Clear memory");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter your choice (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PerformCalculation(calculator, ref usageCount);
                        break;

                    case "2":
                        UsePreviousResult(calculator, ref usageCount);
                        break;

                    case "3":
                        calculator.ViewCalculations();
                        break;

                    case "4":
                        calculator.ClearCalculatorMemory();
                        Console.WriteLine("\nMemory cleared!");
                        Console.WriteLine("\nPress Any Key to Continue.");
                        Console.ReadKey();
                        break;

                    case "5":
                        endApp = true;
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice. Please select a valid option.");
                        break;
                }
            }

            calculator.Finish();
        }

        private static void PerformCalculation(Calculator calculator, ref int usageCount)
        {
            double num1 = GetValidNumber("Type the first number: ");
            double num2 = GetValidNumber("Type the second number: ");

            string operation = GetOperation();
            double result = calculator.DoOperation(num1, num2, operation);
            if (double.IsNaN(result))
            {
                Console.WriteLine("[Error] This operation resulted in a mathematical error.");
            }
            else
            {
                Console.WriteLine($"Result: {result:0.##}\nTotal results calculated: {++usageCount}");
            }

            Console.WriteLine("\nPress Any Key to Continue.");
            Console.ReadKey();
        }

        private static void UsePreviousResult(Calculator calculator, ref int usageCount)
        {
            if (Memory.calculations.Count == 0)
            {
                Console.WriteLine("[Error] No previous calculations available.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a previous result to use as an operand:");
            for (int i = 0; i < Memory.calculations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Memory.calculations[i]}");
            }

            Console.Write("\nEnter the number corresponding to the result: ");
            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= Memory.calculations.Count)
            {
                double selectedResult = ExtractResultFromCalculation(Memory.calculations[selectedIndex - 1]);

                double num2 = GetValidNumber("Type the second number: ");
                string operation = GetOperation();

                double result = calculator.DoOperation(selectedResult, num2, operation);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("[Error] This operation resulted in a mathematical error.");
                }
                else
                {
                    Console.WriteLine($"Result: {result:0.##}\nTotal results calculated: {++usageCount}");
                }
            }
            else
            {
                Console.WriteLine("[Error] Invalid selection.");
            }

            Console.WriteLine("\nPress Any Key to Continue.");
            Console.ReadKey();
        }

        private static string GetOperation()
        {
            Console.WriteLine("Choose an operator:");
            Console.WriteLine("a - Add");
            Console.WriteLine("s - Subtract");
            Console.WriteLine("m - Multiply");
            Console.WriteLine("d - Divide");
            Console.Write("\nEnter your choice (a, s, m, d): ");
            string op = Console.ReadLine();
            return op.Substring(0, 1);
        }

        private static double GetValidNumber(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (double.TryParse(input, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("[Error] Invalid number! Please enter a valid number.");
                }
            }
        }

        private static double ExtractResultFromCalculation(string calculation)
        {
            string[] parts = calculation.Split('=');
            if (parts.Length > 1 && double.TryParse(parts[1].Trim(), out double result))
            {
                return result;
            }
            return double.NaN;
        }
    }
}

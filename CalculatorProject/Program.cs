using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProject
{
    partial class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new();

            while (!endApp)
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
                Console.WriteLine("\th - View Calculation History");
                Console.WriteLine("\tc - Clear Calculation History");

                Console.Write("Your option? ");
                string? op = Console.ReadLine()?.Trim().ToLower();


                if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|h|c|sqrt|pow|10x|sin|cos|tan)$"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        if (op == "h")
                        {
                            var getHistory = calculator.GetCalculationHistory();
                            if (getHistory.Count == 0)
                            {
                                Console.WriteLine("No calculations yet.");
                            }
                            else
                            {
                                Console.WriteLine("Calculation history:");
                                for (int i = 0; i < getHistory.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}: {getHistory[i]}");
                                }

                                Console.Write("Enter the index of a result to reuse, or type 'new' for a new calculation: ");
                                string? choice = Console.ReadLine()?.Trim();

                                if (int.TryParse(choice, out int index) && index >= 1 && index <= getHistory.Count)
                                {
                                    string selectedCalculation = getHistory[index - 1];
                                    string[] parts = selectedCalculation.Split('=');

                                    if (parts.Length == 2 && double.TryParse(parts[1].Trim(), out double reusedNum1))
                                    {
                                        Console.WriteLine($"Using result {reusedNum1} as the first operand.");


                                        string opFromUser = HelperMethods.GetOperatorInput();
                                        PerformCalculation(reusedNum1, calculator, opFromUser);
                                        continue;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid selection. Starting a new calculation.");
                                    }
                                }
                            }
                        }
                        else if (op == "c")
                        {
                            calculator.ClearCalculationHistory();
                            Console.WriteLine("Calculator history is cleared.");
                        }
                        else if (op == "a" || op == "s" || op == "m" || op == "d" || op == "sqrt" || op == "pow" || op == "10x" || op == "sin" || op == "cos" || op == "tan")
                        {
                            PerformCalculation(null, calculator, op);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Oh no! An exception occurred trying to do the math.\n - Details: {e.Message}");
                    }
                }

                Console.WriteLine("------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press Enter to continue: ");

                if (Console.ReadLine() == "n") endApp = true;

                int usageCount = calculator.GetUsageCount();
                Console.WriteLine($"You used the calculator {usageCount} time{(usageCount == 1 ? "" : "s")}");
                Console.WriteLine("\n");
            }

            calculator.Finish();
        }

        static void PerformCalculation(double? reusedNum1, Calculator calculator, string? initialOp = null)
        {
            double num1 = reusedNum1 ?? HelperMethods.GetNumberInput("Type a number, and then press Enter: ");
            string op = initialOp ?? HelperMethods.GetOperatorInput();

            bool isSingleOperand = op == "sqrt" || op == "10x" || op == "sin" || op == "cos" || op == "tan";

            double result;
            if (isSingleOperand)
            {
                result = calculator.DoOperationWithOneNum(num1, op);
            }
            else
            {
                double num2 = HelperMethods.GetNumberInput("Type another number, and then press Enter: ");

                if (op == "d" && num2 == 0)
                {
                    Console.WriteLine("Division by zero is not allowed.");
                    return;
                }

                result = calculator.DoOperationWithTwoNum(num1, num2, op);
            }

            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine($"Your result: {result:0.##}\n");
            }
        }
    }
}

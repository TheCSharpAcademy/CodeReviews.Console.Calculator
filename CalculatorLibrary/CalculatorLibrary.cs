using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;

        public static List<string> calculationList = new List<string>();
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public static void PrintWelcomeMessage()
        {
            Console.WriteLine("\t [V] View History");
            Console.WriteLine("\t [A] Add Numbers");
            Console.WriteLine("\t [S] Subtract Numbers");
            Console.WriteLine("\t [M] Multiply Numbers");
            Console.WriteLine("\t [D] Divide Numbers");
            Console.WriteLine("\t [pow] Exponentiate Numbers");
            Console.WriteLine("\t [sqrt] Square Root");
            Console.WriteLine("\t [sin] Sine");
            Console.WriteLine("\t [cos] Cosine");
            Console.WriteLine("\t [tan] Tangent");
            Console.WriteLine("For the square root and trigonometry operations, only one number will be used.\n");
            Console.WriteLine("What do you want to do?");
        }

        public double CalculateTwoNumbers(double num1, double num2, string operation)
        {
            double result = double.NaN;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (operation)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                case "pow":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                default:
                    Console.WriteLine("Invalid operation. Please try again.");
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public double CalculateOneNumber(double num, string operation)
        {
            double result = double.NaN;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num);
            writer.WritePropertyName("Operation");

            switch (operation)
            {
                case "sqrt":
                    if (num >= 0)
                    {
                        result = Math.Sqrt(num);
                        writer.WriteValue("SquareRoot");
                    }
                    else
                    {
                        Console.WriteLine("Cannot calculate the square root of a negative number.");
                    }
                    break;
                case "sin":
                    result = Math.Sin(num);
                    writer.WriteValue("Sin");
                    break;
                case "cos":
                    result = Math.Cos(num);
                    writer.WriteValue("Cos");
                    break;
                case "tan":
                    result = Math.Tan(num);
                    writer.WriteValue("Tan");
                    break;
                default:
                    Console.WriteLine("Invalid operation. Please try again.");
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public static string GetOperator(string choice)
        {
            return choice switch
            {
                "a" => "+",
                "s" => "-",
                "m" => "*",
                "d" => "/",
                "pow" => "^",
                "sqrt" => "√",
                "sin" => "sin",
                "cos" => "cos",
                "tan" => "tan",
            };
        }

        public static void PrintCalculation(double num1, double num2, string operationType, double result)
        {

            Console.WriteLine($"\t The result of {num1} {operationType} {num2} is: {result}\n");
        }

        public static void PrintAdvancedCalculation(double num, string operationType, double result)
        {

            Console.WriteLine($"\t The result of {operationType} {num} is: {result}\n");
        }

        public static string AddToCalculationList(string calculation)
        {
            calculationList.Add(calculation);
            return calculation;
        }

        public static double GetPreviousResult(List<double> previousResults)
        {
            Console.WriteLine("Type the index of the previous result:");

            for (int index = 1; index < previousResults.Count; index++)
            {
                double result = previousResults[index - 1];
                Console.WriteLine($"{index}: {result}");
            }

            var userChoice = Console.ReadLine();

            return previousResults[int.Parse(userChoice) - 1];
        }

        public static void PrintCalculationList()
        {
            Console.Clear();
            if (calculationList.Count == 0)
            {
                Console.WriteLine("No calculations have been performed yet.");
                return;
            }
            else
            {
                Console.WriteLine("----------------------------------------------------\n");
                Console.WriteLine("\t Calculation History:\n");
                foreach (var calculation in calculationList)
                {
                    Console.WriteLine($"\t {calculation}");
                }
            }

            DeleteCalculationList();
        }

        public static void DeleteCalculationList()
        {
            Console.WriteLine("\n----------------------------------------------------\n");
            Console.WriteLine("Would you like to clear the history? (y/n)");
            string? clearHistory = Console.ReadLine()?.Trim().ToLower();
            if (clearHistory == "y")
            {
                calculationList.Clear();
                Console.WriteLine("Calculation history cleared.");
            }
            else if (clearHistory == "n")
            {
                Console.WriteLine("Calculation history retained.");
            }
            else
            {
                Console.WriteLine("Invalid input. Calculation history retained.");
            }
        }

        public static double[] GetTwoNumbers()
        {
            string input1 = "";
            string input2 = "";
            var result = new double[2];

            Console.WriteLine("Enter your first number: ");
            input1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(input1, out cleanNum1))
            {
                Console.Write("Invalid input. Please enter a numeric value: ");
                input1 = Console.ReadLine();
            }

            Console.WriteLine("Enter your second number: ");
            input2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(input2, out cleanNum2))
            {
                Console.Write("Invalid input. Please enter a numeric value: ");
                input2 = Console.ReadLine();
            }

            result[0] = cleanNum1;
            result[1] = cleanNum2;
            return result;
        }

        public static double GetSingleNumber()
        {
            string input = "";
            double cleanNum = 0;

            Console.WriteLine("Enter your number: ");
            input = Console.ReadLine();

            while (!double.TryParse(input, out cleanNum))
            {
                Console.Write("Invalid input. Please enter a numeric value: ");
                input = Console.ReadLine();
            }
            return cleanNum;
        }

        // Closes the JSON object and array and outputs the log file to bin/debug.
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}

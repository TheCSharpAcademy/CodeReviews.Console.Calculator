using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;

        static List<string> calculationHistory = new List<string>();
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
        public double DoOperation(double num1, double num2, string op)
        {
            
            double result = double.NaN;
            
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    break;
                case "s":
                    result = num1 - num2;
                    break;
                case "m":
                    result = num1 * num2;
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        Console.WriteLine("Division by Zero is not allowed.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Operation.");
                    break;
            }
            string calculation = $"{num1}{op}{num2} = {result}";
            writer.WriteValue(op == "+" ? "Add" : op == "-" ? "Subtract" : op == "*" ? "Multiply" : "Divide");
            calculationHistory.Add(calculation);
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public static void ViewHistory()
        {
            if (calculationHistory.Count == 0)
            {
                Console.WriteLine("No calculations in history");
                return;
            }

            Console.WriteLine("\nCalculation History:");
            for (int i = 0; i < calculationHistory.Count; i++)
            {
                Console.WriteLine($"{i + 1}{calculationHistory[i]}");
            }
        }
        public static void DeleteFromHistory()
        {
            ViewHistory();

            if (calculationHistory.Count == 0)
                return;

            Console.Write("\nEnter the number of the calculation to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= calculationHistory.Count)
            {
                calculationHistory.RemoveAt(index - 1);
                Console.WriteLine("Calculation deleted.");
            }
            else
            {
                Console.WriteLine("Invalid input. No calculation deleted.");
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------");

            Console.WriteLine("Calculator Menu:");
            Console.WriteLine("1. Perform a new calculation");
            Console.WriteLine("2. Perform Trigonometry");
            Console.WriteLine("3. Calculate Ten Power");
            Console.WriteLine("4. Calculate To Power");
            Console.WriteLine("5. Calculate Square Root");
            Console.WriteLine("6. View calculation history");
            Console.WriteLine("7. Delete a calculation from history");
            Console.WriteLine("8. Reuse a calculation from history");
            Console.WriteLine("9. Exit");
            Console.Write("Choose an option: ");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    PerformCalculation();
                    break;
                case "2":
                    PerformTrigonometry();
                    break;
                case "3":
                    CalculateTenPowerX();
                    break;
                case "4":
                    CalculatePower();
                    break;
                case "5":
                    CalculateSquareRoot();
                    break;
                case "6":
                    ViewHistory();
                    break;
                case "7":
                    DeleteFromHistory();
                    break;
                case "8":
                    ReuseCalculation();
                    break;
                case "9":
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
        }

        public static void PerformCalculation()
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }

            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    Calculator calculator = new Calculator();
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n")
            {
                Console.WriteLine("Goodbye");
                Console.ReadKey();
                Environment.Exit(1);
            } 

            Console.WriteLine("\n");
        }

        private static void ReuseCalculation()
        {
            ViewHistory();

            if (calculationHistory.Count == 0)
                return;

            Console.Write("\nEnter the number of the calculation to reuse: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= calculationHistory.Count)
            {
                string selectedCalculation = calculationHistory[index - 1];
                Console.WriteLine($"Reusing: {selectedCalculation}");

                double result = double.Parse(selectedCalculation.Split('=')[1].Trim());

                Console.WriteLine("Enter an operator (+, -, *, /) to apply on the previous result:");
                string? op = Console.ReadLine();

                Console.WriteLine("Enter the number to use in the new calculation:");
                double num1 = Convert.ToDouble(Console.ReadLine());
                double num2 = Convert.ToDouble(Console.ReadLine());
            }
        }
        static void PerformTrigonometry()
        {
            Console.WriteLine("\nTrigonometry Functions:");
            Console.WriteLine("1. Sine (sin)");
            Console.WriteLine("2. Cosine (cos)");
            Console.WriteLine("3. Tangent (tan)");
            Console.Write("Choose a function: ");
            string? choice = Console.ReadLine();

            Console.WriteLine("Enter the angle in degrees:");
            double angle = Convert.ToDouble(Console.ReadLine());
            double radians = angle * (Math.PI / 180); 

            double result = 0;
            string function = "";

            switch (choice)
            {
                case "1":
                    result = Math.Sin(radians);
                    function = "sin";
                    break;
                case "2":
                    result = Math.Cos(radians);
                    function = "cos";
                    break;
                case "3":
                    result = Math.Tan(radians);
                    function = "tan";
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }

            string calculation = $"{function}({angle}°) = {result}";
            calculationHistory.Add(calculation);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("\nCalculation saved to history.");
        }
        static void CalculateTenPowerX()
        {
            Console.WriteLine("\nEnter the exponent (x) for 10^x:");
            double exponent = Convert.ToDouble(Console.ReadLine());

            double result = Math.Pow(10, exponent);

            string calculation = $"10 ^ {exponent} = {result}";
            calculationHistory.Add(calculation);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Calculation saved to history.");
        }
        static void CalculatePower()
        {
            Console.WriteLine("\nEnter the base number:");
            double baseNum = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the exponent:");
            double exponent = Convert.ToDouble(Console.ReadLine());

            double result = Math.Pow(baseNum, exponent);

            string calculation = $"{baseNum} ^ {exponent} = {result}";
            calculationHistory.Add(calculation);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Calculation saved to history.");
        }
        static void CalculateSquareRoot()
        {
            Console.WriteLine("\nEnter the number to find the square root of:");
            double num = Convert.ToDouble(Console.ReadLine());
            double result = Math.Sqrt(num);

            string calculation = $"√{num} = {result}";
            calculationHistory.Add(calculation);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Calculation saved to history.");
        }
    }
}
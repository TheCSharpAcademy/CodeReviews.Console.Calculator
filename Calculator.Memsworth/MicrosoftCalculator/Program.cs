using Newtonsoft.Json;

namespace MicrosoftCalculator
{
    static class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();
            bool endApp = false;

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                double cleanNum1;
                double cleanNum2;
                
                if (calculator.CalculationList.Any())
                {
                    cleanNum1 = Helper.GetInput("Type a number, and then press Enter: ", calculator.CalculationList);
                    cleanNum2 = Helper.GetInput("Type another number, and then press Enter: ", calculator.CalculationList);
                }

                else
                {
                    Console.Write("Type a number, and then press Enter: ");
                    cleanNum1 = Helper.CleanNum();
                    Console.Write("Type another number, and then press Enter: ");
                    cleanNum2 = Helper.CleanNum();
                }

                ShowMenu();

                string op = Console.ReadLine();

                try
                {
                    var result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        calculator.IncreaseCount();
                        calculator.CalculationList.Add(result);
                        Helper.ClearList(calculator.CalculationList);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }

            calculator.Finish();
            return;
        }

        private static void ShowMenu()
        {
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");
        }
    }

    public static class Helper
    {
        public static double CleanNum()
        {
            double cleanNum;
            while (!double.TryParse(Console.ReadLine(), out cleanNum))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
            }

            return cleanNum;
        }

        private static double GetListItem(string message, List<double> calculatorCalculationList)
        {
            int indexInput;
            Console.Write(message);
            foreach (var item in calculatorCalculationList)
            {
                Console.Write($"{item} -  ");
            }

            Console.Write("Enter index: ");
            while (!int.TryParse(Console.ReadLine(), out indexInput) &&
                   calculatorCalculationList.Contains(calculatorCalculationList[indexInput]))
            {
                Console.WriteLine("item doesn't exist in list. Enter index again");
            }

            return calculatorCalculationList[indexInput];
        }

        private static bool GetApproval() => (Console.ReadLine() == "y" || Console.ReadLine() == "Y");

        public static double GetInput(string message, List<double> list)
        {
            Console.WriteLine("Do you want to take from list (Y/y): ");
            if (GetApproval())
            {
                return GetListItem(message, list);
            }

            Console.Write(message);
            return CleanNum();
        }

        public static void ClearList(List<double> list)
        {
            Console.Write("\nDo you want to clear your list? (Y/y) ");
            if (GetApproval())
            {
                list.Clear();
            }
        }
    }

    public class Calculator
    {
        private JsonWriter writer;
        public List<double> CalculationList { get; private set; }
        public int CalculationCount { get; private set; } = 0;

        public Calculator()
        {
            CalculationList = new List<double>();
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public void IncreaseCount() => CalculationCount++;

        public double DoOperation(double num1, double num2, string op)
        {
            double
                result = double
                    .NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
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
                // Return text for an incorrect option entry.
                default:
                    break;
            }

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
    }
}
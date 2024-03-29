using System.Text.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        // Fields and Properties
        private readonly string mainTitle;
        private readonly string[] mainMenu;
        private readonly string infoMessage;
        private readonly string[] subMenu;
        private readonly string errorMessage;
        private int PerformedCalculations { get; set; }
        private List<string> ListOfCalculations { get; set; } = [];
        private List<double> ListOfResults { get; set; } = [];
        private readonly Utf8JsonWriter writer;
        private readonly string operand1;
        private readonly string operand2;

        // Class Constructor
        public Calculator()
        {
            mainTitle = "Console Calculator";
            mainMenu = [
                        "add",
                        "subtract",
                        "multiply",
                        "divide",
                        "square root",
                        "power of number",
                        "cosine of angle",
                        "view list of calculations",
                        "clear list of calculations",
                        "populate list of calculations",
                        "exit the program"
                       ];
            infoMessage = "Press any key to continue...";
            subMenu = [
                       "use result from last calculation",
                       "proceed to new calculation"
                       ];
            operand1 = "Operand1";
            operand2 = "Operand2";
            errorMessage = "Error. Unrecognized input.";
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new Utf8JsonWriter(logFile.BaseStream, new JsonWriterOptions { Indented = true });
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        // Starting point of calculator
        public void Run()
        {
            bool flag = true;
            while (flag)
            {
                int input = GetUserInput(mainTitle, mainMenu);
                switch (input)
                {
                    case 0:
                        Add();
                        break;
                    case 1:
                        Subtract();
                        break;
                    case 2:
                        Multiply();
                        break;
                    case 3:
                        Divide();
                        break;
                    case 4:
                        SquareRoot();
                        break;
                    case 5:
                        PowerOfNumber();
                        break;
                    case 6:
                        CosOfAngle();
                        break;
                    case 7:
                        DisplayCalculations();
                        break;
                    case 8:
                        ClearAppData();
                        break;
                    case 9:
                        PopulateAppData();
                        break;
                    case 10:
                        WriteMessages("Thank you for using our application!");
                        flag = false;
                        break;
                    default:
                        break;
                }
            }
            Dispose();
        }

        // Methods for reading and using user inputs
        private int GetUserInput(string title, string[] items)
        {

            int index = 0; // Default selection
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine();
                Console.WriteLine($"Performed Calculations: {PerformedCalculations}");
                Console.WriteLine();
                Console.WriteLine("Choose an option:");
                Console.WriteLine();

                for (int i = 0; i < items.Length; i++)
                {
                    if (i == index)
                        Console.Write("=> ");
                    else
                        Console.Write("   ");

                    Console.WriteLine(items[i]);
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        index = Math.Max(0, index - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        index = Math.Min(items.Length - 1, index + 1);
                        break;
                    case ConsoleKey.Enter:
                        flag = false;
                        break;
                }
            }
            return index;
        }

        private double GetDoubleNum(string operation, string operand)
        {
            WriteHeader(operation);
            double num;
            if (HasUseLastResultSelected(operation))
            {
                int lastIndex = ListOfResults.Count - 1;
                num = ListOfResults[lastIndex];
                WriteMessages($"Value {num:F2} for {operand} will be used in a calculation");
            }
            else
            {
                WriteHeader(operation, operand);
                Console.Write("Input number and then press Enter: ");
                string input = Console.ReadLine();
                while (!double.TryParse(input, out num))
                {
                    Console.WriteLine(errorMessage);
                    Console.Write("Input: ");
                    input = Console.ReadLine();
                }
                WriteMessages($"Value {num:F2} for {operand} will be used in a calculation");
            }
            return num;
        }
        private bool HasUseLastResultSelected(string operation)
        {
            if (ListOfResults.Count > 0)
            {
                int index = GetUserInput(operation, subMenu);
                if (index == 0) return true;
            }

            return false;
        }

        // Methods to print something to console
        private void WriteMessages(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine(infoMessage);
            Console.ReadKey();
        }
        private void WriteHeader(string operation, string operand = "")
        {
            Console.Clear();
            Console.WriteLine(operation);
            Console.WriteLine(operand);
        }

        // Methods to write to log file, dispose writer and update app data
        private void Dispose()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Dispose();
        }
        private void WriteToLogFile(double num1, double num2, string operation, double result)
        {
            writer.WriteStartObject();
            writer.WriteString(operand1, num1.ToString());
            writer.WriteString(operand2, num2.ToString());
            writer.WriteString("Operation", operation);
            writer.WriteString("Result", result.ToString());
            writer.WriteEndObject();
        }

        private void WriteToLogFile(double num1, string operation, double result)
        {
            writer.WriteStartObject();
            writer.WriteString(operand1, num1.ToString());
            writer.WriteString("Operation", operation);
            writer.WriteString("Result", result.ToString());
            writer.WriteEndObject();
        }
        private void UpdateAppData(string str, double n)
        {
            PerformedCalculations += 1;
            ListOfCalculations.Add(str);
            ListOfResults.Add(n);
        }

        // Methods to perform calculations
        private void Add()
        {
            string operation = "Add";
            double num1 = GetDoubleNum(operation, operand1);
            double num2 = GetDoubleNum(operation, operand2);
            double result = num1 + num2;
            string temp = $"{num1:F2} + {num2:F2} = {result:F2}";
            WriteMessages($"Output: {temp}");
            UpdateAppData(temp, result);
            WriteToLogFile(num1, num2, operation, result);
        }
        private void Subtract()
        {
            string operation = "Subtract";
            double num1 = GetDoubleNum(operation, operand1);
            double num2 = GetDoubleNum(operation, operand2);
            double result = num1 - num2;
            string temp = $"{num1:F2} - {num2:F2} = {result:F2}";
            WriteMessages($"Output: {temp}");
            UpdateAppData(temp, result);
            WriteToLogFile(num1, num2, operation, result);
        }

        private void Multiply()
        {
            string operation = "Multiply";
            double num1 = GetDoubleNum(operation, operand1);
            double num2 = GetDoubleNum(operation, operand2);
            double result = num1 * num2;
            string temp = $"{num1:F2} * {num2:F2} = {result:F2}";
            WriteMessages($"Output: {temp}");
            UpdateAppData(temp, result);
            WriteToLogFile(num1, num2, operation, result);
        }

        private void Divide()
        {
            string operation = "Divide";
            double num1 = GetDoubleNum(operation, operand1);
            double num2 = GetDoubleNum(operation, operand2);
            while (num2 == 0)
            {
                WriteMessages("Can't divide by 0.");
                num2 = GetDoubleNum(operation, operand2);
            }
            double result = num1 / num2;
            string temp = $"{num1:F2} / {num2:F2} = {result:F2}";
            WriteMessages($"Output: {temp}");
            UpdateAppData(temp, result);
            WriteToLogFile(num1, num2, operation, result);
        }

        private void SquareRoot()
        {
            string operation = "SquareRoot";
            double num = GetDoubleNum(operation, operand1);
            double result = Math.Sqrt(Math.Abs(num));
            string temp = $"Square root of {num:F2} = {result:F2}";
            WriteMessages($"Output: {temp}");
            UpdateAppData(temp, result);
            WriteToLogFile(num, operation, result);
        }

        private void PowerOfNumber()
        {
            string operation = "PowerOfNumber";
            double num1 = GetDoubleNum(operation, operand1);
            double num2 = GetDoubleNum(operation, operand2);
            double result = Math.Pow(num1, num2);
            string temp = $"{num1:F2} raised to the {num2:F2} = {result:F2}";
            WriteMessages($"Output: {temp}");
            UpdateAppData(temp, result);
            WriteToLogFile(num1, num2, operation, result);
        }

        private void CosOfAngle()
        {
            string operation = "CosineOfAngle";
            double num = GetDoubleNum(operation, operand1);
            double result = Math.Cos(Math.PI * num / 180);
            string temp = $"Cosine of an angle {num:F2} degrees = {result:F2}";
            WriteMessages($"Output: {temp}");
            UpdateAppData(temp, result);
            WriteToLogFile(num, operation, result);
        }

        // Methods to work with Lists of Calculations
        private void DisplayCalculations()
        {
            WriteMessages("List of calculations");
            if (ListOfCalculations.Count > 0)
            {
                foreach (string calc in ListOfCalculations)
                {
                    Console.WriteLine(calc);
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            else WriteMessages("Oops. App has not performed any operations yet.");
        }

        private void ClearAppData()
        {
            ListOfCalculations.Clear();
            ListOfResults.Clear();
            WriteMessages("All data was cleared.");
            PerformedCalculations = 0;
        }
        private void PopulateAppData()
        {
            ListOfCalculations = [
                "1 + 2 = 3",
                "4 - 3 = 1",
                "2 * 2 = 2",
                "6 / 2 = 3",
                "Square root of 81 = 9",
                "2 raised to the 2 = 4",
                "cosine of an angle 45 degrees = 0.75",
                ];
            ListOfResults = [3, 1, 2, 3, 9, 4, 0.75];
            PerformedCalculations = ListOfResults.Count;
        }
    }
}

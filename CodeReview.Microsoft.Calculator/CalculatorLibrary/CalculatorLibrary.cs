using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private const string FilePath = "calculatorlog.json";
        private static int _usageCount;
        private CalculationsList _calculationsList = new CalculationsList();
        private Calculation _currentCalculation = new Calculation();

        public double DoOperation(double num1, double num2, string op)
        {
            LoadData();
            double result = double.NaN;
            string operationName = "Unknown";

            // Perform the operation
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    operationName = "Add";
                    break;
                case "s":
                    result = num1 - num2;
                    operationName = "Subtract";
                    break;
                case "m":
                    result = num1 * num2;
                    operationName = "Multiply";
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        operationName = "Divide";
                    }
                    break;
                case "sqrt":
                    result = Math.Sqrt(num1);
                    operationName = "Square Root";
                    break;
                case "pow":
                    result = Math.Pow(num1, num2);
                    operationName = "Taking the power";
                    break;
                case "10x":
                    result = num1 * 10;
                    operationName = "10x";
                    break;
                case "sin":
                    result = Math.Sin(DegreeToRadian(num1));
                    operationName = "sin";
                    break;
                case "cos":
                    result = Math.Cos(DegreeToRadian(num1));
                    operationName = "cos";
                    break;
                case "tan":
                    result = Math.Tan(DegreeToRadian(num1));
                    operationName = "tan";
                    break;

            }

            // Add operation to the list
            _usageCount++;
            _currentCalculation = new Calculation
            {
                Operand1 = num1,
                Operand2 = num2,
                Operation = operationName,
                Result = result,
                Counter = _usageCount
            };
            _calculationsList.AddOperation(_currentCalculation);

            // Save the updated list to the file
            SaveCalculations();

            return result;
        }

        private void SaveCalculations()
        {
            string json = JsonConvert.SerializeObject(_calculationsList, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public void PrintCalculations()
        {
            LoadData();
            if (_calculationsList == null || !_calculationsList.Operations.Any())
            {
                Console.WriteLine("No calculations available to display.");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                foreach (var operation in _calculationsList.Operations)
                {
                    //Console.WriteLine($"Operand1: {operation.Operand1}, Operand2: {operation.Operand2}, Operation: {operation.Operation}, Result: {operation.Result}, Counter: {operation.Counter}");
                    Console.WriteLine(
                        $"Counter: {operation.Counter,5}\t" +
                        $"Operand1: {operation.Operand1,-10}\t" +
                        $"Operand2: {operation.Operand2,-10}\t" +
                        $"Operation: {operation.Operation,-10}\t" +
                        $"Result: {operation.Result,10:F2}\t"
                        );
                }
                Console.ReadKey();
            }
        }

        public void LoadData()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    string json = File.ReadAllText(FilePath);
                    _calculationsList = JsonConvert.DeserializeObject<CalculationsList>(json) ?? new CalculationsList();
                    _usageCount = _calculationsList.Operations.Count;
                }
                catch (JsonException)
                {
                    Console.WriteLine("Error: The file contains invalid data. Please check or delete the file.");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    Console.ReadKey();
                }
            }
            else
            {
                SaveCalculations();
                Console.WriteLine("No file found... Creating empty file.");
                Console.ReadKey();
            }
        }

        public void StartCalculator()
        {
            bool endApp = false;
            Calculator calculator = new Calculator();

            // Display title
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                bool useLatestResult = false;
                double cleanNum1 = 0;
                double cleanNum2 = 0;

                Console.WriteLine("Use latest result? Y/N");
                string input = Console.ReadLine().ToUpper().Trim();
                switch (input)
                {
                    case "Y":
                        useLatestResult = true;
                        break;

                    case "N":
                        useLatestResult = false;
                        break;
                }

                if (useLatestResult)
                {
                    LoadData();
                    try
                    {
                        var latestOperation = _calculationsList.Operations.Last();
                        cleanNum1 = latestOperation.Result;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error retrieving the latest operation: {ex.Message}");
                        Console.ReadKey();
                        cleanNum1 = PromptForNumber("Type the first number and then press Enter: ");
                    }
                }
                else
                {
                    cleanNum1 = PromptForNumber("Type the first number and then press Enter: ");
                }

                cleanNum2 = PromptForNumber("Type the second number or leave empty if not needed, and then press Enter: ");

                Console.WriteLine("Choose an option:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tsqrt - Square root");
                Console.WriteLine("\tpow - Take the power");
                Console.WriteLine("\t10x - multiply by 10");
                Console.WriteLine("\tsin - Sine of the angle (in degrees)");
                Console.WriteLine("\tcos - Cosine of the angle (in degrees)");
                Console.WriteLine("\ttan - Tangent of the angle (in degrees)");

                Console.Write("Your option? ");
                string op = Console.ReadLine();

                try
                {
                    double result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    Console.WriteLine($"Your result: {result:0.##}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.WriteLine("------------------------\n");
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }


        }
        private static double PromptForNumber(string promptMessage)
        {
            double cleanNum;
            Console.Write(promptMessage);
            string numInput = Console.ReadLine();

            if (string.IsNullOrEmpty(numInput))
            {
                numInput = "0"; // Default to "0" if the input is empty or null
            }

            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput = Console.ReadLine();
            }

            return cleanNum;
        }

        public void Delete()
        {
            File.Delete(FilePath);
        }
        static double DegreeToRadian(double degree)
        {
            return degree * (Math.PI / 180);
        }
    }

}
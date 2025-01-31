using Newtonsoft.Json;
namespace CalculatorLibrary
{
    public class Calculator
    {
        private int _numberOfUses;
        private JsonWriter _writer;
        private List<string> _calculationHistory;
        private List<double> _calculationHistoryResults;
        public bool GettingResultFromHistory { get; set; }
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            _writer = new JsonTextWriter(logFile);
            _writer.Formatting = Formatting.Indented;
            InitializeJSONLog();
            _numberOfUses = GetNumberOfUses();
            _calculationHistory = new List<string>();
            _calculationHistoryResults = new List<double>();
            GettingResultFromHistory = false;

        }
        
        private int GetNumberOfUses()
        {
            StreamReader readNumberOfUses = new StreamReader("NumberOfUses.txt");
            int numberOfUses = Convert.ToInt32(readNumberOfUses.ReadLine());
            readNumberOfUses.Close();
            return numberOfUses;
        }
        private void WriteNumberOfUses(int numberOfUses)
        {
            StreamWriter writeNumberOfUses = new StreamWriter("NumberOfUses.txt");
            writeNumberOfUses.Write(numberOfUses);
            writeNumberOfUses.Close();
        }
        private void InitializeJSONLog()
        {
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();
        }

        private void AddOperandsToJSON(double num1, double num2)
        {
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(num2);
            _writer.WritePropertyName("Operation");
        }
        private void AddOperandsToJSON(double num1)
        {
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);
            _writer.WritePropertyName("Operation");
        }
        private void AddResultToJSON(double result)
        {
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
        }
        public double Calculate(string operation) {
            double result = double.NaN;
            double num1;
            double num2;

            switch (operation) {
                case "a": // addition
                case "s": // substraction
                case "m": // multiplication
                case "d": //division
                    num1 = GetNumber(1);
                    num2 = GetNumber(2);
                    result = BasicOperation(num1, num2, operation);
                    break;
                case "p": // power
                    num1 = GetNumber(1);
                    num2 = GetNumber(2);
                    result = Power(num1, num2);
                    break;
                case "sr": // square root
                    num1 = GetNumber(1);
                    result = SquareRoot(num1);
                    break;
                case "10x":
                    num1 = GetNumber(1);
                    result = TenTimes(num1);
                    break;
                case "sin":
                case "cos":
                case "tg":
                    num1 = GetNumber(1);
                    result = Trigonometry(num1, operation);
                    break;
            }
            return result;
        }
        public double TenTimes(double number)
        {
            double result = double.NaN;
            result = 10 * number;
            AddOperandsToJSON(number);
            _writer.WriteValue("10x");
            _calculationHistory.Add($"10 x {number} = {result}");
            _calculationHistoryResults.Add(result);
            AddResultToJSON(result);
            _numberOfUses++;
            return result;
        }
        public double Power(double number, double power)
        {
            double result = double.NaN;
            result = Math.Pow(number, power);
            AddOperandsToJSON(number, power);
            _writer.WriteValue("Power");
            _calculationHistory.Add($"{number} ^ {power} = {result}");
            _calculationHistoryResults.Add(result);
            AddResultToJSON(result);
            _numberOfUses++;

            return result;
        }
        public double SquareRoot(double number)
        {
            double result = double.NaN;
            result = Math.Sqrt(number);
            AddOperandsToJSON(number);
            _writer.WriteValue("Square Root");
            _calculationHistory.Add($"√{number} = ±{result}");
            _calculationHistoryResults.Add(result);
            AddResultToJSON(result);
            _numberOfUses++;
            return result;
        }
        public double Trigonometry(double number, string function)
        {
            double result = double.NaN;
            AddOperandsToJSON(number);
            switch (function) 
            {
                case "sin":
                    result = Math.Sin(number);
                    _writer.WriteValue("Sinus");
                    _calculationHistory.Add($"Sin({number}) = {result}");
                    break;
                case "cos":
                    result = Math.Cos(number);
                    _writer.WriteValue("Cosinus");
                    _calculationHistory.Add($"Cos({number}) = {result}"); 
                    break;
                case "tg":
                    result = Math.Tan(number);
                    _writer.WriteValue("Tangent");
                    _calculationHistory.Add($"Tan({number}) = {result}"); 
                    break;
            }
            _calculationHistoryResults.Add(result);
            AddResultToJSON(result);
            _numberOfUses++;
            return result;
        }
        public double BasicOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            AddOperandsToJSON(num1, num2);
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    _writer.WriteValue("Add");
                    _calculationHistory.Add($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    _writer.WriteValue("Subtract");
                    _calculationHistory.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    _writer.WriteValue("Multiply");
                    _calculationHistory.Add($"{num1} * {num2} = {result}");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    _calculationHistory.Add($"{num1} / {num2} = {result}");
                    _writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            _calculationHistoryResults.Add(result); 
            AddResultToJSON(result);
            _numberOfUses++;
            return result;
        }
        
        public void Finish()
        {
            _writer.WriteEndArray();
            _writer.WriteEndObject();
            _writer.Close();
            WriteNumberOfUses(_numberOfUses);
        }
        
        public void OptionsMenu()
        {
            PrintOptionsMenu();
            ProcessOptionMenu();
        }
        
        private int GetAndValidateMenuInput()
        {
            string? selection;
            int numericSelection;
            Console.Write("Please enter your choice: ");
            selection = Console.ReadLine();

            while (int.TryParse(selection, out numericSelection) == false || (numericSelection < 1 || numericSelection > 3))
            {
                Console.Write("Please enter valid choice: ");
                selection = Console.ReadLine();
            }


            return numericSelection;

        }

        private void ProcessOptionMenu()
        {
            bool wantToStayInMenu = true;
            
            while (wantToStayInMenu) 
            {
                int selection = GetAndValidateMenuInput();

                switch (selection)
                {
                    case 1:
                        PrintHistory();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        PrintOptionsMenu();
                        break;
                    case 2:
                        _calculationHistory.Clear();
                        _calculationHistoryResults.Clear();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        PrintOptionsMenu();
                        break;
                    case 3:
                        wantToStayInMenu = false;
                        break;

                }
                
            }
            
        }
        public double GetNumber(int position)
        {
            if (GettingResultFromHistory)
            {
                return GetNumberFromResult();
            }
            else
            {
                return GetNewNumber(position);
            }
        }
        private double GetNewNumber(int position)
        {
            
            string? numInput;
            double cleanNum = 0;

            if (position == 1)
            {
                Console.Write("Enter fisrt number folowed by an [Enter] key: ");
            }
            else if (position == 2)
            {
                Console.Write("Enter second number folowed by an [Enter] key: ");
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid number position passed to GetNumber()");
            }

            numInput = Console.ReadLine();
            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput = Console.ReadLine();
            }

            return cleanNum;
        }
        private double GetNumberFromResult()
        {
            string? numInput;
            int index;

            Console.Write("Enter index number of result you'd like to use: ");
            numInput = Console.ReadLine();
            while (!int.TryParse(numInput, out index) || index-1 < 0 || index-1 > _calculationHistoryResults.Count)
            {
                Console.Write("This is not valid input. Please enter a valid numeric value: ");
                numInput = Console.ReadLine();
            }
            Console.WriteLine("First Number for calculation: " + _calculationHistoryResults[index - 1]);

            GettingResultFromHistory = false;
            return _calculationHistoryResults[index-1];
        }
        public void PrintInputMenu()
        {
            Console.WriteLine("Choose an operation from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tsr - Square Root");
            Console.WriteLine("\t10x - Multiply by 10");
            Console.WriteLine("\tsin - Sinus");
            Console.WriteLine("\tcos - Cosinus");
            Console.WriteLine("\ttg - tangent ");
            Console.Write("Your input:");
        }
        public void PrintHistory()
        {
            Console.WriteLine("Calculation history:");
            if (_calculationHistory == null || _calculationHistory.Count == 0)
            {
                Console.WriteLine("No records in history");
            }
            else
            {
                for (int i = 0; i < _calculationHistory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {_calculationHistory[i]}");
                }

            }
            Console.WriteLine("------------------------");
            Console.WriteLine();
        }
        public void PrintTitle()
        {
            Console.WriteLine("Welsome to Console Calculator in C#\r");
            Console.WriteLine($"Calculator was used to solve {_numberOfUses} problems so far");
            Console.WriteLine("------------------------\n");
        }
        private void PrintOptionsMenu()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            PrintTitle();
            Console.WriteLine("Press coresponding number and [Enter] for selection: ");
            Console.WriteLine("1. Show History");
            Console.WriteLine("2. Delete History");
            Console.WriteLine("3. Return to calculations");
            Console.WriteLine("------------------------");
        }
    }
}

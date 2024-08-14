using Newtonsoft.Json;
namespace CalculatorLibrary
{
    public class Calculator : BaseOperations
    {
        private int _usageCounter;
        private JsonWriter _writer;
        private bool _isWriterInitialized = false;

        public Calculator() => InitializeWriter();

        /********* trigonometric functions *********/
        public double CalculatePowerOfTen(double power)
        {
            return Math.Pow(10, power);
        }

        public double CalculateSquareRoot(double a)
        {
            return Math.Sqrt(a);
        }

        public double CalculateTangent(double a)
        {
            return Math.Tan(a);
        }

        public double CalculateSine(double a)
        {
            return Math.Sin(a);
        }

        public double CalculateCosine(double a)
        {
            return Math.Cos(a);
        }
        /********* ------------------ *********/

        private string GetCurrentPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory.Replace("/bin/Debug/net7.0", "");
        }

        private string GetFilePath()
        {
            string path = Path.Combine(GetCurrentPath(), "calculatorlog.json");
            return path;
        }

        private void InitializeWriter()
        {
            if (_isWriterInitialized)
            {
                _writer.Close();
            }

            StreamWriter logFile = File.CreateText($"{GetFilePath()}");
            logFile.AutoFlush = true;
            _writer = new JsonTextWriter(logFile)
            {
                Formatting = Formatting.Indented
            };
            _writer.WriteStartObject();
            CreateOperationRecords();
            _isWriterInitialized = true;
        }

        private void ReinitializeWriterIfNeeded()
        {
            if (!_isWriterInitialized)
            {
                InitializeWriter();
            }
        }
        private string GetExtraCalucationOption()
        {
            Console.Write("Extra Calculation Y/N: ");
            string? answer = Console.ReadLine()?.ToLower();
            if (answer == "y")
            {
                if (answer.Equals("y"))
                {
                    Console.WriteLine("------------------------ Extra Calculation ------------------------");
                    Console.WriteLine("\tx - 10x");
                    Console.WriteLine("\tr - Square Root");
                    Console.WriteLine("------------------------ trigonometry -----------------------------");
                    Console.WriteLine("\tc - Cosine");
                    Console.WriteLine("\ts - Sin");
                    Console.WriteLine("\tt - Tangente");
                    Console.Write("Your option? ");
                    return Console.ReadLine() ?? "";
                }
            }
            return "";
        }
        public double AskCalculationQuestion(double n)
        {
            ReinitializeWriterIfNeeded();
            string? option = GetExtraCalucationOption();
            if (!string.IsNullOrEmpty(option))
            {
                return option switch
                {
                    "x" => CalculatePowerOfTen(n),
                    "r" => CalculateSquareRoot(n),
                    "c" => CalculateCosine(n),
                    "s" => CalculateSine(n),
                    "t" => CalculateTangent(n),
                    _ => double.NaN,
                };
            }
            return double.NaN;
        }

        public double AskUserForNumber(string message)
        {
            Console.Write(message);
            try
            {
                string? userInput = Console.ReadLine();
                double result = AskCalculationQuestion(Convert.ToDouble(userInput));
                if (!double.IsNaN(result))
                {
                    Console.WriteLine($"The number is changed to {result}");
                    return result;
                }
                return Convert.ToDouble(userInput);
            }
            catch
            {
                throw new FormatException("Invalid number: Ensure to enter a valid number");
            }
        }

        public int UsageCounter => _usageCounter;

        public void CountUsage()
        {
            _usageCounter++;
        }

        protected override string GetOperationName(string oper)
        {
            return oper.ToLower() switch
            {
                "*" => "Multiply",
                "-" => "Subtract",
                "/" => "Divide",
                "+" => "Add",
                _ => throw new InvalidOperationException("\nInvalid operation. Try again, please"),
            };
        }

        private double DivisionWithValidation(double num1, double num2)
        {
            while (num2 == 0)
            {
                num2 = AskUserForNumber("Enter a non-zero divisor: ");
            }
            return Division(num1, num2);
        }

        public double Calculate(double num1, double num2, string operation)
        {
            ReinitializeWriterIfNeeded();
            string oper = GetOperation(operation);
            double result = operation.ToLower() switch
            {
                "a" => Addition(num1, num2),
                "s" => Substraction(num1, num2),
                "m" => Multiplication(num1, num2),
                "d" => DivisionWithValidation(num1, num2),
                _ => throw new InvalidOperationException("Operation is not supported")
            };
            RecordOperation(num1, num2, oper, result);
            return result;
        }

        public void DeleteJsonFile()
        {
            string filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
                Console.WriteLine("The file has been deleted successfully");
                Console.WriteLine("Press Enter to reinitialize the file.");
                Console.ReadLine();
                InitializeWriter();
            }
        }

        private void CreateOperationRecords()
        {
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();
        }

        private void SetRecords(double a, double b, string operation)
        {
            if (_writer.WriteState != WriteState.Array)
            {
                throw new InvalidOperationException("SetRecords can only be called within an array context.");
            }
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(a);
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(b);
            _writer.WritePropertyName("Operation");
            _writer.WriteValue(GetOperationName(operation));
        }

        private void DisplayResult(double num1, double num2, string oper, double result)
        {
            Console.WriteLine($"------------------------ Operation Result ------------------------");
            Console.WriteLine($"Your result: {num1} {oper} {num2} = {result}");
            Console.WriteLine("-------------------------------------------------------------------");
        }

        private void SaveResult(double result)
        {
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
        }

        private void RecordOperation(double num1, double num2, string oper, double result)
        {
            SetRecords(num1, num2, oper);
            DisplayResult(num1, num2, oper, result);
            SaveResult(result);
            _writer.WriteEndObject();
        }

        public void CloseRecord()
        {
            if (_isWriterInitialized)
            {
                _writer.WriteEndArray();
                _writer.WriteEndObject();
                _writer.Close();
                _isWriterInitialized = false;
            }
        }
    }
}

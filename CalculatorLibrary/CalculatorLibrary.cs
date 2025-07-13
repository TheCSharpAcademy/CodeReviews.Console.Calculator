namespace CalculatorLibrary
{
    public class Calculator
    {
        private readonly Logger _logger;
        public Calculator(Logger logger)
        {
            _logger = logger;
        }
        public struct Calculation
        {
            public double Num1 { get; set; }
            public double? Num2 { get; set; }
            public double Result { get; set; }
            public string Operation { get; set; }
        }

        Calculation[] _calculationHistory = new Calculation[10];
        int _historyCount;
        public void AddToHistory(Calculation newCalculation)
        {
            if (_historyCount < _calculationHistory.Length)
            {
                _calculationHistory[_historyCount] = newCalculation;
                _historyCount++;
            }
            else
            {
                // we start from the first (1) index rather than 0 to shift everything appropriately
                for (int i = 1; i < _calculationHistory.Length; i++)
                {
                    _calculationHistory[i - 1] = _calculationHistory[i];
                }

                _calculationHistory[_calculationHistory.Length - 1] = newCalculation;
            }
        }

        public bool IsHistoryEmpty()
        {
            return _historyCount == 0;
        }

        public string GetOperand(string op)
        {
            ////a|s|m|d|sqrt|pow|10x|sin|cos|tan
            switch (op)
            {
                case "a": return "+";
                case "s": return "-";
                case "m": return "*";
                case "d": return "/";
                case "sqrt": return "√";
                case "pow": return "^";
                case "l": return "Log";
                case "sin": return "Sin";
                case "cos": return "Cos";
                case "tan": return "Tan";
                default:
                    return "";
            }
        }

        public void ShowHistory()
        {
            for (int i = 0; i < _historyCount; i++)
            {
                Calculation tempCalc = _calculationHistory[i];
                Console.WriteLine($"{i + 1}. {GetFormattedString(tempCalc)}");
            }
        }

        public bool GetCalculation(int index, out Calculation calc)
        {
            if (index >= 0 && index < _historyCount)
            {
                calc = _calculationHistory[index];
                return true;
            }
            else
            {
                calc = default;
                return false;
            }
        }

        public string GetFormattedString(Calculation calc)
        {
            string ret = "";
            if (calc.Num2 != null)
            {
                ret = $"{calc.Num1}{GetOperand(calc.Operation)}{calc.Num2} = {calc.Result}";
            }
            else
            {
                ret = $"{GetOperand(calc.Operation)}({calc.Num1}) = {calc.Result}";
            }

            return ret;
        }

        public void ParseOption(string? op, out bool usingCalculator, out bool endApp, out bool reuseCalculation)
        {
            usingCalculator = false;
            endApp = false;
            reuseCalculation = false;
            // Validate input is not null, and matches the pattern
            var validOptions = new HashSet<string> { "e", "c", "h", "dh", "r" };
            while (op == null || !validOptions.Contains(op))
            {
                Console.WriteLine("Error: Unrecognized input.");
                op = Console.ReadLine();
            }

            switch (op)
            {
                case "h":
                    if (_historyCount == 0)
                    {
                        Console.WriteLine("History is currently empty. Perform some calculations first.");
                    }
                    else
                    {
                        ShowHistory();
                    }
                    break;
                case "c":
                    usingCalculator = true;
                    break;
                case "dh":
                    _calculationHistory = new Calculation[10];
                    _historyCount = 0;
                    Console.WriteLine("Local history has been wiped.");
                    break;
                case "r":
                    reuseCalculation = true;
                    break;
                case "e":
                    endApp = true;
                    break;
                default:
                    break;
            }
        }
        public Calculation DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            Calculation calculation = new Calculation();
            calculation.Num1 = num1;
            var binaryOperations = new HashSet<string> { "a", "s", "m", "d", "pow" };
            if (binaryOperations.Contains(op))
            {
                calculation.Num2 = num2;
            }
            calculation.Operation = op;
            // Use a switch statement to do the math.
            //a|s|m|d|sqrt|pow|10x|sin|cos|tan
            string operationName = "";
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    calculation.Result = result;
                    operationName = "Addition";
                    break;
                case "s":
                    result = num1 - num2;
                    calculation.Result = result;
                    operationName = "Subtraction";
                    break;
                case "m":
                    result = num1 * num2;
                    calculation.Result = result;
                    operationName = "Multiplication";
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        operationName = "Division";
                        calculation.Result = result;
                    }
                    break;
                case "sqrt":
                    if (num1 >= 0)
                    {
                        result = Math.Sqrt(num1);
                        calculation.Result = result;
                        operationName = "Square Root";
                        calculation.Num2 = null; // second number isn't used in square root
                    }
                    else
                    {
                        Console.WriteLine("Cannot calculate square root of a negative number.");
                    }
                    break;
                case "pow":
                    result = Math.Pow(num2, num1);
                    operationName = "Power";
                    calculation.Result = result;
                    break;
                case "l":
                    if (num1 >= 0)
                    {
                        operationName = "Logarithm";
                        result = Math.Log(num1);
                        calculation.Result = result;
                    }
                    else
                    {
                        Console.WriteLine("Cannot calculate logarithm of a negative number.");
                    }
                    calculation.Num2 = null;
                    break;
                case "sin":
                    result = Math.Sin(num1 * (Math.PI / 180));
                    calculation.Result = result;
                    operationName = "Sine";
                    calculation.Num2 = null;
                    break;
                case "cos":
                    result = Math.Cos(num1 * (Math.PI / 180));
                    calculation.Result = result;
                    operationName = "Cosine";
                    calculation.Num2 = null;
                    break;
                case "tan":
                    result = Math.Tan(num1 * (Math.PI / 180));
                    calculation.Result = result;
                    operationName = "Tangent";
                    calculation.Num2 = null;
                    break;
                default:
                    break;
            }
            _logger.LogOperation(num1, binaryOperations.Contains(op) ? num2 : null, op, operationName, result);
            AddToHistory(calculation);

            return calculation;
        }
    }
}
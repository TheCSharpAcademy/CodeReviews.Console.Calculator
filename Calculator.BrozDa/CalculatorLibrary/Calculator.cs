namespace CalculatorLibrary
{
    /// <summary>
    /// Represent instance of Calculor alowing user to perform mathematical calculations
    /// </summary>
    public class Calculator
    {
        private int _numberOfUses;
        private Logger _logger;
        private Printer _printer;
        private List<string> _calculationHistory;
        private List<double> _calculationHistoryResults;
        private readonly Dictionary<string, string> _operationCharacters = new Dictionary<string, string>()
        {
            {"a", "+"},
            {"s", "-"},
            {"m", "*"},
            {"d", "/"},
            {"p", "^"},
            {"sr", "√"},
            {"10x", "* 10"},
            {"sin", "Sin"},
            {"cos", "Cos"},
            {"tg", "Tan"},

        };
        public bool GettingResultFromHistory { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="Calculator"/> object
        /// </summary>
        public Calculator()
        {
            _logger = new Logger();
            _printer = new Printer();
            _numberOfUses = _logger.GetNumberOfUses();
            _calculationHistory = new List<string>();
            _calculationHistoryResults = new List<double>();
            GettingResultFromHistory = false;
        }
        /// <summary>
        /// Starts the calculator application
        /// </summary>
        public void Start()
        {
            _printer.PrintTitle(_numberOfUses);
            _printer.PrintHistory(_calculationHistory);
            _printer.PrintOperationSelectionMenu();
        }
        /// <summary>
        /// Perform calculation based on chosen operation, logs it to JSON and add it to history
        /// </summary>
        /// <param name="operation"><see cref="string"/> literal representing operation for calculation</param>
        /// <returns><see cref="double"/> representing result of the calculation</returns>
        public double Calculate(string operation) {
            double result = double.NaN;
            double num1;
            double num2;

            switch (operation) {
                case "a": // addition
                case "s": // substraction
                case "m": // multiplication
                case "d": // division
                    num1 = GetNumber(1);
                    num2 = GetNumber(2);
                    result = BasicOperation(num1, num2, operation);
                    _logger.LogToJson(num1, num2, operation, result);
                    AddCalculationToHistory(num1, num2, operation, result);
                    break;
                case "p": // power
                    num1 = GetNumber(1);
                    num2 = GetNumber(2);
                    result = Power(num1, num2);
                    _logger.LogToJson(num1, num2, operation, result);
                    AddCalculationToHistory(num1, num2, operation, result);
                    break;
                case "sr": // square root
                    num1 = GetNumber(1);
                    result = SquareRoot(num1);
                    _logger.LogToJson(num1, operation, result);
                    AddCalculationToHistory(num1, operation, result);
                    break;
                case "10x":
                    num1 = GetNumber(1);
                    result = TenTimes(num1);
                    _logger.LogToJson(num1, operation, result);
                    AddCalculationToHistory(num1, operation, result);
                    break;
                case "sin":
                case "cos":
                case "tg":
                    num1 = GetNumber(1);
                    result = Trigonometry(num1, operation);
                    _logger.LogToJson(num1, operation, result);
                    AddCalculationToHistory(num1, operation, result);
                    break;
            }
            _calculationHistoryResults.Add(result);
            _numberOfUses++;

            return result;
        }
        /// <summary>
        /// Perform basic math operation based on the <see cref="string"/> literal representing math operation
        /// </summary>
        /// <param name="num1"><see cref="double"/> number representing first operand</param>
        /// <param name="num2"><see cref="double"/> number representing second operand</param>
        /// <param name="operation"><see cref="string"/> literal representing operation for calculation</param>
        /// <returns><see cref="double"/> representing result of the calculation</returns>
        public double BasicOperation(double num1, double num2, string operation)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            switch (operation)
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
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }

            return result;
        }
        /// <summary>
        /// Multiplies selected number 10 times
        /// </summary>
        /// <param name="number"><see cref="double"/> number representing operand</param>
        /// <returns><see cref="double"/>Number multiplied by 10</returns>
        public double TenTimes(double number)
        {
            return number * 10;
        }
        /// <summary>
        /// Performs power operation
        /// </summary>
        /// <param name="number"><see cref="double"/> Number to be raised to power</param>
        /// <param name="power"><see cref="double"/> Number specifiyng the power</param>
        /// <returns><see cref="double"/> Number raised to power</returns>
        public double Power(double number, double power)
        {
            return Math.Pow(number, power); 
        }
        /// <summary>
        /// Perform Square root operation
        /// </summary>
        /// <param name="number"><see cref="double"/> number for which you need to find square root</param>
        /// <returns><see cref="double"/> number representing square root of number</returns>
        public double SquareRoot(double number)
        {
            return Math.Sqrt(number);
        }
        /// <summary>
        /// Performs Trigonometry operation based on operation passed in argument
        /// </summary>
        /// <param name="number"><see cref="double"/> number representing the angle</param>
        /// <param name="function"><see cref="string"/> representing the function</param>
        /// <returns><see cref="double"/> representing result or the operation</returns>
        public double Trigonometry(double number, string function)
        {
            double result = double.NaN;
            switch (function) 
            {
                case "sin":
                    result = Math.Sin(number);
                    break;
                case "cos":
                    result = Math.Cos(number);
                    break;
                case "tg":
                    result = Math.Tan(number);
                    break;
            }
            return result;
        }
        /// <summary>
        /// Gets a number for the operation for specified position
        /// </summary>
        /// <param name="position"><see cref="int"/> representing position of the operand</param>
        /// <returns><see cref="double"/> number representing the operand</returns>
        private double GetNumber(int position)
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
        /// <summary>
        /// Gets a new number for operand from user input
        /// </summary>
        /// <param name="position"><see cref="int"/> representing position of the operand</param>
        /// <returns><see cref="double"/> number representing the operand</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown then the operation is invalid</exception>
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
        /// <summary>
        /// Gets a number for operation from results of already performed calculations
        /// </summary>
        /// <returns><see cref="double"/> number representing the operand</returns>
        private double GetNumberFromResult()
        {
            string? numInput;
            int index;

            Console.Write("Enter index number of result you'd like to use: ");
            numInput = Console.ReadLine();
            while (!int.TryParse(numInput, out index) || index - 1 < 0 || index - 1 > _calculationHistoryResults.Count)
            {
                Console.Write("This is not valid input. Please enter a valid numeric value: ");
                numInput = Console.ReadLine();
            }
            Console.WriteLine("First Number for calculation: " + _calculationHistoryResults[index - 1]);

            GettingResultFromHistory = false;
            return _calculationHistoryResults[index - 1];
        }
        /// <summary>
        /// Prints and option menu and start processing it
        /// </summary>
        public void OptionsMenu()
        {
            _printer.PrintOptionsMenu(_numberOfUses);
            ProcessOptionMenu();
        }
        /// <summary>
        /// Process user input within the options menu and perform selected actions
        /// </summary>
        private void ProcessOptionMenu()
        {
            bool wantToStayInMenu = true;

            while (wantToStayInMenu)
            {
                int selection = GetAndValidateMenuInput();

                switch (selection)
                {
                    case 1:
                        _printer.PrintHistory(_calculationHistory);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        _printer.PrintOptionsMenu(_numberOfUses);
                        break;
                    case 2:
                        _calculationHistory.Clear();
                        _calculationHistoryResults.Clear();
                        Console.WriteLine("History Deleted");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        _printer.PrintOptionsMenu(_numberOfUses);
                        break;
                    case 3:
                        wantToStayInMenu = false;
                        break;
                }
            }
        }
        /// <summary>
        /// Prompts user for an input and validates it
        /// </summary>
        /// <returns>Valid <see cref="int"/> number representing user selection</returns>
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
        /// <summary>
        /// Add current calculation with two operands to history 
        /// </summary>
        /// <param name="num1"><see cref="double"/> number representing first operand</param>
        /// <param name="num2"><see cref="double"/> number representing second operand</param>
        /// <param name="operation"><see cref="string"/> literal representing operation for calculation</param>
        /// <param name="result"><see cref="double"/> number representing result of calculation</param>
        private void AddCalculationToHistory(double num1, double num2, string operation, double result)
        {
            _calculationHistory.Add($"{num1} {_operationCharacters[operation]} {num2} = {result}");

        }
        /// <summary>
        /// Add current calculation with single operand to history 
        /// </summary>
        /// <param name="num1"><see cref="double"/> number representing first operand</param>
        /// <param name="operation"><see cref="string"/> literal representing operation for calculation</param>
        /// <param name="result"><see cref="double"/> number representing result of calculation</param>
        private void AddCalculationToHistory(double num1, string operation, double result)
        {
            _calculationHistory.Add($"{_operationCharacters[operation]} {num1} = {result}");
        }
        /// <summary>
        /// Wraps up calculator application, closing all log files
        /// </summary>
        public void Finish()
        {
            _logger.Finish(_numberOfUses);
        }
    }
}

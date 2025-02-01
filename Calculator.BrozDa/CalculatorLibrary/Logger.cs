using Newtonsoft.Json;

namespace CalculatorLibrary
{
    /// <summary>
    /// Represent object used to log calculation operations to a log file
    /// </summary>
    internal class Logger
    {
        private readonly Dictionary<string, string> operationText = new Dictionary<string, string>()
        {
            {"a", "Add"},
            {"s", "Substract"},
            {"m", "Multiply"},
            {"d", "Divide"},
            {"p", "Power"},
            {"sr", "Square root"},
            {"10x", "Ten time"},
            {"sin", "Sinus"},
            {"cos", "Cosinus"},
            {"tg", "Tangent"},

        };
        private JsonWriter _writer;
        /// <summary>
        /// Initializes new object of <see cref="Logger"/> class
        /// </summary>
        public Logger()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            _writer = new JsonTextWriter(logFile);
            _writer.Formatting = Formatting.Indented;
            InitializeJsonLog();
        }
        /// <summary>
        /// Initializes JSON log used for logging calculations
        /// </summary>
        public void InitializeJsonLog()
        {
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();
        }
        /// <summary>
        /// Logs current calculation with two operands to JSON 
        /// </summary>
        /// <param name="num1"><see cref="double"/> number representing first operand</param>
        /// <param name="num2"><see cref="double"/> number representing second operand</param>
        /// <param name="operation"><see cref="string"/> literal representing operation for calculation</param>
        /// <param name="result"><see cref="double"/> number representing result of calculation</param>
        public void LogToJson(double num1, double num2, string operation, double result)
        {
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(num2);
            _writer.WritePropertyName("Operation");
            _writer.WriteValue(operationText[operation]);
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
        }
        /// <summary>
        /// Logs current calculation single operand to JSON 
        /// </summary>
        /// <param name="num1"><see cref="double"/> number representing first operand</param>
        /// <param name="operation"><see cref="string"/> literal representing operation for calculation</param>
        /// <param name="result"><see cref="double"/> number representing result of calculation</param>
        public void LogToJson(double num1, string operation, double result)
        {
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);
            _writer.WritePropertyName("Operation");
            _writer.WriteValue(operationText[operation]);
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
        }
        /// <summary>
        /// Retreive a number representing number of times <see cref="Calculator"/> was used
        /// </summary>
        /// <returns><see cref="int"/> number representing number of times <see cref="Calculator"/> was used</returns>
        public int GetNumberOfUses()
        {
            StreamReader readNumberOfUses = new StreamReader("NumberOfUses.txt");
            int numberOfUses = Convert.ToInt32(readNumberOfUses.ReadLine());
            readNumberOfUses.Close();
            return numberOfUses;
        }
        /// <summary>
        /// Logs a number representing number of times <see cref="Calculator"/> was used
        /// </summary>
        /// <param name="numberOfUses"><see cref="int"/> number representing number of times <see cref="Calculator"/> was used</param>
        public void WriteNumberOfUses(int numberOfUses)
        {
            StreamWriter writeNumberOfUses = new StreamWriter("NumberOfUses.txt");
            writeNumberOfUses.Write(numberOfUses);
            writeNumberOfUses.Close();
        }
        /// <summary>
        /// Wraps up calculator application, closing all log files
        /// </summary>
        /// <param name="numberOfUses"><see cref="int"/> number representing number of uses of the calculator application</param>
        public void Finish(int numberOfUses)
        {
            _writer.WriteEndArray();
            _writer.WriteEndObject();
            _writer.Close();
            WriteNumberOfUses(numberOfUses);
        }
    }
}

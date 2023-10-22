namespace CalculatorLibrary
{
    public class Constants
    {
        public const string PATH = "calculatorlog.json";
        public const string ADD = "Add";
        public const string SUBSTRACT = "Substract";
        public const string MULTIPLY = "Multiply";
        public const string DIVIDE = "Divide";
        public const string OPERATION_ADD = "a";
        public const string OPERATION_SUB = "s";
        public const string OPERATION_MUL = "m";
        public const string OPERATION_DIV = "d";
        public const string CALCULATION_TIMES = "t";
        public const string CALCULATION_HISTORY = "h";
        public const string CLEAR_HISTORY = "c";

        public static readonly Dictionary<string, string> TPS = new Dictionary<string, string>
        { {ADD, "+"}, {SUBSTRACT, "-"}, {MULTIPLY, "*"}, { DIVIDE, "/"}};
    };
}

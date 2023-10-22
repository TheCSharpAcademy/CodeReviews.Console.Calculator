namespace CalculatorLibrary
{
    public class Constants
    {
        public const string PATH = "calculatorlog.json";
        public const string ADD = "+";
        public const string SUBSTRACT = "-";
        public const string MULTIPLY = "*";
        public const string DIVIDE = "/";
        public const string SQUARE_ROOT = "√";
        public const string POWER = "**";
        public const string SIN = "sin";
        public const string COS = "cos";
        public const string OPERATION_ADD = "a";
        public const string OPERATION_SUB = "s";
        public const string OPERATION_MUL = "m";
        public const string OPERATION_DIV = "d";
        public const string OPERATION_SR = "sr";
        public const string OPERATION_P = "p";
        public const string OPERATION_SIN = "sin";
        public const string OPERATION_COS = "cos";
        public const string CALCULATION_TIMES = "t";
        public const string CALCULATION_HISTORY = "h";
        public const string CLEAR_HISTORY = "c";
        public const string USE_HISTORY = "u";

        public static readonly HashSet<string> TWO_PARA_TYPES = new HashSet<string>()
        { ADD, SUBSTRACT, MULTIPLY, DIVIDE};

    };
}

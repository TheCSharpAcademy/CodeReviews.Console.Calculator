

namespace CalculatorLibrary
{
    static public class HistoryFunctions
    {
        internal static List<History> history = new() { };
        public static double HistoryResult { get; set; }
        public static int CalcUses { get; set; }

        public static void AddToHistory(double num1, double? num2, OperationType type, double result)
        {

            history.Add(new History
            {
                Operand1 = num1,
                Operand2 = num2,
                Type = type,
                Result = result
            });
        }

        public static void HistoryMenu()
        {
            PrintHistory();
            Console.WriteLine("\n");
            Console.WriteLine(@$"Choose:
c - To clear history
r - Start a new operation using a previous result
or Enter to return to the main menu");

            var userChoice = Console.ReadLine().ToLower();

            switch (userChoice)
            {
                case "c":
                    ClearHistory();
                    HistoryMenu();
                    break;
                case "r":
                    ContinueOperation();
                    break;
                default:
                    break;
            }
        }

        public static void PrintHistory()
        {
            Console.Clear();
            Console.WriteLine("History\n");
            foreach (var entry in history)
            {
                Console.WriteLine($"{history.IndexOf(entry) + 1}. {entry.Operand1} {entry.Type} {entry.Operand2} = {entry.Result}");
            }
            Console.WriteLine("\n");
        }

        public static void ClearHistory()
        {
            history = new List<History> { };
        }

        public static OperationType TextToEnum(string op)
        {
            return op switch
            {
                "a" => OperationType.Add,
                "s" => OperationType.Subtract,
                "m" => OperationType.Multiply,
                "d" => OperationType.Divide,
                "q" => OperationType.SquareRoot,
                "p" => OperationType.PowerOf,
                "x" => OperationType.X10,
                "i" => OperationType.Sin,
                "c" => OperationType.Cos,
                "t" => OperationType.Tan,
                _ => OperationType.Broken,
            };
        }

        public static void ContinueOperation()
        {
            Console.WriteLine("Type the number of the operation you wish to continue from:");
            var operationChoice = Int32.Parse(Console.ReadLine());
            var selectedHistory = history[operationChoice - 1].Result;
            HistoryResult = selectedHistory;
            Console.Clear();
        }
    }
}


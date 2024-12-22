using Data.Models;

namespace Helpers
{
    internal static class Helper
    {
        public static void AddToHistory(int id, double num1, double num2, string op, double result, List<CalculationHistory> calculationHistory)
        {
            CalculationHistory history = new CalculationHistory();

            if (op == "sr" || op == "t")
            {
                history.Id = id;
                history.Operand1 = num1;
                history.Operation = op;
                history.Result = result;
                calculationHistory.Add(history);
                return;
            }
            else
            {
                history.Id = id;
                history.Operand1 = num1;
                history.Operand2 = num2;
                history.Operation = op;
                history.Result = result;
                calculationHistory.Add(history);
            }

        }

        public static void ViewHistory(List<CalculationHistory> calculationHistory)
        {
            Console.Clear();
            Console.WriteLine("Calculation History ----------------------------------------------------------------------");
            foreach (CalculationHistory history in calculationHistory)
            {
                if (history.Operation == "sr" || history.Operation == "t")
                {
                    Console.WriteLine($"Operation # {history.Id}: {GetOperationSign(history.Operation)} {history.Operand1} = {history.Result}");
                    continue;
                }
                else
                {
                    Console.WriteLine($"Operation # {history.Id}: {history.Operand1} {GetOperationSign(history.Operation)} {history.Operand2} = {history.Result}");
                }

            }
            Console.WriteLine();
            Console.WriteLine("Enter 'c' to clear history, or press any other key to contiue\n");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            string reply = Console.ReadLine();

            if (reply == "c")
            {
                calculationHistory.Clear();
            }
        }

        public static string GetOperationSign(string op)
        {
            switch (op)
            {
                case "a":
                    return "+";
                case "s":
                    return "-";
                case "m":
                    return "*";
                case "d":
                    return "/";
                case "p":
                    return "Power Of";
                case "sr":
                    return "Square Root Of";
                case "t":
                    return "tan";
                default:
                    return "";
            }
        }

        public static double GetInput(bool historyAvailable, List<CalculationHistory> calculationHistory)
        {
            double cleanNum = 0;
            string input = null;


            if (historyAvailable)
            {
                Console.Write("Type a number, or type 'h' to use a result from history, and then press Enter: ");
                input = Console.ReadLine();
                while (!double.TryParse(input, out cleanNum) && input != "h")
                {
                    Console.Write("This is not valid input. Please enter an integer value or 'h': ");
                    input = Console.ReadLine();
                }
                if (input == "h")
                {
                    cleanNum = GetPreviousResult(calculationHistory);
                }

            }
            else
            {
                Console.Write("Type a number, and then press Enter: ");
                input = Console.ReadLine();
                while (!double.TryParse(input, out cleanNum))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    input = Console.ReadLine();
                }
            }

            return cleanNum;
        }

        public static double GetPreviousResult(List<CalculationHistory> calculationHistory)
        {
            int historyCount = calculationHistory.Count;
            int operationNumber = 0;
            Console.Clear();
            Console.WriteLine("Calculation History ----------------------------------------------------------------------");
            foreach (CalculationHistory history in calculationHistory)
            {
                Console.WriteLine($"Operation # {history.Id}: {history.Operand1} {GetOperationSign(history.Operation)} {history.Operand2} = {history.Result}");
            }
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.Write("Enter the operation number to use the result: ");

            while (!int.TryParse(Console.ReadLine(), out operationNumber) || operationNumber < 1 || operationNumber > historyCount)
            {
                Console.Write("This is not valid input. Please enter a valid operation number: ");
            }

            return calculationHistory[operationNumber - 1].Result;
        }
    }
}




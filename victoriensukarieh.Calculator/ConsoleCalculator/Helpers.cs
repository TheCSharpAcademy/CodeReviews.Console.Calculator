using ConsoleCalculator.Models;
namespace ConsoleCalculator;

internal class Helpers
{
    public static List<Operation> history = new();

    public static void SaveToHistory(Double[] operands, string oper, double result)
    {
        List<Double> theList = new();
        for (int i = 0; i < operands.Length; i++)
        {
            theList.Add(operands[i]);
        }
        history.Add(new Operation
        {
            Operator = oper,
            Operands = theList,
            Result = result
        });
    }

    public static void DisplayHistory()
    {
        if (history.Count == 0)
        {
            Console.WriteLine("No history to display...");
        }
        else
        {
            //Console.WriteLine("Date \t Type \t Player \t Score");
            Console.WriteLine("Calculation History");
            Console.WriteLine("-----------------");
            foreach (var h in history)
            {
                Console.WriteLine($"Operator: {h.Operator}");
                for (int i = 0; i < h.Operands.Count; i++)
                {
                    Console.WriteLine($"Operand {i} : {h.Operands[i]}");
                }
                Console.WriteLine($"Result: {h.Result}");
                Console.WriteLine("-----------------");
            }            
        }
    }
    public static bool NoHistory() {
        if (history.Count == 0) { return true; }
        else return false;
    }

    public static void ClearHistory() {
        history.Clear();  
        Console.WriteLine("History Cleared");
    }

    public static string AssignOperation(string o) {
        string operationAssigned="";
        switch (o.ToUpper()) {
            case "A":
                operationAssigned = "Add";
                break;
            case "S":
                operationAssigned = "Subtract";
                break;
            case "M":
                operationAssigned = "Multiply";
                break;
            case "D":
                operationAssigned = "Divide";
                break;
            case "P":
                operationAssigned = "Power";
                break;
            case "Q":
                operationAssigned = "Square Root";
                break;
            case "T":
                operationAssigned = "10x";
                break;
            case "TC":
                operationAssigned = "Cosine";
                break;
            case "TS":
                operationAssigned = "Sine";
                break;
            case "TT":
                operationAssigned = "Tangeant";
                break;
            default:
                break;
        }
        return operationAssigned;
    }
    public static double GetLastResult() {
        var lastResult = history.Last();
        return lastResult.Result;
    }
}

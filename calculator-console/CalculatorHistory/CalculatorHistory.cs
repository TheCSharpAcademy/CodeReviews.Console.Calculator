using System.Runtime.CompilerServices;

namespace CalculatorHistory
{
    public static class History
    {
        private static List<List<double>> operandsList = [];
        private static List<int> typeOfOperationList = new List<int>();
        private static List<int> operationsList = [];
        private static List<double> resultList = [];
        private static List<DateTime> calculationTimestamp = [];

        private static string ResolveOperatorsFromInt(int op, int typeofop)
        {
            switch (typeofop)
            {
                case 0:
                    return op switch
                    {
                        0 => "+",
                        1 => "-",
                        2 => "*",
                        3 => "/",
                        4 => "^",
                        5 => "sqrt",
                        6 => "x10^",
                        _ => throw new Exception("operator not valid")
                    };
                case 1:
                    return op switch
                    {
                        0 => "Sin",
                        1 => "Cos",
                        2 => "Tan",
                        3 => "Cosec",
                        4 => "Sec",
                        5 => "Cot",
                        _ => throw new Exception("operator not valid")
                    };
                default:
                    throw new NotImplementedException("No other type of operations yet");

            }
        }

        public static bool HasHistory()
        {
            return resultList.Count > 0;
        }
        public static void SaveHistory(List<double> operands,int typeOfOperation, int operation, double result, DateTime timestamp)
        {
            operandsList.Add(new List<double>(operands));
            typeOfOperationList.Add(typeOfOperation);
            operationsList.Add(operation);
            resultList.Add(Math.Round(result, 3));
            calculationTimestamp.Add(timestamp);
        }

        public static void DisplayHistory()
        {
            Console.WriteLine("\n\n--------------------------------------------------------------------------------");
            Console.WriteLine("CALCULATION HISTORY");
            Console.WriteLine("--------------------------------------------------------------------------------\n");
            Console.WriteLine("S.N.\t\tOperands\t\tOperation\t\tResult\t\t\tTimestamp");
            if (resultList.Count > 0)
            {

                for (int i = 0; i < resultList.Count; i++)
                {
                    Console.Write(i + 1 + "\t\t");
                    string operands = String.Join(",", operandsList[i]);
                    Console.Write($"{operands}\t\t\t");
                    Console.Write($"{ResolveOperatorsFromInt(operationsList[i], typeOfOperationList[i])}\t\t\t");
                    Console.Write($"{resultList[i]}\t\t\t");
                    Console.WriteLine(calculationTimestamp[i].ToString("yyyy/MM/dd HH:mm:ss"));
                }
                Console.WriteLine("\n\n");
            }
            else
            {
                Console.WriteLine("No records in the history. Perform some calculations first");
            }
            Console.WriteLine("--------------------------------------------------------------------------------\n");
        }

        public static void DeleteHistory()
        {
            operandsList = [];
            typeOfOperationList = [];
            operationsList = [];
            resultList = [];
            calculationTimestamp = [];
            DisplayHistory();
        }

        public static void DeleteHistory(int index)
        {
            if (index > 0 && index <= operandsList.Count)
            {
                operandsList.RemoveAt(index - 1);
                typeOfOperationList.RemoveAt(index - 1);
                operationsList.RemoveAt(index - 1);
                resultList.RemoveAt(index - 1);
                calculationTimestamp.RemoveAt(index - 1);
            }
            else
            {
                Console.WriteLine("Invalid index entered");
                return;
            }
            DisplayHistory();
        }

        public static int GetPastResultCount()
        {
            return resultList.Count;
        }


        public static double GetPastResult(int index)
        {
             return resultList[index - 1];
        }
    }
}

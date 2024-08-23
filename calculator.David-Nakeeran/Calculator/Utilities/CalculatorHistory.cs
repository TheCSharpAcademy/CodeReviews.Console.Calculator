namespace CalculatorProgram
{
    class CalculatorHistory
    {
        List<CalculationResult> calculations = new List<CalculationResult>();
        internal int CalculationsCount
        {
            get { return calculations.Count; }
        }

        internal void AddToCalculatorHistory(double num1, double num2, double resultValue, string symbol, OperationType operationType)
        {
            calculations.Add(new CalculationResult
            {
                NumOne = num1,
                NumTwo = num2,
                Result = resultValue,
                OperationSymbol = symbol,
                Operation = operationType
            });
        }

        internal double[] GetCalculationByIndex(int index1, int index2)
        {
            double[] result = new double[2];

            result[0] = calculations[index1 - 1].Result;
            result[1] = calculations[index2 - 1].Result;

            return result;
        }

        internal void PrintCalculatorHistory()
        {
            if (calculations.Count == 0)
            {
                Console.WriteLine("No previous calculations");
                return;
            }
            else
            {
                for (int i = 0; i < calculations.Count; i++)
                {
                    CalculationResult result = calculations[i];
                    Console.WriteLine($"Index number = {i + 1}) {result.NumOne} {result.OperationSymbol} {result.NumTwo} = {result.Result}");
                }
            }

        }

        internal bool IsValidIndex(int index)
        {
            return index >= 1 && index <= calculations.Count;
        }

        internal void ClearHistory()
        {
            calculations.Clear();
        }
    }

}
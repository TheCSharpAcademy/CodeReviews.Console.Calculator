using Calculator.Models;

namespace Calculator
{

    internal class HistoryCalcu
    {
        public List<StorageCal> history = new List<StorageCal>();


        public void AddCals(string op, double result)
        {
            history.Add(new StorageCal { Date = DateTime.Now, NameOp = op, Result = result });
        }

        public void PrintHistory()
        {
            foreach (StorageCal cal in history)
            {
                Console.WriteLine($" {cal.Date.ToString("yyyy-MM-dd")}, Operation: {cal.NameOp}, Result: {cal.Result}");
            }
        }


        public double GetPResult()
        {
            if (history.Count == 0)
            {
               
                return 0;
            }

            return history[^1].Result;

        }

    }
}

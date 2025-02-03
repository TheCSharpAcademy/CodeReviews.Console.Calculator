



namespace CalculatorLibrary
{
    public class CalculatorHistory
    {
       private List<double> operationsHistory=new();
        private int placeToInsert;

        public void LogResult(double result)
        {
            if (operationsHistory.Count == 5)
            {
                operationsHistory.Insert(placeToInsert % 5, result);
                placeToInsert++;
            }
            else
            {
                operationsHistory.Add(result);
            }
        }
        public void RemoveList()
        {
            operationsHistory.Clear();
        }

        public void ShowList()
        {
            foreach (var item in operationsHistory)
            {
                Console.WriteLine(item);
            }
        }
        public double GetNumberFromList()
        {
            double cleanNum = Double.NaN;

            if (operationsHistory.Count()==0)
            {
                Console.WriteLine("History empty! Choose something yourself: ");
                string userChoice=Console.ReadLine();
                Double.TryParse(userChoice, out cleanNum);
                while(!Double.TryParse(userChoice, out cleanNum))
                {
                    Console.WriteLine("Bad input, try again");
                    userChoice = Console.ReadLine();
                }
                return cleanNum;

            }
            
            Console.WriteLine("History of Calculations:");
            Console.WriteLine("Choose one ");
            foreach (var item in operationsHistory)
            {
                int i = 0;
                Console.WriteLine($"[{i++}] {item}");
            }
            string input=Console.ReadLine();
            double.TryParse(input, out cleanNum);
            while (!double.TryParse(input, out cleanNum) || cleanNum > 5 || cleanNum < 0) 
            {
                Console.WriteLine("Bad input, try again");
                input = Console.ReadLine();
            }
            return operationsHistory[(int)cleanNum];
        }
    }
}

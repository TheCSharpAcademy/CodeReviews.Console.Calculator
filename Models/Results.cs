using System.Text.RegularExpressions;

namespace calculador.Models
{
    public class Results
    {   
        public List<double> ResultsOperations { get; set; }

        public Results()
        {
            ResultsOperations = new List<double>();
        }

        public void AddResultToList(double result)
        {
            ResultsOperations.Add(result);
        }
        public void ListOfResults()
        {   
            string? op = "";

            Console.WriteLine("1 - Show list");
            Console.WriteLine("2 - Delete list");
            op = Console.ReadLine();

            Console.WriteLine();

            while (op == null || !Regex.IsMatch(op, "[1|2]"))
            {
                Console.WriteLine("Error: Unrecognized input. Type 1 or 2");
                op = Console.ReadLine();
            }
            if (op == "1")
            {
                foreach (double item in ResultsOperations)
                {
                    Console.WriteLine(item);
                }
            } else if (op == "2")
            {
                ResultsOperations.Clear();
            }
        }

    }
}
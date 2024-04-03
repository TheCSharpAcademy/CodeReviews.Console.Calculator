using System.Text.RegularExpressions;

namespace calculador.Models
{
    public class Results
    {   
        public List<double> resultsOperations { get; set; }

        public Results()
        {
            resultsOperations = new List<double>();
        }

        public void addResultToList(double result)
        {
            resultsOperations.Add(result);
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
                foreach (double item in resultsOperations)
                {
                    Console.WriteLine(item);
                }
            } else if (op == "2")
            {
                resultsOperations.Clear();
            }
        }

    }
}
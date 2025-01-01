using System.Text.RegularExpressions;

namespace Calculator
{
    internal class Menu
    {
        public static HistoryCalcu historyCalcu = new HistoryCalcu();
        public static MenuHelpers menuHelpers = new MenuHelpers();
        public void MainMenu()
        {

            Calculator.SetHistoryCalcu(historyCalcu);
            Input.SetHistoryInput(historyCalcu);
            bool endApp = false;
            Console.WriteLine("Console calculator en c#\r");
            Console.WriteLine("------------------------\n");
            int count = 0;
            
            while (!endApp)
            {
                Console.WriteLine($"Have you used {count} times the calculator");

                double result = 0;

                Console.WriteLine("Choose an operation from the follow list:");
                Console.WriteLine("\t1 - add");
                Console.WriteLine("\t2 - Subtract");
                Console.WriteLine("\t3 - Multiply");
                Console.WriteLine("\t4 - Divide");
                Console.WriteLine("\t5 - Sqrt");
                Console.WriteLine("\t6 - Take Power");
                Console.WriteLine("\t7 - Sen");
                Console.WriteLine("\t8 - Cos");
                Console.WriteLine("\t9 - Tan");
                Console.WriteLine("\t10 - History Operations");
                Console.Write("Your option? ");



                string? op = Console.ReadLine();

                //Validate the input is not null and matches the pattern
                if (op == null || !Regex.IsMatch(op, "^[1-9]$|^10$"))
                {
                    Console.WriteLine("Error: Unrecognized input");
                    continue;
                }
                if (op == "10")
                {
                    historyCalcu.PrintHistory();

                }
                else
                {
                    result = menuHelpers.PerfomOp(op);
                    if (!double.IsNaN(result))
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                }

                Console.WriteLine("---------------\n");
                Console.WriteLine("Press n and Enter to close the app or any other key to continue ");
                if (Console.ReadLine()?.ToLower() == "n")
                {
                    endApp = true;

                }
                count++;


            }
            return;

        }
    }
}

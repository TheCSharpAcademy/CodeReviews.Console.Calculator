using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            double previusResult = 0;
            while (!endApp)
            {
                Console.WriteLine($"Have you used {count} times the calculator");
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;



                Console.WriteLine("Choose an operation from the follow list:");
                Console.WriteLine("\t1 - add");
                Console.WriteLine("\t2 - Subtract");
                Console.WriteLine("\t3 - Multiply");
                Console.WriteLine("\t4 - Divide");
                Console.WriteLine("\t5 - Sqrt");
                Console.WriteLine("\t6 - Historial calculation");
                Console.Write("Your option? ");



                string? op = Console.ReadLine();

                //Validate the input is not null and matches the pattern
                if (op == null || !Regex.IsMatch(op, "^[1-6]$"))
                {
                    Console.WriteLine("Error: Unrecognized input");
                    continue;
                }
                if (op == "6")
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
                if (Console.ReadLine()?.ToLower() == "n"){
                    endApp = true;

                }
                count++;
                

            }
            return;
            
        }
    }
}

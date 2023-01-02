using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Lonchanick
{
    public class ToolBox
    {
        public static double GetInputDouble()
        {
            double aux;
            string answer = Console.ReadLine();
            while (!double.TryParse(answer, out aux))
            {
                Console.WriteLine("Error, Type again:");
                answer = Console.ReadLine();
            }
            return aux;
        }

        public static string GetValidOption()
        {
            //this mean option can't be more than one char and an option that is not allowed
            string ops = "asmd";
            string op = "";
            bool wh = true;
            while (wh)
            {
                op = Console.ReadLine();
                if (ops.IndexOf(op[0]) == -1 | op.Length > 1)
                    Console.WriteLine("No es una opcion valida try again: ");
                else
                    wh = false;
            }
            return op;
        }
    }
}

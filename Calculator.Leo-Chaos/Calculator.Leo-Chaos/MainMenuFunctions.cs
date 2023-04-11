

namespace Calculator.LeoChaos
{
    internal class MainMenuFunctions
    {
        public static bool RequiresSecondNumber(string op)
        {
            var num2Operations = new[]
             {
                    "a",
                    "s",
                    "m",
                    "d",
                    "p"
                };
            return num2Operations.Contains(op);
        }

        public static double GetNumber()
        {
            Console.Write("Type a number, and then press Enter: ");
            var numInput = Console.ReadLine();

            double cleanNum;
            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput = Console.ReadLine();
            }

            return cleanNum;
        }


        public static string MenuOperationList()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tq - Find the square root)");
            Console.WriteLine("\tp - Power of (number one to the power of number two)");
            Console.WriteLine("\tx - x10");
            Console.WriteLine("\ti - Sin");
            Console.WriteLine("\tc - Cos");
            Console.WriteLine("\tt - Tan");
            Console.WriteLine("\th - History");
            Console.Write("Your option? ");

            return Console.ReadLine().ToLower();
        }
    }
}

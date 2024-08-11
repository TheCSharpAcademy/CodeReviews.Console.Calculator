using Console_Calculator_App.ConsoleCalculatorApp.Model;

namespace Console_Calculator_App.ConsoleCalculatorApp.View

{
    internal class Menu
    {
        public static void Title(int usage)
        {
            Console.Clear();
            Console.WriteLine($"Console Calculator App (Used {usage} {(usage < 2 ? "time" : "times")})");
            Console.WriteLine("\n-----------------------");
        }

        public static void FirstNum()
        {
            Console.WriteLine("\n-----------------------");
            Console.Write("Type a number, and then press Enter: ");
        }

        public static void SecondNum()
        {
            Console.Write("Type another number, and then press Enter: ");
        }

        public static void Operation()
        {
            Console.WriteLine("\nChoose an operator from the following list:");
            Console.WriteLine("       a - Add");
            Console.WriteLine("       s - Subtract");
            Console.WriteLine("       m - Multiply");
            Console.WriteLine("       d - Divide");
            Console.Write("Your option? ");
        }

        public static void Answer(float answer)
        {
            Console.WriteLine($"Your result: {answer:F2}");
        }

        public static void Error()
        {
            Console.WriteLine($"This operation will result in a mathematical error.\n");
        }

        public static void Options()
        {
            Console.WriteLine("Options:");
            Console.WriteLine("     n - Exit");
            Console.WriteLine("     d - Delete List");
            Console.WriteLine("     u - Use Results");
            Console.WriteLine("     Any other or no letter to continue using the calculator");
            Console.Write("Please select an option from above and press enter: ");
        }

        public static void InvalidInput()
        {
            Console.Write("This is not a valid input. Please enter a numeric value: ");
        }

        public static void InvalidIndex(int max)
        {
            Console.Write($"The index entered is out of range, please enter a number between 0 and {max}: ");
        }

        public static void End()
        {
            Console.Write("Press enter to continue...");
        }

        public static void ViewList(IList<MathProblem> mathProblems)
        {
            Console.Clear();
            Title(mathProblems.Count);
            Console.WriteLine("Index: Math Problem & Results");
            for(int i = 0; i < mathProblems.Count; i++)
            {
                Console.WriteLine($"{i}: {mathProblems[i].ToString()}");
            }
            Console.WriteLine("Enter r to return to the menu or any other letter for your own number");
        }

        public static void PickAResult(int num)
        {
            Console.Write($"Enter the index for the result that will be used for operand {num}: ");
        }
    }
}

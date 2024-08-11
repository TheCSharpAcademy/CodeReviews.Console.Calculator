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

        public static void End()
        {
            Console.WriteLine("\n-----------------------");
            Console.WriteLine("Options:");
            Console.WriteLine("     n - Exit");
            Console.WriteLine("     d - Delete List");
            Console.WriteLine("     v - View List");
            Console.WriteLine("     Any other or no keys to continue");
            Console.WriteLine("Please select an option from above and press enter:");
        }

        public static void InvalidInput()
        {
            Console.Write("This is not a valid input. Please enter a numeric value: ");
        }
    }
}

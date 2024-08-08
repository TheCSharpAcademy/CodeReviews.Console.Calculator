namespace Console_Calculator_App.ConsoleCalculatorApp.View
{
    internal class Menu
    {
        public static void DisplayFirstNum()
        {
            Console.Clear();
            Console.WriteLine("-----------------------\n");
            Console.Write("Type a number, and then press Enter: ");
        }

        public static void DisplaySecondNum()
        {
            Console.Write("Type another number, and then press Enter: ");
        }

        public static void DisplayOperation()
        {
            Console.WriteLine("Choose an operator from the following list:\n");
            Console.WriteLine("       a - Add");
            Console.WriteLine("       s - Subtract");
            Console.WriteLine("       m - Multiply");
            Console.WriteLine("       d - Divide");
            Console.Write("Your option? ");
        }

        public static void DisplayAnswer(float answer)
        {
            Console.WriteLine($"Your result: {answer:F2}");
        }

        public static void DisplayError()
        {
            Console.WriteLine($"This operation will result in a mathematical error.\n");
        }

        public static void DisplayEnd()
        {
            Console.WriteLine("-----------------------\n");
            Console.Write("Press 'n' and Enter to close the app, " +
                                "or press any other key and Enter to continue: ");
        }

        public static void DisplayInvalidInput()
        {
            Console.Write("This is not a valid input. Please enter a numeric value: ");
        }
    }
}

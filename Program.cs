using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();

        float? answer = null;
        float num1, num2;
        string operation, input1, input2;
        do
        {
            Console.Clear();
            Console.WriteLine("-----------------------\n");
            Console.Write("Type a number, and then press Enter: ");
            input1 = Console.ReadLine()!;

            num1 = ParseInput(input1);

            Console.Write("Type another number, and then press Enter: ");
            input2 = Console.ReadLine()!;

            num2 = ParseInput(input2);

            Console.WriteLine("Choose an operator from the following list:\n");
            Console.WriteLine("       a - Add");
            Console.WriteLine("       s - Subtract");
            Console.WriteLine("       m - Multiply");
            Console.WriteLine("       d - Divide");
            Console.Write("Your option? ");
            operation = Console.ReadLine()!.ToLower();

            answer = calculator.CalculateAnswer(operation, num1, num2);

            if (answer != null)
            {
                Console.WriteLine($"Your result: {answer:F2}");
            }
            else
            {
                Console.WriteLine($"This operation will result in a mathematical error.\n");
            }

            Console.WriteLine("-----------------------\n");
            Console.Write("Press 'n' and Enter to close the app, " +
                                "or press any other key and Enter to continue: ");
            string input = Console.ReadLine()!;
            if (input == "n")
            {
                calculator.Finish();
                System.Environment.Exit(0);
            }

        } while (true);
    }

    private static float ParseInput(string input)
    {
        float num;

        do
        {
            try
            {
                num = float.Parse(input);
                break;
            }
            catch (Exception)
            {
                Console.Write("This is not a valid input. Please enter a numeric value: ");
                input = Console.ReadLine()!;
            }
        } while (true);

        return num;
    }
}
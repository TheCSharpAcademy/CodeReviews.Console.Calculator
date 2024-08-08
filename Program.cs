class Program
{
    static void Main(string[] args)
    {
        float? answer = null;
        float num1, num2;
        string operation, input1, input2;
        do
        {
            Console.Clear();
            Console.WriteLine("-----------------------\n");
            Console.Write("Type a number, and then press Enter: ");
            input1 = Console.ReadLine()!;

            do
            {
                try
                {
                    num1 = float.Parse(input1);
                    break;
                }
                catch (Exception)
                {
                    Console.Write("This is not a valid input. Please enter a numeric value: ");
                    input1 = Console.ReadLine()!;
                }
            } while (true);
            
            Console.Write("Type another number, and then press Enter: ");
            input2 = Console.ReadLine()!;

            do
            {
                try
                {
                    num2 = float.Parse(input2);
                    break;
                }
                catch (Exception)
                {
                    Console.Write("This is not a valid input. Please enter a numeric value: ");
                    input2 = Console.ReadLine()!;
                }
            } while (true);

            Console.WriteLine("Choose an operator from the following list:\n");
            Console.WriteLine("       a - Add");
            Console.WriteLine("       s - Subtract");
            Console.WriteLine("       m - Multiply");
            Console.WriteLine("       d - Divide");
            Console.Write("Your option? ");
            operation = Console.ReadLine()!.ToLower();

            switch (operation)
            {
                case "a":
                    answer = num1 + num2;
                    break;
                case "b":
                    answer = num1 - num2;
                    break;
                case "c":
                    answer = num1 * num2;
                    break;
                case "d":
                    answer = num1 / num2;
                    break;
                default:
                    break;
            };

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
                System.Environment.Exit(0);

        } while (true);
    }
}
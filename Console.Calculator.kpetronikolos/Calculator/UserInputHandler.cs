namespace CalculatorProgram;

public static class UserInputHandler
{
    public static double AskForNumber(string message)
    {
        Console.Write($"{message}. Press Enter: ");
        string numInput = Console.ReadLine();

        double output = 0;
        while (!double.TryParse(numInput, out output))
        {
            Console.Write("This is not valid input. Please enter a number: ");
            numInput = Console.ReadLine();
        }

        return output;
    }

    public static int AskForInt(string message)
    {
        Console.Write($"{message}. Press Enter: ");
        string numInput = Console.ReadLine();

        Console.WriteLine();

        int output = 0;
        while (!int.TryParse(numInput, out output))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput = Console.ReadLine();
        }

        return output;
    }

    public static int GetIntAfterOutOfBoundsCheck(int index, int calculationsCount)
    {
        while (index < 1 || index > calculationsCount)
        {
            if (calculationsCount == 1)
            {
                Console.Write($"This is not valid input. There is only one calculation, so kindly choose the following index: 1 ");
            }
            else
            {
                Console.Write($"This is not valid input. Please select a number from the following index range: 1 - {calculationsCount} ");
            }
            
            Console.WriteLine();
            index = AskForInt("Re-enter index");
        }

        return index;
    }

    public static string GetOperation()
    {
        string operation = Console.ReadLine();
        return operation;
    }
}

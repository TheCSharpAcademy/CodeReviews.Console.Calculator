
namespace CalculatorProgram;

public static class UserInputHandler
{
    public static double AskForNumber()
    {
        Console.Write("Type a number, and then press Enter: ");
        string numInput = Console.ReadLine();

        double output = 0;
        while (!double.TryParse(numInput, out output))
        {
            Console.Write("This is not valid input. Please enter a number: ");
            numInput = Console.ReadLine();
        }

        return output;
    }

    public static int AskForInt()
    {
        Console.Write("Type number, and then press Enter: ");
        string numInput = Console.ReadLine();

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
            Console.Write($"This is not valid input. Please select a number from the following list range (1 - {calculationsCount}): ");
            index = AskForInt();
        }

        return index;
    }

    public static string GetOperation()
    {
        string operation = Console.ReadLine();
        return operation;
    }
}

namespace CalculatorProgram;

internal class Helpers
{
    internal static double GetNumber(string message)
    {
        string? numInput = "";
        Console.Write(message);
        numInput = Console.ReadLine();

        double cleanNum = 0;
        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput = Console.ReadLine();
        }

        return cleanNum;
    }
}
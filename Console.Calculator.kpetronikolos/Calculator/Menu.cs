namespace CalculatorProgram;

public static class Menu
{
    public static void DisplayMainMenu()
    {
        // Wait for the user to respond before closing.
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\tp - Perform Calculation");
        Console.WriteLine("\tc - Count the amount of times the calculator was used");
        Console.WriteLine("\tq - Quit the App");
        Console.Write("Enter your option: ");
    }
    public static void DisplayCalculationMenu()
    {
        // Ask the user to choose an operator.
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.Write("Your option? ");
    }
}

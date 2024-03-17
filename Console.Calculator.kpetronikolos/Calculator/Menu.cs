namespace CalculatorProgram;

public static class Menu
{
    public static void DisplayMainMenu()
    {
        // Wait for the user to respond before closing.
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\tp - Perform Calculation");
        Console.WriteLine("\tc - Count the amount of times the calculator was used");
        Console.WriteLine("\tv - View a list of the calculations");
        Console.WriteLine("\td - Delete the list of the calculations");
        Console.WriteLine("\tq - Quit the App");
        Console.Write("Enter your option: ");
    }
    public static void DisplayCalculationMenu()
    {
        // Ask the user to choose an operator.
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\t+ - Add");
        Console.WriteLine("\t- - Subtract");
        Console.WriteLine("\t* - Multiply");
        Console.WriteLine("\t/ - Divide");
        Console.Write("Your option? ");
    }
}

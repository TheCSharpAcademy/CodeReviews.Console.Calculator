namespace CalculatorProgram;

public static class Menu
{
    public static void DisplayMainMenu()
    {
        // Wait for the user to respond before closing.
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\tp - Perform Calculation");
        Console.WriteLine("\ta - Perform Advanced Calculation");
        Console.WriteLine("\tc - Count the amount of times the calculator was used");
        Console.WriteLine("\tv - View a list of the calculations");
        Console.WriteLine("\td - Delete the list of the calculations");
        Console.WriteLine("\th - Use the results from the calculation list to perform new calculation");
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
        Console.WriteLine("\t^ - Pow");
        Console.Write("Your option? ");
    }

    public static void DisplayAdvancedCalculationMenu()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\tsqrt - Square Root");
        Console.WriteLine("\t10^ - 10^");
        Console.WriteLine("\tsin - Sinus");
        Console.WriteLine("\tcos - CosSinus");
        Console.WriteLine("\ttan - Tangient");
        //Console.WriteLine("\ttan - Tangient"); TODO:sftan()
        Console.Write("Your option? ");
    }

    public static void DisplayCalculationOptions()
    {
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\tp - Perform Calculation");
        Console.WriteLine("\ta - Perform Advanced Calculation");
        Console.Write("Enter your option: ");
    }
}

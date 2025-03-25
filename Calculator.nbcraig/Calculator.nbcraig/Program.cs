Console.WriteLine("--- WELCOME TO C# CALCULATOR ---");
Console.WriteLine("--------------------------------");

// Initialize the two operands
double num1 = 0; double num2 = 0;

// Prompt the user to enter the first number
Console.WriteLine("Enter the first number!");
num1 = Convert.ToDouble(Console.ReadLine());

// Prompt the user to enter the second number
Console.WriteLine("Enter the second number!");
num2 = Convert.ToDouble(Console.ReadLine());

// Prompt the user to choose a operator
Console.WriteLine("Choose an operator from the list!");
Console.WriteLine("\ta - Addition");
Console.WriteLine("\ts - Subtraction");
Console.WriteLine("\tm - Multiplication");
Console.WriteLine("\td - Division");
Console.WriteLine("Your Operator: ");

// Perform operations according to the user's choice

string? operand = Console.ReadLine();

switch (operand.ToLower())
{
    case "a":
        Console.Clear();
        Console.WriteLine($"Result: {num1} + {num2} = {num1 + num2}");
        break;
    case "s":
        Console.Clear();
        Console.WriteLine($"Result: {num1} - {num2} = {num1 - num2}");
        break;
    case "m":
        Console.Clear();
        Console.WriteLine($"Result: {num1} * {num2} = {num1 * num2}");
        break;
    case "d":
        // Prompt the user to enter a non zero divisor until they do so
        while (num2.Equals(0))
        {
            Console.WriteLine("Cannot divide by Zero(0) !");
            Console.WriteLine("HINT: Enter a non-zero divisor!");

            num2 = Convert.ToDouble(Console.ReadLine());
        }
        Console.Clear();
        Console.WriteLine($"Result: {num1} / {num2} = {num1 / num2}");
        break;
    default:
        Console.WriteLine("Please select a suitable operator!");
        break;
}

Console.ReadKey();
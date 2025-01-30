// Declare variables and then initialize to zero.
double num1 = 0; double num2 = 0;

// Display title as the C# console calculator app.
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

// Ask the user to type the first number.
Console.WriteLine("Type a number, and then press Enter");
var num1Parsable = false;
while (!num1Parsable)
{
    try
    {
        num1 = Convert.ToDouble(Console.ReadLine());
        num1Parsable = true;
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Please enter a numeric value!");
    }
}



// Ask the user to type the second number.
Console.WriteLine("Type another number, and then press Enter");
var num2Parsable = false;
while (!num2Parsable)
{
    try
    {
        num1 = Convert.ToDouble(Console.ReadLine());
        num2Parsable = true;
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Please enter a numeric value!");
    }
}

// Ask the user to choose an option.
Console.WriteLine("Choose an option from the following list:");
Console.WriteLine("\ta - Add");
Console.WriteLine("\ts - Subtract");
Console.WriteLine("\tm - Multiply");
Console.WriteLine("\td - Divide");
Console.Write("Your option? ");

// Use a switch statement to do the math.
switch (Console.ReadLine())
{
    case "a":
        Console.WriteLine($"Your result: {num1} + {num2} = " + (num1 + num2));
        break;
    case "s":
        Console.WriteLine($"Your result: {num1} - {num2} = " + (num1 - num2));
        break;
    case "m":
        Console.WriteLine($"Your result: {num1} * {num2} = " + (num1 * num2));
        break;
    case "d":
        // Ask the user to enter a non-zero divisor until they do so
        while (num2 == 0)
        {
            Console.WriteLine("Enter a non-zero divisor: ");
            num2 = Convert.ToDouble(Console.ReadLine());
        }
        Console.WriteLine($"Your result: {num1} / {num2} = " + (num1 / num2));
        break;
}
// Wait for the user to respond before closing.
Console.Write("Press any key to close the Calculator console app...");
Console.ReadKey();
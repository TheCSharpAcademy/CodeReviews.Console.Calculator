static double GetInputDouble()
{
    double aux;
    string answer = Console.ReadLine();
    while (!double.TryParse(answer, out aux))
    {
        Console.WriteLine("This is not an integer");
        answer = Console.ReadLine();
    }
    return aux;
}

static string GetValidOption()
{
    //this mean option can't be more than one char and an option that is not allowed
    string ops = "asmd";
    string op = "";
    bool wh = true;
    while (wh)
    {
        op = Console.ReadLine();
        if (ops.IndexOf(op[0]) == -1 | op.Length > 1)
            Console.WriteLine("No es una opcion valida try again: ");
        else
            wh = false;
    }
    return op;
}

// Declare variables and then initialize to zero.
double num1 = 0; double num2 = 0;

// Display title as the C# console calculator app.
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

// Ask the user to type the first number.
Console.WriteLine("Type a number, and then press Enter");
num1 = GetInputDouble();

// Ask the user to type the second number.
Console.WriteLine("Type another number, and then press Enter");
num2 = GetInputDouble();

// Ask the user to choose an option.
Console.WriteLine("Choose an option from the following list:");
Console.WriteLine("\ta - Add");
Console.WriteLine("\ts - Subtract");
Console.WriteLine("\tm - Multiply");
Console.WriteLine("\td - Divide");
Console.Write("Your option? ");

//pedimos los valores por teclado
string op = GetValidOption();

// Use a switch statement to do the math.
switch (op)
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
        while (num2 == 0)
        {
            Console.WriteLine("Enter a non-zero divisor: ");
            num2 = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine($"Your result: {num1} / {num2} = " + (num1 / num2));
        break;
}
// Wait for the user to respond before closing.
Console.Write("Press any key to close the Calculator console app...");
Console.ReadKey();


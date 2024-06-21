//double a = 42;
//double b = 119;
//double c = a + b;
//Console.WriteLine(c);
//Console.ReadKey();

// Declare variables and initialize them to zero.
double num1 = 0;
double num2 = 0;

// Display title as the C# console calulator app
Console.WriteLine("Alvin's Console Calculator in C#\r");
Console.WriteLine("--------------------------------\n");

// Ask the user to typee the first number
Console.WriteLine("Type a number, and then press Enter");
num1 = Convert.ToDouble(Console.ReadLine());

// Ask the user to typee the second number
Console.WriteLine("Type a number, and then press Enter");
num2 = Convert.ToDouble(Console.ReadLine());

// Ask the user to choose an option.
Console.WriteLine("Choose an option from the following list:");
Console.WriteLine("\ta - Add");
Console.WriteLine("\ts - Subtract");
Console.WriteLine("\tm - Multiply");
Console.WriteLine("\td - divide");
Console.Write("Your option? ");

// Use a switch statement to do the math
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
		// Ask the user to eneter a non-zero divisor
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



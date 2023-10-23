namespace CalculatorProgram;

public class UI
{
	public void ShowTitle()
	{
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.WriteLine("Console Calculator");
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine("------------------------");
	}

	public void ShowOptions()
	{
		Console.Clear();

		Console.WriteLine("Options:");
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("u -> Unary operation (square root, power, trig, 10x)");
		Console.WriteLine("b -> Binary operation (addition, subtraction...)");
		Console.WriteLine("c -> View previous calculations");
		Console.WriteLine("d -> Delete calculations");

		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine("q -> Quit game");
		Console.ForegroundColor = ConsoleColor.White;
	}

	public void Clear()
	{
		Console.Clear();
	}

	public void ShowBinaryOperations()
	{
		Console.WriteLine("Choose an operator from the following list:");
		Console.WriteLine("\ta - Add");
		Console.WriteLine("\ts - Subtract");
		Console.WriteLine("\tm - Multiply");
		Console.WriteLine("\td - Divide");
		Console.Write("Your option? ");
	}

	public void ShowUnaryOperations()
	{
		Console.WriteLine("Choose an operator from the following list:");
		Console.WriteLine("\tsqrt - Square Root");
		Console.WriteLine("\tp - Power");
		Console.WriteLine("\tx - 10x");
		Console.WriteLine("\ts - Sin");
		Console.WriteLine("\tc - Cos");
		Console.WriteLine("\tt - Tan");
		Console.Write("Your option? ");
	}

	public void ShowResult(double result)
	{
		if (double.IsNaN(result))
		{
			Console.WriteLine("This operation will result in a mathematical error.\n");
		}
		else Console.WriteLine("Your result: {0:0.##}\n", result);
	}

	public void ShowError(string message)
	{
		Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + message);
	}

	public void ShowEndText()
	{
		Console.WriteLine("------------------------");
		Console.WriteLine("Press n to close the app.");

		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("Press any key to continue.");
		Console.ForegroundColor = ConsoleColor.White;
	}
}

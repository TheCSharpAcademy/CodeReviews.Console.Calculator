namespace CalculatorProgram;

public class Helper
{
	public static string? GetStringFromUser(string text)
	{
		string? input;

		Console.Write(text);
		input = Console.ReadLine();

		return input;
	}

	public static string? GetOptionFromUser(string text)
	{
		string? input;
		do
		{
			input = GetStringFromUser(text);
		} while (input != "u" && input != "b" && input != "c" && input != "q" && input != "d");

		return input;
	}

	public static double GetNumberFromUser(string text)
	{
		double number;

        Console.WriteLine(text);
        string? input = Console.ReadLine();

		while (!double.TryParse(input, out number))
		{
			Console.Write("This is not valid input. Please enter an integer value: ");
			input = Console.ReadLine();
		}

		return number;
	}
}

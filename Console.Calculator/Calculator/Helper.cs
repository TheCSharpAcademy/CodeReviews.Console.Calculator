using CalculatorLibrary;

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

	public static double GetNumberFromCalculations(Calculator calculator)
	{
		calculator.ShowCalculations(isDecorative: false);

		int index;
		do
		{
			index = (int)GetNumberFromUser("Choose the index for the number you want to select: ");
		} while (index < 0 || index >= calculator.Calculations.Count);

		double result = calculator.Calculations[index];

		Console.WriteLine($"You chose {result}.");
		return result;
    }

	public static double GetNumber(Calculator calculator, string promptText)
	{
		if (Console.ReadLine() == "y" && !calculator.IsEmptyCalculations())
		{
			return GetNumberFromCalculations(calculator);
		}
		else
		{
			return GetNumberFromUser(promptText);
		}
	}
}

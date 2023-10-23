using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
	static void Main(string[] args)
	{
		bool endApp = false;
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.WriteLine("Console Calculator");
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine("------------------------");

		Calculator calculator = new Calculator();
		while (!endApp)
		{
			string? numInput1;
			string? numInput2;
			double result;

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

			string? option;
			do
			{
				Console.Write("Choose an option: ");
				option = Console.ReadLine();
			} while (option != "u" && option != "b" && option != "c" && option != "q" && option != "d");

			if (option == "q")
			{
				Environment.Exit(0);
			}
			else if (option == "c")
			{
				calculator.ShowCalculations();
				continue;
			} else if (option == "d")
			{
				calculator.ClearCalculations();
				continue;
			}

			Console.Clear();

			Console.Write("Type a number, and then press Enter: ");
			numInput1 = Console.ReadLine();

			double cleanNum1;
			while (!double.TryParse(numInput1, out cleanNum1))
			{
				Console.Write("This is not valid input. Please enter an integer value: ");
				numInput1 = Console.ReadLine();
			}

			double cleanNum2;
			if (option == "b")
			{
				Console.Write("Type another number, and then press Enter: ");
				numInput2 = Console.ReadLine();

				while (!double.TryParse(numInput2, out cleanNum2))
				{
					Console.Write("This is not valid input. Please enter an integer value: ");
					numInput2 = Console.ReadLine();
				}

				Console.WriteLine("Choose an operator from the following list:");
				Console.WriteLine("\ta - Add");
				Console.WriteLine("\ts - Subtract");
				Console.WriteLine("\tm - Multiply");
				Console.WriteLine("\td - Divide");
				Console.Write("Your option? ");

				string? op = Console.ReadLine();

				try
				{
					result = calculator.DoOperation(cleanNum1, cleanNum2, op);
					if (double.IsNaN(result))
					{
						Console.WriteLine("This operation will result in a mathematical error.\n");
					}
					else Console.WriteLine("Your result: {0:0.##}\n", result);
				}
				catch (Exception e)
				{
					Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
				}
			}
			else if (option == "u")
			{
				Console.WriteLine("Choose an operator from the following list:");
				Console.WriteLine("\tsqrt - Square Root");
				Console.WriteLine("\tp - Power");
				Console.WriteLine("\tx - 10x");
				Console.WriteLine("\ts - Sin");
				Console.WriteLine("\tc - Cos");
				Console.WriteLine("\tt - Tan");
				Console.Write("Your option? ");

				string? op = Console.ReadLine();

				try
				{
					result = calculator.DoOperation(cleanNum1, op);
					if (double.IsNaN(result))
					{
						Console.WriteLine("This operation will result in a mathematical error.\n");
					}
					else Console.WriteLine("Your result: {0:0.##}\n", result);
				}
				catch (Exception e)
				{
					Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
				}
			}

			Console.WriteLine("------------------------");
			Console.WriteLine("Press n to close the app.");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Press any key to continue.");
			Console.ForegroundColor = ConsoleColor.White;

			if (Console.ReadLine() == "n") endApp = true;
		}

		calculator.ShowUsedCount();
		calculator.Finish();
		return;
	}
}
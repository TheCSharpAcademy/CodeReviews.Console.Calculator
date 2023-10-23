using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
	static void Main(string[] args)
	{
		bool endApp = false;
		Console.WriteLine("Console Calculator in C#\r");
		Console.WriteLine("------------------------\n");

		Calculator calculator = new Calculator();
		while (!endApp)
		{
			string? numInput1;
			string? numInput2;
			double result;

			Console.WriteLine("Is this a unary operation (square root, power, trig) or binary (addition, subtraction...)");
            Console.WriteLine("1 -> Unary\n0 -> Binary");

			int operationTypeClean;
			string? operationType = Console.ReadLine();

			while (!int.TryParse(operationType, out operationTypeClean) || operationTypeClean != 1 && operationTypeClean != 0)
			{
				Console.Write("This is not valid input. Please enter 1 or 0: ");
				operationType = Console.ReadLine();
			}

            Console.Write("Type a number, and then press Enter: ");
			numInput1 = Console.ReadLine();

			double cleanNum1;
			while (!double.TryParse(numInput1, out cleanNum1))
			{
				Console.Write("This is not valid input. Please enter an integer value: ");
				numInput1 = Console.ReadLine();
			}

			double cleanNum2;
			if (operationTypeClean == 0)
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
			} else
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

			Console.WriteLine("------------------------\n");

			Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
			if (Console.ReadLine() == "n") endApp = true;

			Console.WriteLine("\n");
		}

		calculator.ShowUsedCount();
		calculator.Finish();
		return;
	}
}
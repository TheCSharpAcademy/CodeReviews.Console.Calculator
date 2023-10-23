using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
	static void Main(string[] args)
	{
		bool endApp = false;
		Calculator calculator = new Calculator();
		UI ui = new UI();

		ui.ShowTitle();

		while (!endApp)
		{
			double result;

			ui.ShowOptions();
			string? option = Helper.GetOptionFromUser("Choose an option: ");

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

			ui.Clear();

			double num1 = Helper.GetNumberFromUser("Type a number, and then press Enter: ");
			double num2;

			if (option == "b")
			{
				num2 = Helper.GetNumberFromUser("Type another number, and then press Enter: ");
				ui.ShowBinaryOperations();

				string? op = Helper.GetStringFromUser("Enter option: ");
				try
				{
					result = calculator.DoOperation(num1, num2, op);
					ui.ShowResult(result);
				}
				catch (Exception e)
				{
					ui.ShowError(e.Message);
				}
			}
			else if (option == "u")
			{
				ui.ShowUnaryOperations();

				string? op = Helper.GetStringFromUser("Enter option: ");
				try
				{
					result = calculator.DoOperation(num1, op);
					ui.ShowResult(result);
				}
				catch (Exception e)
				{
					ui.ShowError(e.Message);
				}
			}

			ui.ShowEndText();
			if (Console.ReadLine() == "n") endApp = true;
		}

		calculator.ShowUsedCount();
		calculator.Finish();
		return;
	}
}
using CalculatorLibrary;
using CalculatorFuriax;

namespace CalculatorProgram
{

	class Program
	{
		static void Main(string[] args)
		{

			bool endApp = false;
			bool reuseResult = false;
			int counter = 0;

			Calculator calculator = new Calculator();
			while (!endApp)
			{
				// Declare variables and set to empty.

				double num1;
				double num2;
				double result = 0;

				// get operand trough menu

				string operand = Helpers.Menu();

				//close off app
				if (operand == "q")
				{ endApp = true; break; }

				// Ask user for a number or set num1 to previous result if selected.
				if (reuseResult == true)
				{
					num1 = Calculator.GetLatestResult();
					reuseResult = false;
				}
				else
					num1 = Helpers.GetNumber();

				//do math based on operand
				if (operand == "t")
				{
					string operand2 = Helpers.Trigonometry();
					result = calculator.GetResult(num1, operand2);

				}
				else if (operand == "r" || operand == "x")
				{
					result = calculator.GetResult(num1, operand);

				}
				else if (operand == "a" || operand == "s" || operand == "m" || operand == "d" || operand == "p")
				{
					// Ask the user for a second number.
					num2 = Helpers.GetNumber();

					// do the math

					result = calculator.GetResult(num1, num2, operand);

				}
				Console.WriteLine("------------------------\n");

				counter++;
				// Ask to quit or show list.
				Console.Write("Press 'q' and Enter to close the app \nPress 'l' to Show List \nor press any other key and Enter to continue: ");
				string input = Console.ReadLine();
				if (input == "q")
				{
					endApp = true;
				}
				else if (input == "l")
				{
					Calculator.ViewList();
					Console.WriteLine("Do you want to reuse the latest result for a new calculation ? (y/n)");
					if (Console.ReadLine() == "y")
					{
						reuseResult = true;
					}
					else
					{
						Console.WriteLine("Do you want to delete the list ? (y/n)");
						if (Console.ReadLine() == "y")
							Calculator.ClearList();
					}
				}
			}
			Console.WriteLine($"You used the calculator {counter} times.");
			calculator.Finish();
			return;
		}
	}
}
using CalculatorLibrary;
using System.Text.RegularExpressions;
using static CalculatorLibrary.Calculator;

namespace CalculatorProgram
{
	class Program
	{
		static private List<CalculationData> calculationHistory = new List<CalculationData>();
		static Calculator calculator = new Calculator();
		static bool endApp = false;

		static void Main(string[] args)
		{
			Console.CancelKeyPress += OnExit;
			AppDomain.CurrentDomain.ProcessExit += OnExit;

			while (!endApp)
			{
				ShowMainMenu();
			}

			//calculator.Finish();
			return;
		}

		private static void ShowMainMenu()
		{
			Console.WriteLine("Console Calculator in C#\r");
			Console.WriteLine($"This app has been run {calculator.RunCount} times.");
			Console.WriteLine("------------------------\n");
			Console.WriteLine("Welcome to the calculator, choose an option:");
			Console.WriteLine("\t1. Do a calculation with 2 numbers (Add, subtract, multiply or divide)");
			Console.WriteLine("\t2. Do a calculation on 1 number (square, root or 10x)");
			Console.WriteLine("\t3. Show operation history");
			Console.WriteLine("\t4. Delete operation history");
			Console.WriteLine("\t0. Quit");
			Console.Write("Your option? ");

			string? menuChoice = Console.ReadLine();

			if (menuChoice.Length == 1 && char.IsDigit(menuChoice[0]))
			{
				int userInput = int.Parse(menuChoice);

				switch (userInput)
				{
					case 1:
						DoCalculationForTwoNumbers();
						break;
					case 2:
						DoCalculationForOneNumber();
						break;
					case 3:
						ShowCalculationHistory();
						break;
					case 4:
						DeleteHistory();
						break;
					case 0:
						endApp = true;
						break;
				}
			}
			else
			{
				Console.Write("Invalid input, try again.");
				Console.ReadLine();
			}
		}

		private static void ShowCalculationHistory()
		{
			for (int i = 0; i < calculationHistory.Count; i++)
			{

				if (calculationHistory[i].num2.HasValue)
				{
					Console.WriteLine($"{i}. " +
					$"{calculationHistory[i].num1} " +
					$"{calculationHistory[i].operand} " +
					$"{calculationHistory[i].num2} = " +
					$" {calculationHistory[i].finalResult} "
					);
				}
				else
				{
					Console.WriteLine($"{i}. " +
					$"{calculationHistory[i].num1} " +
					$"{calculationHistory[i].operand} " +
					$" {calculationHistory[i].finalResult} "
					);
				}

				// We only want 9 most recent entries
				if (i > 9)
				{
					break;
				}
			}

			Console.WriteLine("Which calculation would you like to access?  Enter a number...");
			string calculationSelection = Console.ReadLine();
			HandleHistorySelection(calculationSelection);
		}

		// Handles what happens when the user selects an entry from the calculation history
		private static void HandleHistorySelection(string calculationSelection)
		{
			if (calculationSelection.Length == 1 && char.IsDigit(calculationSelection[0]))
			{
				int userInput = int.Parse(calculationSelection);

				if(calculationHistory[userInput].num2.HasValue)
				{
					DoCalculationForTwoNumbers(calculationHistory[userInput].num1.ToString(), calculationHistory[userInput].num2.ToString());
				}
				else
				{
					DoCalculationForOneNumber(calculationHistory[userInput].num1.ToString());
				}
			}
		}

		private static void DeleteHistory()
		{
			calculationHistory.Clear();
		}

		// Calculation for square root, power and 10x
		private static void DoCalculationForOneNumber(string numInput1 = "")
		{
			double result = 0;

			Console.Write("Type a number, and then press Enter: ");

			if (string.IsNullOrEmpty(numInput1))
			{
				numInput1 = Console.ReadLine();
			}
			else
			{
				numInput1 = ReadLine(numInput1);
			}

			double cleanNum1 = 0;
			while (!double.TryParse(numInput1, out cleanNum1))
			{
				Console.Write("This is not valid input. Please enter an integer value: ");
				numInput1 = Console.ReadLine();
			}

			Console.WriteLine("Choose an operator from the following list:");
			Console.WriteLine("\tr - Square Root");
			Console.WriteLine("\tp - Power");
			Console.WriteLine("\tt - 10x");
			Console.Write("Your option? ");

			string? op = Console.ReadLine();

			// Validate input is not null, and matches the pattern
			if (op == null || !Regex.IsMatch(op, "[r|p|t]"))
			{
				Console.WriteLine("Error: Unrecognized input.");
			}
			else
			{
				try
				{
					result = calculator.DoOperationForOneNumber(cleanNum1, op);
					CalculationData calcData = new CalculationData(cleanNum1, null, calculator.GetSymbolForCalculation(op), result);
					calculationHistory.Insert(0, calcData);

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
			Console.WriteLine("Press any key to return to the main menu.\n");

			Console.ReadLine();
			Console.Clear();
		}

		static void DoCalculationForTwoNumbers(string numInput1 =  "", string numInput2 = "")
		{
			double result = 0;

			Console.Write("Type a number, and then press Enter: ");

			if (string.IsNullOrEmpty(numInput1))
			{
				numInput1 = Console.ReadLine();
			}
			else
			{
				numInput1 = ReadLine(numInput1);
			}

			double cleanNum1 = 0;
			while (!double.TryParse(numInput1, out cleanNum1))
			{
				Console.Write("This is not valid input. Please enter an integer value: ");
				numInput1 = Console.ReadLine();
			}

			Console.Write("Type a number, and then press Enter: ");

			if (string.IsNullOrEmpty(numInput2))
			{
				numInput2 = Console.ReadLine();
			}
			else
			{
				numInput2 = ReadLine(numInput2);
			}

			double cleanNum2 = 0;
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

			// Validate input is not null, and matches the pattern
			if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
			{
				Console.WriteLine("Error: Unrecognized input.");
			}
			else
			{
				try
				{
					result = calculator.DoOperationForTwoNumbers(cleanNum1, cleanNum2, op);
					CalculationData calcData = new CalculationData(cleanNum1, cleanNum2, calculator.GetSymbolForCalculation(op), result);
					calculationHistory.Insert(0, calcData);

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
			Console.WriteLine("Press any key to return to the main menu.\n");

			Console.ReadLine();
			Console.Clear();
		}

		// Allows for reading a console input with prefilled text
		static string ReadLine(string prefill)
		{
			Console.Write(prefill);
			string input = prefill;
			int cursorPosition = prefill.Length;

			while (true)
			{
				ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // Read key without displaying

				if (keyInfo.Key == ConsoleKey.Enter)
				{
					Console.WriteLine(); // Move to the next line
					return input; // Finish input
				}
				else if (keyInfo.Key == ConsoleKey.Backspace)
				{
					if (cursorPosition > 0)
					{
						input = input.Remove(cursorPosition - 1, 1);
						cursorPosition--;

						Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
						Console.Write(input.Substring(cursorPosition) + " "); // Rewrite everything after the cursor
						Console.SetCursorPosition(Console.CursorLeft - input.Length + cursorPosition - 1, Console.CursorTop);
					}
				}
				else if (keyInfo.Key == ConsoleKey.Delete)
				{
					if (cursorPosition < input.Length)
					{
						input = input.Remove(cursorPosition, 1);

						Console.Write(input.Substring(cursorPosition) + " "); // Rewrite everything after the cursor
						Console.SetCursorPosition(Console.CursorLeft - input.Length + cursorPosition, Console.CursorTop);
					}
				}
				else if (keyInfo.Key == ConsoleKey.LeftArrow)
				{
					if (cursorPosition > 0)
					{
						cursorPosition--;
						Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
					}
				}
				else if (keyInfo.Key == ConsoleKey.RightArrow)
				{
					if (cursorPosition < input.Length)
					{
						cursorPosition++;
						Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
					}
				}
				else
				{
					char newChar = keyInfo.KeyChar;
					input = input.Insert(cursorPosition, newChar.ToString());
					cursorPosition++;

					Console.Write(input.Substring(cursorPosition - 1));
					Console.SetCursorPosition(Console.CursorLeft - input.Length + cursorPosition, Console.CursorTop);
				}
			}

			return input;
		}

		static void OnExit(object sender, EventArgs e)
		{
			calculator.Finish();
		}
	}
}
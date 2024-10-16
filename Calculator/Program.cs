using System.Text.RegularExpressions;

using CalculatorLibrary;

// Program.cs
namespace CalculatorProgram
{
	using System;
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	public class Program
	{
		public static void Main(string[] args)
		{
			List<string> calculationHistory = new List<string>();
			List<double> results = new();
			bool endApp = false;
			int count = 0;
			double result = 0; // Initialize result variable here.

			// Display title as the C# console calculator app.
			Console.WriteLine("Console Calculator in C#\r");
			Console.WriteLine("------------------------\n");

			Calculator calculator = new Calculator();
			while (!endApp)
			{  Console.Clear();
				// Declare variables
				double cleanNum1 = 0;
				double cleanNum2 = 0;
				string? numInput1;
				string? numInput2;

				// Ask the user to choose an operator
				Console.WriteLine("Choose an operator from the following list:");
				Console.WriteLine("\ta - Add");
				Console.WriteLine("\ts - Subtract");
				Console.WriteLine("\tm - Multiply");
				Console.WriteLine("\td - Divide");
				Console.WriteLine("\tsqrt - Square Root");
				Console.WriteLine("\tpow - Power");
				Console.WriteLine("\ttenx - 10^x");
				Console.WriteLine("\tsin - Sine");
				Console.WriteLine("\tcos - Cosine");
				Console.Write("Your option? ");

				string? op = Console.ReadLine();

				// Check if the operation is valid
				if (op == null || !Regex.IsMatch(op, @"^(a|s|m|d|sqrt|pow|tenx|sin|cos)$"))
				{
					Console.WriteLine("Error: Unrecognized input.");
					continue; // Skip to the next iteration of the loop
				}

				// Check if the calculation history has results to reuse
				if (calculationHistory.Count > 0)
				{
					Console.Write("Do you want to use a result from previous operations (y/n)? ");
					string useLastResult = Console.ReadLine();

					if (useLastResult?.ToLower() == "y")
					{
						Console.Clear();
						ShowList(calculationHistory);
						Console.Write("Enter the number of the operation to use its result: ");
						string userInput = Console.ReadLine();
						int operationIndex;

						// Validate the input for an integer and range
						while (!int.TryParse(userInput, out operationIndex) || operationIndex < 1 || operationIndex > calculationHistory.Count)
						{
							Console.Write("Invalid input. Please enter a valid operation number: ");
							userInput = Console.ReadLine();
						}

						// Retrieve the previous result
						cleanNum1 = results[operationIndex - 1]; // Use the selected result as the first number
						Console.WriteLine($"Using previous result: {cleanNum1}");
					}
					else
					{
						// If the user does not want to use the previous result, ask for the first number
						Console.Write("Enter the first number: ");
						numInput1 = Console.ReadLine();

						// Validate the first number
						while (!double.TryParse(numInput1, out cleanNum1))
						{
							Console.Write("This is not valid input. Please enter a valid number: ");
							numInput1 = Console.ReadLine();
						}
					}
				}
				else
				{
					// No previous operations, so ask for the first number
					Console.Write("Enter the first number: ");
					numInput1 = Console.ReadLine();

					// Validate the first number
					while (!double.TryParse(numInput1, out cleanNum1))
					{
						Console.Write("This is not valid input. Please enter a valid number: ");
						numInput1 = Console.ReadLine();
					}
				}

				// Check if the operation requires a second number
				if (op != "sqrt" && op != "tenx" && op != "sin" && op != "cos")
				{
					// Get the second number
					Console.Write("Enter the second number: ");
					numInput2 = Console.ReadLine();

					// Validate the second number
					while (!double.TryParse(numInput2, out cleanNum2))
					{
						Console.Write("Invalid input for the second number. Please enter a valid number: ");
						numInput2 = Console.ReadLine();
					}
				}

				// Perform the operation
				try
				{
					result = calculator.DoOperation(cleanNum1, cleanNum2, op); // Ensure this is properly defined in the Calculator class
					if (double.IsNaN(result))
					{
						Console.WriteLine("This operation will result in a mathematical error.\n");
					}
					else
					{
						count++;
						Console.WriteLine("Your result: {0:0.##}\n", result);
						Console.WriteLine($"Calculator has been used {count} times.");
						AddToList(calculationHistory, cleanNum1, result, op, cleanNum2);
						results.Add(result); // Add result to the results list
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
				}

				Console.WriteLine("------------------------\n");

				// Handle app closing or showing history
				Console.Write("Press 'n' and Enter to close the app, 's' to show history or press any other key and Enter to continue: ");
				string userInput2 = Console.ReadLine();
				if (userInput2 == "n") endApp = true;
				else if (userInput2 == "s")
				{
					ShowList(calculationHistory);
					Console.WriteLine("Do you want to delete the list? (y/n)");
					string? userAnswer = Console.ReadLine().ToLower();
					if (userAnswer == "y") DeleteList(calculationHistory);
				}

				Console.WriteLine("\n"); // Friendly linespacing.
			}

			// Helper methods for adding, showing, and deleting history
			static void AddToList(List<string> ls, double num1, double res, string op, double num2 = 0)
			{
				string operatorSymbol = "";
				string s = ""; // Initialize 's' here to ensure it's in scope.

				switch (op)
				{
					case "a":
						operatorSymbol = "+";
						break;
					case "d":
						operatorSymbol = "/";
						break;
					case "s":
						operatorSymbol = "-";
						break;
					case "m":
						operatorSymbol = "*";
						break;
					case "sqrt":
						s = $"√{num1} = {res}";
						break;
					case "pow":
						s = $"{num1} ^ {num2} = {res}";
						break;
					case "tenx":
						s = $"10^{num1} = {res}";
						break;
					case "sin":
						s = $"sin({num1}) = {res}";
						break;
					case "cos":
						s = $"cos({num1}) = {res}";
						break;
					default:
						Console.WriteLine("Error: Invalid operation");
						return; // Exit if operation is invalid.
				}

				// If operatorSymbol is not empty, build the standard operation string.
				if (!string.IsNullOrEmpty(operatorSymbol))
				{
					s = $"{num1} {operatorSymbol} {num2} = {res}";
				}

				ls.Add(s);
			}

			static void ShowList(List<string> ls)
			{
				if (ls.Count == 0)
				{
					Console.WriteLine("No calculations have been performed yet.");
					return;
				}

				for (int i = 0; i < ls.Count; i++)
				{
					Console.WriteLine($"{i + 1}: {ls[i]}");
				}
			}

			static void DeleteList(List<string> ls)
			{
				ls.Clear();
				Console.WriteLine("The history list has been cleared.");
			}
		}
	}
}
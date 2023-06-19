
namespace CalculatorFuriax
{
	public class Helpers
	{

		public static string Menu()
		{
			bool isValid = false;
			Console.Clear();
			Console.WriteLine("Console Calculator in C#\r");
			Console.WriteLine("------------------------\n");
			// Ask the user to choose an operator.
			string output;
			do
			{
				Console.WriteLine("What math operation do you want to do:");
				Console.WriteLine("\ta - Add");
				Console.WriteLine("\ts - Subtract");
				Console.WriteLine("\tm - Multiply");
				Console.WriteLine("\td - Divide");
				Console.WriteLine("\tp - Taking The Power");
				Console.WriteLine("\tx - x10");
				Console.WriteLine("\tr - Take Square Root");
				Console.WriteLine("\tt - Trigonometry functions");
				Console.WriteLine("\tq - Quit Program");
				Console.Write("Your option? ");
				output = Console.ReadLine();

				if (output == "a" || output == "s" || output == "m" || output == "d" || output == "q" ||
					output == "p" || output == "x" || output == "r" || output == "t")
					isValid = true;
				else
					Console.WriteLine("Invalid input, enter a correct option");

			} while (isValid == false);
			return output;
		}
		public static string Trigonometry()
		{
			bool isValid = false;
			string output;
			do
			{
				Console.WriteLine("Which Trigonometry function do you want to calculate:");
				Console.WriteLine("\tsin - Sine");
				Console.WriteLine("\tcos - Cosine");
				Console.WriteLine("\ttan - Tangent");
				Console.Write("Your option? ");
				output = Console.ReadLine();

				switch (output)
				{
					case "sin": isValid = true; break;
					case "cos": isValid = true; break;
					case "tan": isValid = true; break;
					default: Console.WriteLine("invalid input, try again"); break;
				}

			} while (isValid == false);
			return output;
		}

		public static double GetNumber()
		{
			string numInput;
			Console.Write("Type a number, and then press Enter: ");
			numInput = Console.ReadLine();

			double cleanNum = 0;
			while (!double.TryParse(numInput, out cleanNum))
			{
				Console.Write("This is not valid input. Please enter a numeric value: ");
				numInput = Console.ReadLine();
			}
			return cleanNum;
		}
	}
}

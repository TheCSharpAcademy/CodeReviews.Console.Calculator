namespace CalculatorLibrary
{
    public class CalculatorList
	{
		public void CountCalculations()
		{
			if (File.Exists("Usage.txt"))
			{
				string initialNumber = File.ReadAllText("Usage.txt");
				int.TryParse(initialNumber, out int count);
				count++;
				File.WriteAllText("Usage.txt", count.ToString());
			}
			else
			{
				int count = 1;
				File.WriteAllText("Usage.txt", count.ToString());
			}
		}
		public int ReturnCount()
		{
			if (File.Exists("Usage.txt"))
			{
				string currentCount = File.ReadAllText("Usage.txt");
				int.TryParse(currentCount, out int countInteger);
				return countInteger;
			}
			else
			{
				return 0;
			}
		}

		public void WriteHistory(double num1, double num2, string operation, double result)
		{
			operation = DoSwitch(operation);
            string newline = "";
            if (File.Exists("History.txt"))
			{
				newline = "\n";
			}
			
			File.AppendAllText("History.txt", $"{newline}{num1} {operation} {num2} = {result}");
		}

		private string DoSwitch(string op)
		{
            switch (op)
            {
                case "a":
                    return "+";
                case "s":
                    return "-";
                case "m":
                    return "*";
                case "d":
                    return "/";
				case "r":
					return "sqroot";
            }
			return null;
        }

		public void ViewHistory()
		{
			if (File.Exists("History.txt"))
			{
				string[] history = File.ReadAllLines("History.txt");
				for (int i = 0; i < history.Length; i++)
				{
					Console.WriteLine($"{i + 1}. {history[i]}");
				}
			}
			else
			{
				Console.WriteLine("No history");
			}
		}

		public int GetNumber()
		{
			string[] history = File.ReadAllLines("History.txt");
			ViewHistory();
			int lineNumber = 0;
			while (lineNumber < 1 || lineNumber > history.Length)
			{
				Console.WriteLine("Please choose a result for your integer");
				int.TryParse(Console.ReadLine(), out lineNumber);
			}
			string[] numberToget = history[lineNumber - 1].Split(" ");
			int.TryParse(numberToget[4], out int number);
			return number;
		}
	}
}



namespace ConsoleApp
{
	class Calculator
	{
		static void Main(string[] args)
		{
			Console.WriteLine("type your name");
			var name = Console.ReadLine();
			Console.WriteLine($"\nhello {name.ToUpper()}! thanks for playing.");
			bool flag = true;
			while (flag)
			{
				Console.WriteLine("-------------------------------");
				Console.WriteLine("choose an operation: (a)ddition, (s)ubtraction, (m)ultiplication, (d)ivision, (q)uit");
				Console.WriteLine("-------------------------------");
				var choice = Console.ReadLine().ToLower();

				switch (choice)
				{
					case "a":
						Console.WriteLine("you picked addition! enter two numbers.");
						Console.WriteLine("first number: ");
						var firstA = Convert.ToInt32(Console.ReadLine());
						Console.WriteLine("second number: ");
						var secondA = Convert.ToInt32(Console.ReadLine());
						Console.WriteLine($"both numbers added is {firstA + secondA}");
						break;
					case "s":
						Console.WriteLine("you picked subtraction! enter two numbers.");
						Console.WriteLine("first number: ");
						var firstS = Convert.ToInt32(Console.ReadLine());
						Console.WriteLine("second number: ");
						var secondS = Convert.ToInt32(Console.ReadLine());
						Console.WriteLine($"both numbers subtracted is {firstS - secondS}");
						break;
					case "m":
						Console.WriteLine("you picked multiplication! enter two numbers.");
						Console.WriteLine("first number: ");
						var firstM = Convert.ToInt32(Console.ReadLine());
						Console.WriteLine("second number: ");
						var secondM = Convert.ToInt32(Console.ReadLine());
						Console.WriteLine($"both numbers multiplied is {firstM * secondM}");
						break;
					case "d":
						Console.WriteLine("you picked division! enter two numbers.");
						Console.WriteLine("first number: ");
						var firstD = Convert.ToDouble(Console.ReadLine());
						Console.WriteLine("second number: ");
						var secondD = Convert.ToDouble(Console.ReadLine());
						Console.WriteLine($"both numbers divided is {firstD / secondD}");
						break;
					case "q":
						Environment.Exit(0);
						flag = false;
						break;
					default:
						Console.WriteLine("invalid choice. select a valid operation.");
						break;
				}
			}
		}
	}
}
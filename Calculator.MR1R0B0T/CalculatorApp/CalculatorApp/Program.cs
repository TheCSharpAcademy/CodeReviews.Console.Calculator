namespace CalculatorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console Calculator Game");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Please enter a key to continue. . .");
            Console.ReadLine();
            bool showMenu = true;
            while (showMenu)
            {
                Menu();
            }
        }

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\tHere are the menus for the game: ");
            string menu = $"\t\t\t\t\t\t 1 - Addition \n \t\t\t\t\t\t 2 - Subtraction \n \t\t\t\t\t\t 3 - Multiplication \n \t\t\t\t\t\t 4 - Division \n\n\n";
            Console.WriteLine(menu);
            Choice();
        }

        public static void Choice()
        {
            string choiceMessage = "Please input the type of game you want to play: ";
            Console.Write(choiceMessage);
            var choices = Console.ReadLine();

            switch (choices)
            {
                case "1":
                    Console.Clear();
                    AdditionGame();
                    break;
                case "2":
                    Console.Clear();
                    SubtractionGame();
                    break;
                case "3":
                    Console.Clear();
                    MultiplicationGame();
                    break;
                case "4":
                    Console.Clear();
                    DivisionGame();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Out of bounds. Please try again.");
                    Environment.Exit(0);
                    break;
            }
        }

        //Addition
        public static void AdditionGame()
        {
            Console.WriteLine("\t\t\t\t\t\tWelcome to the Addition Game\n\n\n");
            Console.WriteLine("Please input the 2 numbers: ");
            Console.Write("First Number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Second Number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());
            int sum = num1 + num2;
            Console.WriteLine($"The sum of the numbers, {num1} and {num2} is: {sum}");
            Console.WriteLine("Please enter a key to continue. . .");
            Console.ReadLine();
        }

        //Subtraction
        public static void SubtractionGame()
        {
            Console.WriteLine("\t\t\t\t\t\tWelcome to the Subtraction Game\n\n\n");
            Console.WriteLine("Please input the 2 numbers: ");
            Console.Write("First Number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Second Number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());
            int difference = num1 - num2;
            Console.WriteLine($"The difference of the numbers, {num1} and {num2} is: {difference}");
            Console.WriteLine("Please enter a key to continue. . .");
            Console.ReadLine();
        }

        //Multiplication
        public static void MultiplicationGame()
        {
            Console.WriteLine("\t\t\t\t\t\tWelcome to the Multiplication Game\n\n\n");
            Console.WriteLine("Please input the 2 numbers: ");
            Console.Write("First Number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Second Number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());
            int product = num1 * num2;
            Console.WriteLine($"The product of the numbers, {num1} and {num2} is: {product}");
            Console.WriteLine("Please enter a key to continue. . .");
            Console.ReadLine();
        }

        //Division
        public static void DivisionGame()
        {
            Console.WriteLine("\t\t\t\t\t\tWelcome to the Division Game\n\n\n");
            Console.WriteLine("Please input the 2 numbers: ");
            Console.Write("First Number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Second Number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());
            int quotient = num1 / num2;
            Console.WriteLine($"The quotient of the numbers, {num1} and {num2} is: {quotient}");
            Console.WriteLine("Please enter a key to continue. . .");
            Console.ReadLine();
        }
    }
}
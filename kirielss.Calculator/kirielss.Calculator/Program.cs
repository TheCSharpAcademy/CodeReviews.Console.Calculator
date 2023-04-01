using System.Globalization;

namespace kirielss.Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool uptime = true;
            double x = 0; double y = 0;
            bool check;
            char selector;

            Console.WriteLine("WELCOME TO KIRIEL'S CALCULATOR!");
            Console.WriteLine("-------------------------------");

            while (uptime)
            {
                Console.WriteLine("First insert two numbers");
                Console.Write("X = ");
                check = double.TryParse((Console.ReadLine()), out x);
                while (check == false)
                {
                    Console.WriteLine("Insert a valid number!");
                    check = double.TryParse(Console.ReadLine(), out x);
                }
                Console.Write("Y = ");
                check = double.TryParse(Console.ReadLine(), out y);
                while (check == false)
                {
                    Console.WriteLine("Insert a valid number!");
                    check = double.TryParse(Console.ReadLine(), out y);
                }

                Console.Clear();
                Console.WriteLine("Now choose your operation");
                Console.WriteLine("\ta - Addition");
                Console.WriteLine("\ts - Subtraction");
                Console.WriteLine("\tm - Multiplication");
                Console.WriteLine("\td - Division");
                selector = char.Parse(Console.ReadLine().ToLower());

                Console.Clear();
                switch (selector)
                {
                    case 'a':
                        Console.WriteLine($"{x} + {y} = " + (x + y));
                        break;
                    case 's':
                        Console.WriteLine($"{x} - {y} = " + (x - y));
                        break;
                    case 'm':
                        Console.WriteLine($"{x} x {y} = " + (x * y));
                        break;
                    case 'd':
                        if (y != 0)
                        {
                            Console.WriteLine($"{x} / {y} = " + (x / y).ToString("F3", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            while (y == 0)
                            {
                                Console.WriteLine("YOU CAN'T DIVIDE BY ZERO!");
                                Console.WriteLine("Please insert a new value as divisor");
                                double.TryParse(Console.ReadLine(), out y);
                            }
                            Console.WriteLine($"{x} / {y} = " + (x / y).ToString("F3", CultureInfo.InvariantCulture));
                        }
                        break;
                }
                Console.WriteLine("Please insert 'n' to quit, or just press enter to continue");
                if (Console.ReadLine() == "n")
                {
                    uptime = false;
                }
            }



        }
    }
}
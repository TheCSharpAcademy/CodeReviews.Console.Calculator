using System;
using MathOperations = Calculator.Mo3ses.MathOperations;

namespace Calculator.Mo3ses
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int counter = 0;
            int answer = 0;
            bool listValue = true;
            double value1 = 0;
            double value2 = 0;

            MathOperations calculator = new MathOperations();
            do
            {

                bool verify = false;

                while (verify == false)
                {
                    Menu.StartMenu(listValue);
                    Console.Write("Your option? ");
                    if (int.TryParse(Console.ReadLine(), out answer))
                    {
                        verify = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Option, Try Again.");
                    }

                }

                if (verify == true && answer != 9 && answer != 10 && answer != 11)
                {
                    if (listValue == true && answer != 8)
                    {
                        Console.WriteLine("Choose the Value:");
                        value1 = Convert.ToDouble(Console.ReadLine());
                    }

                    switch (answer)
                    {
                        case 5:
                            break;
                        case 6:
                            Console.WriteLine("Choose pow Number:");
                            value2 = Convert.ToDouble(Console.ReadLine());
                            break;
                        case 7:
                        break;
                        case 8:
                        break;
                        default:
                            Console.WriteLine("Choose 2 Value:");
                            value2 = Convert.ToDouble(Console.ReadLine());
                            break;
                    }
                    while (answer == 4 && value2 == 0)
                    {
                        Console.WriteLine("Enter a non-zero divisor: ");
                        value2 = Convert.ToDouble(Console.ReadLine());
                    }
                }

                listValue = Menu.MenuAnswer(answer, value1, value2, calculator);
                Console.WriteLine();
                if (listValue == false)
                {
                    Console.WriteLine("Do you want to use one of the results in the next calculation? (y/n)");
                    if (Console.ReadLine() == "y")
                    {
                        Console.WriteLine("Type the number of the result in the list above to use in the next calculation.");
                        int id = Convert.ToInt32(Console.ReadLine());
                        value1 = Convert.ToDouble(calculator.OperationGetById(id));
                    }
                    else
                    {
                        listValue = true;
                    }
                }
                if (answer != 9 && answer != 10)
                {
                    counter++;
                    Console.WriteLine($"You used the calculator {counter} times.");
                }

            } while (answer != 11);
        }
    }
}
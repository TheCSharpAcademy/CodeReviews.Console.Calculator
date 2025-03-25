namespace CalculatorLibrary
{
    public class Calculator
    {
        public static double HandleOperations(double num1, double num2, string op)
        {
            double result = 0;

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    break;
                case "s":
                    result = num1 - num2;
                    break;
                case "m":
                    result = num1 * num2;
                    break;
                case "d":
                    // Prompt the user to enter a non zero divisor until they do so
                    // We make sure at the same occasion if the input is in correct format
                    while (num2.Equals(0))
                    {
                        Console.WriteLine("Cannot divide by Zero(0) !");
                        Console.WriteLine("HINT: Enter a non-zero divisor!");

                        string num2Input = Console.ReadLine();

                        while (!double.TryParse(num2Input, out num2))
                        {
                            Console.WriteLine("Enter the number in the correct format!");
                            num2Input = Console.ReadLine();
                        }
                    }

                    result = num1 / num2;
                    break;
            }


            return result;
        }
    }
}

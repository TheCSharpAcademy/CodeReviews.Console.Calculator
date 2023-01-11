namespace didntreally_ConsoleCalculator
{
    public class Calculator
    {
        public static double Operation(double num1, double num2, string optr)
        {
            double result = double.NaN;

            switch (optr.ToUpper())
            {
                case "A":
                    result = num1 + num2;
                    break;
                case "S":
                    result = num1 - num2;
                    break;
                case "M":
                    result = num1 * num2;
                    break;
                case "D":
                    //Repeatly ask user to enter a non zero divisor until they do so
                    while (num2 == 0)
                    {
                        Console.WriteLine("Please enter a non zero divisor! ");
                        num2 = Convert.ToDouble(Console.ReadLine());
                    }
                    result = num1 / num2;
                    break;
                default:
                    {
                        Console.WriteLine("Please enter a valid alphabet!");
                        break;
                    }

            }

            return result;
        }
    }
}

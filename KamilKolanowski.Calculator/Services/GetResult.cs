namespace KamilKolanowski.Calculator;

public class GetResult
{
    public decimal GetResultFromOperation(string operation)
            {
                ICalculator calculator = null;
                char choice;
    
                switch (operation.ToLower())
                {
                    case "a":
                        calculator = new Add();
                        choice = '+';
                        break;
                    case "s":
                        calculator = new Sub();
                        choice = '-';
                        break;
                    case "m":
                        calculator = new Mul();
                        choice = '*';
                        break;
                    case "d":
                        calculator = new Div();
                        choice = '/';
                        break;
                    case "q":
                        choice = 'q';
                        break;
                    default:
                        Console.WriteLine("Invalid operation.");
                        return 0;
                }

                try
                {
                    decimal[] nums = GetNumbers();
                    
                    Console.WriteLine(
                        $"Your result is: {calculator.Operation(nums[0], nums[1])}");
                    Console.WriteLine("____________________________________________");
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine($"You can't add such numbers because: {ex.Message}");
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine($"Enter valid numbers!");
                }
    
                return 0;
            }


    public static decimal[]? GetNumbers()
    {
        Console.Write("Enter the first number: ");
        string? input1 = Console.ReadLine();

        Console.Write("Enter the second number: ");
        string? input2 = Console.ReadLine();
        
        try
        {
            decimal firstNumber = decimal.Parse(input1);
            decimal secondNumber = decimal.Parse(input2);

            decimal[] numbers = { firstNumber, secondNumber };
            return numbers;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input!");
            return null;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Number is too large or too small.");
            return null;
        }
    }
}
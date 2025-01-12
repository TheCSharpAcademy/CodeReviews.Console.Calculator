namespace KamilKolanowski.Calculator;

public class GetResult
{
    public decimal GetResultFromOperation(string operation)
            {
                ICalculator calculator = null;
    
                switch (operation.ToLower())
                {
                    case "a":
                        calculator = new Add();
                        break;
                    case "s":
                        calculator = new Sub();
                        break;
                    case "m":
                        calculator = new Mul();
                        break;
                    case "d":
                        calculator = new Div();
                        break;
                    default:
                        Console.WriteLine("Invalid operation.");
                        return 0;
                }
    
                try
                {
                    decimal[] nums = GetNumbers();
    
                    checked
                    {
                        Console.WriteLine($"Your result is: {calculator.Operation(nums[0], nums[1])}");
                    }
                }
                catch (System.OverflowException ex)
                {
                    Console.WriteLine($"You can't add such numbers because: {ex.Message}");
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
    
                return 0;
            }


    public decimal[] GetNumbers()
    {
        Console.Write("Enter the first number: ");
        decimal firstNumber = decimal.Parse(Console.ReadLine());
    
        Console.Write("Enter the second number: ");
        decimal secondNumber = decimal.Parse(Console.ReadLine());
        
        decimal[] numbers = { firstNumber, secondNumber };

        return numbers;
    }
}
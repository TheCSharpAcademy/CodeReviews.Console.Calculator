namespace KamilKolanowski.Calculator;

public class CalculatorApp
{
    public void Calc()
    {
        Console.WriteLine("Welcome to the calculator!");
        
        while (true)
        {
            // Calculator Menu 
            Console.WriteLine("Please specify which operation you want to perform");
            Console.WriteLine("\t a -> Add");
            Console.WriteLine("\t s -> Subtract");
            Console.WriteLine("\t m -> Multiply");
            Console.WriteLine("\t d -> Divide");
            Console.WriteLine("\t q -> Quit");
            //////////////////////////////////////
            Console.Write($"Your choice: ");
            string operation = Console.ReadLine();
            
            if (operation.ToLower() == "q")
            {
                Console.WriteLine("Thank you for using the calculator. Goodbye!");
                break; // Exit the loop
            }

            var res = new GetResult();
            res.GetResultFromOperation(operation);
            
        }
    }
}
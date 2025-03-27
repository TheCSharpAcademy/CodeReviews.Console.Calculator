using CalculatorLibrary;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("--- WELCOME TO C# CALCULATOR ---");
        Console.WriteLine("--------------------------------");

        // New instance/session of Calculator [See CalculatorLibrary].
        // Initialisation is done here because there can only be one
        // instance of the Trace class through the whole app lifecycle.
        Calculator calculator = new Calculator();

        bool calculatorOff = false;

        while (!calculatorOff)
        {
            // Initialize variables to store user input
            string? num1Input = "";
            string? num2Input = "";

            // Prompt the user to enter the first number
            Console.WriteLine("Enter the first number!");
            num1Input = Console.ReadLine();
            // Use TryParse to handle format exceptions 
            double num1 = 0;
            while (!double.TryParse(num1Input, out num1))
            {
                Console.WriteLine("Enter the first number in the correct format!");
                num1Input = Console.ReadLine();
            }

            // Repeat process for the second number
            Console.WriteLine("Enter the second number!");
            num2Input = Console.ReadLine();
            double num2 = 0;
            while (!double.TryParse(num2Input, out num2))
            {
                Console.WriteLine("Enter the second number in the correct format!");
                num2Input = Console.ReadLine();
            }



            // Prompt the user to choose a operator
            Console.WriteLine("Choose an operator from the list!");
            Console.WriteLine("\ta - Addition");
            Console.WriteLine("\ts - Subtraction");
            Console.WriteLine("\tm - Multiplication");
            Console.WriteLine("\td - Division");
            Console.WriteLine("Your Operator: ");

            string? op = Console.ReadLine().ToLower();

            // Verify entered operator and perform operation. Basci block for readability

            string[] allOperators = { "a", "s", "m", "d" };

            while (!allOperators.Contains(op))
            {
                Console.Clear();
                Console.WriteLine("Please enter a suitable operator!");
                Console.WriteLine("Your Operator: ");

                op = Console.ReadLine().ToLower();
            }

            double operationResult = calculator.HandleOperations(num1, num2, op);



            // Show well formatted results
            Console.Clear();
            Console.WriteLine($"RESULT: {operationResult}");


            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") calculatorOff = true;
        }

        // Call the Finish method to close the JSON writer before 
        // end of app 'session'
        calculator.Finish();

        // End loop and exit app if calcultorOff is to be set to true
        return;
    }
}
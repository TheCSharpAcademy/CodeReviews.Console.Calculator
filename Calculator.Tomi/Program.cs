using Calculator.Tomi.CalculatorCore;
using Calculator.Tomi.CalculatorPrompts;

class Program
{
    static void Main(string[] args)
    {


        bool endApp = false;

        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        CalculatorEngine calculatorEngine = new();


        while (!endApp)
        {
            double result = 0;

            string op = Prompts.PromptForOperation();
            List<double> operands = [];

            bool reUseResult = Prompts.AskToReUseResult(calculatorEngine.CanReuseResult);

            int requiredOperandCount = op == "sq" || op == "sqr" ? 1 : 2;

            if (reUseResult)
            {
                if (requiredOperandCount > 1) operands = Prompts.PromptForOperands(1);
            }
            else
            {
                operands = Prompts.PromptForOperands(requiredOperandCount);
            }

            if (calculatorEngine.CanReuseResult && reUseResult) operands.Insert(0, calculatorEngine.PrevResult);

            try
            {
                result = calculatorEngine.DoOperation(operands, op);

                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }

        calculatorEngine.Finish();
        return;

    }
}
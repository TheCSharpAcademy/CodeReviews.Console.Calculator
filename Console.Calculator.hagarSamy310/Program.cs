using System.Text.RegularExpressions;
using CalculatorLibrary;
using Newtonsoft.Json;

namespace CalculatorProgram
{
    class Program
    {
        static void Main()
        {
            bool endApp = false;
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("-----------------------------------------\n");

            Calculator calculator = new Calculator();

            while (!endApp)
            {

                Console.WriteLine($"This calculator was used {calculator.Counter} times.");
                Console.WriteLine(@"
Choose an operator from the following list:

Standard Operations:
    A - Add
    S - Subtract
    M - Multiply
    D - Divide
    V - Average
Advanced Operations:
    W - Power
    R - Square Root
    N - Sine
    C - Cosine
    T - Tangent
    L - Logarithm
Other Operations:
    H - History
    Q - Quit the program
-----------------------------------------
");

                Console.Write("Your option? ");
                string? opInput = Console.ReadLine()?.Trim().ToLower();

                if (opInput == "q")
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0); // Terminates the program immediately

                }

                if (opInput == "h")
                {
                    ShowHistory();
                    continue;
                }

                // check whether the input does not match a specific pattern using regular expressions (Regex)
                if (string.IsNullOrEmpty(opInput) || !Regex.IsMatch(opInput, "^[asmvdwrnctlh]$")) 
                {
                    Console.WriteLine("Error: Unrecognized input. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }

                double cleanNum1 = 0;
                double cleanNum2 = 0;

                // Only need one number for these operations
                bool singleInputOp = new[] { "r", "n", "c", "t", "l" }.Contains(opInput);

                var firstPrompt = calculator.Results.Count() == 0
                    ? "Write a number and press Enter: "
                    : "Write a number or 'p' to use a previous result(can use on second number as well), then press Enter: ";

                var validationError = calculator.Results.Count() == 0
                    ? "This is not valid input. Please write a number: "
                    : "Invalid input. Please write a number or 'p': ";

                Console.Write(firstPrompt);
                string? numInput1 = Console.ReadLine();

                while (numInput1?.ToLower() != "p" && !double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write(validationError);
                    numInput1 = Console.ReadLine();
                }

                if (numInput1?.ToLower() == "p")
                {
                    cleanNum1 = GetPreviousResult(calculator.Results);
                }

                if (!singleInputOp)
                {
                    Console.Write("Write a second number and press Enter: ");
                    string? numInput2 = Console.ReadLine();

                    while (numInput2?.ToLower() != "p" && !double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write(validationError);
                        numInput2 = Console.ReadLine();
                    }

                    if (numInput2?.ToLower() == "p")
                    {
                        cleanNum2 = GetPreviousResult(calculator.Results);
                    }
                }

                try
                {
                    double result = calculator.DoOperation(cleanNum1, cleanNum2, opInput);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation resulted in a mathematical error.");
                    }
                    else
                    {
                        Console.WriteLine($"Your result: {result:0.##}\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception occurred: " + e.Message);
                }

                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("(Press Enter to continue.)");
                if (Console.ReadLine()?.Trim().ToLower() == "q") endApp = true;
            }
        }


        private static double GetPreviousResult(List<double> previousResults)
        {
            if (previousResults == null || !previousResults.Any())
            {
                Console.WriteLine("No previous results available.");
                return 0; // Default value
            }

            Console.WriteLine("Type the index of the previous result:");
            for (int index = 1; index <= previousResults.Count; index++)
            {
                Console.WriteLine($"{index}: {previousResults[index - 1]}");
            }

            while (true)
            {
                string? userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int index) && index > 0 && index <= previousResults.Count)
                {
                    return previousResults[index - 1];
                }
                Console.WriteLine("Invalid index. Please try again.");
            }
        }


        private static void ShowHistory()
        {
            try
            {
                string json = File.ReadAllText("calculatorlog.json");
                var data = JsonConvert.DeserializeObject<CalculatorLog>(json);

                if (data?.Operations != null && data.Operations.Any())
                {
                    Console.WriteLine("\n--- Operation History ---");
                    foreach (var op in data.Operations)
                    {
                        Console.WriteLine($"{op.Operation} | {op.Operand1} & {op.Operand2} => {op.Result}");
                    }
                    Console.WriteLine("-------------------------\n");
                }
                else
                {
                    Console.WriteLine("No operations found in history.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading history: " + ex.Message);
            }
        }
    }
}
using CalculatorLibrary;

namespace CalculatorProgram
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // set up the initial variables
            bool endApp = false;
            bool usePrevious = false;
            double cleanNum1 = 0;

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            Calculator calculator = new();

            //Run the calculator until the user chooses to exit.
            while (!endApp)
            {
                // Prompt the user for the action they'd like to perform
                //Repeat the loop until user chooses to exit or perform a new calculation
                char menuChoice;
                do
                {
                    menuChoice = Menu();

                    switch (menuChoice)
                    {
                        case '1':
                            break;
                        case '2':
                            if (calculator.calculations.Count == 0)
                            {
                                Console.WriteLine("No previous calculations to use.");
                                break;
                            }
                            else
                            {
                                usePrevious = true;
                                cleanNum1 = UsePreviousCalculations(calculator);
                            }
                            menuChoice = '1';
                            break;
                        case '3':
                            if (calculator.calculations.Count == 0)
                            {
                                Console.WriteLine("No previous calculations to use.");
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                calculator.ShowCount();
                                Console.WriteLine("\nCalculations:");
                                calculator.ShowCalculations();
                            }
                            break;
                        case '4':
                            calculator.ClearCalculations();
                            break;
                        case '5':
                            endApp = true;
                            return;

                    }
                }
                while (menuChoice != '1' && menuChoice != '5');
                Console.WriteLine("\n"); // Friendly linespacing.
                Console.Clear();


                // Ask the user to choose an operation to perform
                string opChoice = OperatorMenu();
                double result = 0;

                // Ask the user to type the first number if not using a previous result
                if (!usePrevious)
                {
                    // Ask the user to type the first number.
                    string? numInput1 = "";
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    cleanNum1 = 0;
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine($"The first number will be {cleanNum1}.");
                    usePrevious = false;
                }

                //Some operations only require one number, so we can skip the second number input
                if (int.Parse(opChoice) > 5)
                {
                    result = calculator.DoOperation(cleanNum1, 0, opChoice);
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    continue;
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                string? numInput2 = "";
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, opChoice);
                    if (double.IsNaN(result))
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    else
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

            }
            // Add call to close the JSON writer before return
            calculator.Finish();
            return;
        }

        public static char Menu()
        // Add a method to display the menu options
        // verify that the user's input is valid
        {
            char selection = '0';
            char[] validSelections = { '1', '2', '3', '4', '5' };

            while (Array.IndexOf(validSelections, selection) == -1)
            {
                Console.WriteLine("\nWhat would you like to do next?");
                Console.WriteLine("\t1. Perform New Calculation");
                Console.WriteLine("\t2. Continue from a Previous Calculation");
                Console.WriteLine("\t3. Show Previous Calculations");
                Console.WriteLine("\t4. Clear Calculations");
                Console.WriteLine("\t5. Exit");
                Console.WriteLine("");
                Console.Write("Your option? ");
                selection = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
            return selection;
        }

        public static string OperatorMenu()
        // Generate the menu to display all of the possible operations
        // Verify that the user's input is valid
        {
            string? selection = "0";
            string[] validSelections = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };

            while (Array.IndexOf(validSelections, selection) == -1)
            {
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\t1. Add");
                Console.WriteLine("\t2. Subtract");
                Console.WriteLine("\t3. Multiply");
                Console.WriteLine("\t4. Divide");
                Console.WriteLine("\t5. To the Power Of");
                Console.WriteLine("\t6. Square Root");
                Console.WriteLine("\t7. 10x");
                Console.WriteLine("\t8. Sin");
                Console.WriteLine("\t9. Cos");
                Console.WriteLine("\t10. Tan");
                Console.WriteLine("\t11. ArcSin");
                Console.WriteLine("\t12. ArcCos");
                Console.WriteLine("\t13. ArcTan");
                Console.WriteLine("");
                Console.Write("Your option? ");
                selection = Console.ReadLine();
                Console.WriteLine();
            }
            return selection;
        }

        public static double UsePreviousCalculations(Calculator calculator)
        // If the user chooses to use a previous result, display the list of previous calculations
        //Prompt the user to select a result to use
        {
            Console.WriteLine("Which Result do you want to use?");
            calculator.ShowCalculations();
            Console.WriteLine("");
            Console.Write("Your option? ");

            char selection = 'a';
            bool validSelection = false;
            double num = 0;

            while (!validSelection)
            {
                selection = Console.ReadKey().KeyChar;
                try
                {
                    num = calculator.calculations[int.Parse(selection.ToString()) - 1].Result;
                    validSelection = true;
                }
                catch
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
                Console.WriteLine();
            }
            return num;
        }
    }
}

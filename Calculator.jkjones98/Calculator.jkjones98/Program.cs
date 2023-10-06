using CalculatorLibrary;
namespace CalculatorProgram.jkjones98;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int count = 0;
        List<double> calcList = new List<double>();


        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        // Create new calculator variable for member invocation rather than a call to a static method
        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tt - Powers of 10");
            Console.WriteLine("\ti - Sin");
            Console.WriteLine("\tc - Cos");
            Console.WriteLine("\tn - Tan");

            Console.Write("Your option? ");

            string op = Console.ReadLine();

            Console.Write("Do you wish to use previous result in this operation? If so, press Y and enter: \n");

            // Ask the user to type the first number.

            Console.Write("If not, type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;

            // Check if numInput1 is y
            if (numInput1 == "y")
            {
                // cleanNum1 = first element in calcList
                cleanNum1 = calcList.First();
            }
            else
            {
                // verify numInput1 is a valid number
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
            }

            // check to see which operation was selected
            // if any of the trig/operations which require only 1 number - pass to doOperation method
            // which only requires 1 double and a string
            if (op == "r" || op == "t" || op == "i" || op == "c" || op == "n")
            {
                try
                {
                    // Pass parsed numbers and selected operation into calculator do operation method/function
                    result = calculator.DoOperation(cleanNum1, op);

                    // If the result is not a number, print the following message
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    // Else print the result
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        // Add result to a list - to be displayed at a later time to ask if we would like to use a previous calculation
                        calcList.Add(result);
                        count++;
                        Console.WriteLine("Calculator used {0} times", count);
                    }
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
            else
            {
                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }



                try
                {
                    // Pass parsed numbers and selected operation into calculator do operation method/function
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);

                    // If the result is not a number, print the following message
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    // Else print the result
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        // Add result to a list - to be displayed at a later time to ask if we would like to use a previous calculation
                        calcList.Add(result);
                        count++;
                        Console.WriteLine("Calculator used {0} times", count);
                    }
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

           
        }

        // Add call to close the JSON Writer before return
        calculator.Finish();
        return;
    }
}




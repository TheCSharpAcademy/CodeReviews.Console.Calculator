
using CalculatorAppMenu;
using CalculatorAppValidator;

namespace Calculator
    {
    public class Program
        {
        private static void Main(string [ ] args)
            {
            string answer, firstNumber, secondNumber;
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            CalculatorLibrary.Calculator calculator = new CalculatorLibrary.Calculator();

            Validator validate = new Validator();

            var store = new List<string>();
            List<double> numList1 = new List<double>();
            List<double> numList2 = new List<double>();

            int number = -1;
            string operationSign;
            string operand = " ";
            string record;
            while ( !endApp )
                {

                Menu menu = new Menu();
                // Ask the user to choose an operator.
                menu.OperandMenu();
                string op = validate.ValidateString(Console.ReadLine());

                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                if ( op == "r" || op =="t" )
                    {
                    numInput2 = numInput1;
                    }
                else
                    {
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();
                    }

                double cleanNum1 = 0;

                while ( !double.TryParse(numInput1, out cleanNum1) )
                    {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                    }

                double cleanNum2 = 0;
                while ( !double.TryParse(numInput2, out cleanNum2) )
                    {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                    }

                try
                    {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if ( double.IsNaN(result) )
                        {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                catch ( Exception e )
                    {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }

                Console.WriteLine("------------------------------------------------------------------------------\n");
                calculator.Counter();
                Console.WriteLine("------------------------------------------------------------------------------\n");
                answer = result.ToString();
                firstNumber = numInput1;
                secondNumber = numInput2;
                number++;
                operationSign = calculator.Sign(op);
                operand = calculator.Operation(op);

                switch ( op )
                    {

                case "r":
                    record = "Index " + number + "." + operand + " ==>" + "SquareRoot"+ "(" + firstNumber + ")" + " = " + answer;
                    store.Add(record);
                    break;
                case "t":
                    record = "Index " + number + "." + operand + " ==>" + "Sin"+ "(" + firstNumber + ")"+ " = " + answer;
                    store.Add(record);
                    break;

                default:
                    record = "Index " + number + "." + operand + " ==>" + firstNumber + " " + operationSign + " " + secondNumber + " = " + answer;
                    //Store View
                    store.Add(record);
                    break;
                    }
                //RETRIEVE AND REBUILT MATH
                numList1.Add(cleanNum1);
                numList2.Add(cleanNum2);
                string userResponse;
                int selection;
                for ( int i = 0 ; i < store.Count ; i++ )
                    {
                    Console.WriteLine(store [ i ]);
                    }
                do
                    {
                    menu.DeleteMenu();


                    selection = validate.ValidateNumbers(Console.ReadLine());
                    Console.WriteLine("------------------------------------------------------------------------------\n");
                    switch ( selection )
                        {
                    case 1:
                        int userIndex = 0;
                        do
                            {
                            Console.Write("Enter correct Index of record to delete : ");
                            userIndex = validate.ValidateNumbers(Console.ReadLine());
                            if ( userIndex <= store.Count ) { store.RemoveAt(userIndex); }
                            if ( userIndex <= numList1.Count ) { numList1.RemoveAt(userIndex); }
                            if ( userIndex <= numList2.Count ) { numList2.RemoveAt(userIndex); }
                            } while ( userIndex > store.Count || userIndex > numList1.Count || userIndex > numList2.Count );
                        break;
                    case 2:
                        store.Clear();
                        numList1.Clear();
                        numList2.Clear();
                        Console.WriteLine("List has been successfully removed");
                        break;
                    case 3:
                        Console.Write("Index to use for new Operation : ");
                        int operationIndex = validate.ValidateNumbers(Console.ReadLine());

                        cleanNum1 = numList1 [ operationIndex ];
                        cleanNum2 = numList2 [ operationIndex ];

                        menu.OperandMenu();
                        op = validate.ValidateString(Console.ReadLine());
                        try
                            {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                            if ( double.IsNaN(result) )
                                {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                                }
                            else Console.WriteLine("Your result: {0:0.##}\n", result);
                            }
                        catch ( Exception e )
                            {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                            }
                        operationSign = calculator.Sign(op);
                        operand = calculator.Operation(op);
                        number++;
                        record = "Index " + number + "." + operand + " ==>" + cleanNum1 + " " + operationSign + " " + cleanNum2  + " = " + result;
                        store.Add(record);

                        break;

                    case 4:
                        for ( int i = 0 ; i < store.Count ; i++ )
                            {
                            Console.WriteLine(store [ i ]);
                            }
                        break;
                    default:
                        Console.WriteLine("End of Delete Operations");
                        break;
                        }
                    Console.Write(" Perform More Delete Operations (Y/N): ");
                    userResponse = validate.ValidateString(Console.ReadLine());

                    } while ( userResponse == "Y" || userResponse == "y" );


                Console.WriteLine("------------------------------------------------------------------------------\n");
                // Wait for the user to respond before closing.
                Console.WriteLine("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                Console.WriteLine("------------------------------------------------------------------------------\n");
                //Print answer print.
                Console.WriteLine("------------------------------------------------------------------------------\n");
                if ( Console.ReadLine() == "n" ) endApp = true;
                Console.WriteLine("\n"); // Friendly linespacing.
                }
            //Add call to close the JSON writer before return
            calculator.Finish();
            return;
            }
        }
    }

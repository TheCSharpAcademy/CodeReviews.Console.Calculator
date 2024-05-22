using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CalculatorProgram
{
    class Program
    {

        static void Main(string[] args)
        {
           Menu(); 
        }

        internal static void Menu(){
            Console.Clear(); 
            DateTime date = DateTime.UtcNow;
            Console.WriteLine(date); 
            Console.WriteLine("Welcome to the Console Calculator"); 
            Console.WriteLine(@"Select the following options:

0 - Close the Application
1 - Calculate 
2 - Historic Records 
            ");

            
            var readResult = Console.ReadLine(); 
            switch(readResult){
                case "0":
                    Environment.Exit(0);
                    break; 
                
                case "1":
                    Calculate(); 
                    break; 

                case "2":
                    Helpers.PrintCalculations(); 
                    Calculate(); 
                    break; 
            }
        }
        internal static void Calculate(){

            bool isAppOn = true;

            Console.Clear(); 

            while (isAppOn)
            {
                var result = 0; 
                var cleanNum1 = Logic.SetFirstNum();
                var (operation, count) = Logic.SetOp(); 

                double cleanNum2 = 0; 
                if(!(operation.Equals("r") || operation.Equals("sin") || operation.Equals("cos") || operation.Equals("tan"))){
                    cleanNum2 = Logic.SetSecondNum(); 
                }

                Logic.TotalOperation(cleanNum1, cleanNum2, operation); 

                    // Wait for the user to respond before closing.
                Console.Write(@$"Press the following keys:

    press 'q' and Enter to quit the application.
    press 't' and Enter to see the times you used the calculator.  
    press 'r' and Enter to calculate with the result of the last operation.
    or press any other key and Enter to continue: ");

                Console.WriteLine("\n\n------------------------\n");

                string? readResult = Console.ReadLine();

                switch (readResult)
                {
                    case "q":
                        isAppOn = false;
                        break;
        
                    case "t":
                        Helpers.CountCalculator(count);
                        break;

                    case "r":
                        Helpers.Recalculate(result); 
                        break; 
                    
                }

                Console.WriteLine("\n"); // Friendly linespacing.
                Console.Clear();

            }
            return;
        }
    }
}

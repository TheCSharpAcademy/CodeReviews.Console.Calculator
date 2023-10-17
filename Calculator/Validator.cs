using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace CalculatorAppValidator
    {
    public class Validator
        {
        public int ValidateNumbers(string userInput)
            {
            int convertedResult;

            while ( !Int32.TryParse(userInput, out convertedResult) )
                {
                Console.Write("Not a valid number!! Try Again. Pleases provide correct Input! : ");
                userInput = Console.ReadLine();
                }

            return convertedResult;
            }

        public string ValidateString(string userInput)
            {


            string pattern = "^[a-zA-Z]*$"; // Adjust the regex pattern as needed
                                            // Replace this with how you get user input

            while ( !Regex.IsMatch(userInput, pattern) )
                {
                Console.Write("Not a valid input!! Try Again. Pleases provide correct Input! : ");
                userInput = Console.ReadLine();
                }
            return userInput;
            }

        }
    }

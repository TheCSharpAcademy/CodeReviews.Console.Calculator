

using System.Linq;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorAppMenu
    {
    public class Menu
        {
        public void OperandMenu()

            {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - SquareRoot");
            Console.WriteLine("\tp - Power ( a ^ b)");
            Console.WriteLine("\tt - Trigonometry (Sine(Angle))"); //Square Root, Taking the Power, 10x, Trigonometry functions.
            Console.Write("Your option? ");

            }

        public void DeleteMenu()
            {
            Console.WriteLine("------------------------------------------------------------------------------\n");
            Console.WriteLine(" Delete Operations Menu ");
            Console.Write(" 1. Index of record to Delete :  \n");
            Console.Write(" 2. Delete All records   \n");
            Console.Write(" 3. Index of record to use to perform a new operation :  \n");
            Console.Write(" 4. View History   \n");
            Console.Write(" 5. Exit Menu   \n");
            Console.Write(" Select a number to perform a Delete Operation : ");
            }


        }


    }
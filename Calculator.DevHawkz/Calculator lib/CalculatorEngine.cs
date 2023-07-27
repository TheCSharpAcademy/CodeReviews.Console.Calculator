using Calculator_lib.Model;

namespace Calculator_lib
{
    internal static class CalculatorEngine
    { 
        internal static void Addition()
        {
            double firstNumber = Helpers.GetNumber();
            double secondNumber = Helpers.InputNumber();

            double result = firstNumber + secondNumber;
            Console.WriteLine($"Result of {firstNumber} + {secondNumber} is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.Addition, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        internal static void Subtraction() 
        {
            double firstNumber = Helpers.GetNumber();
            double secondNumber = Helpers.InputNumber();

            double result = firstNumber - secondNumber;
            Console.WriteLine($"Result of {firstNumber} - {secondNumber} is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.Subraction, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        internal static void Multiplication()
        {
            double firstNumber = Helpers.GetNumber();
            double secondNumber = Helpers.InputNumber();

            double result = firstNumber * secondNumber;
            Console.WriteLine($"Result of {firstNumber} * {secondNumber} is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.Addition, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        internal static void Divison()
        {
            double firstNumber = Helpers.GetNumber();
            double secondNumber = Helpers.InputNumber();

            while(secondNumber == 0)
            {
                Console.WriteLine("Can't divide by zero\nPress any number to continue...");
                Console.ReadKey();
                secondNumber = Helpers.InputNumber();
            }

            double result = firstNumber / secondNumber;
            Console.WriteLine($"Result of {firstNumber} / {secondNumber} is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.Divison, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        internal static void TimesTen()
        {
            double number = Helpers.GetNumber();
            double result = number * 10;
            
            Console.WriteLine($"{number} * 10 is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.TimesTen, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

        internal static void TakeToPower()
        {
            double number = Helpers.GetNumber();
            double exponent = Helpers.InputNumber('p');

            double result = Math.Pow(number, exponent);

            Console.WriteLine($"{number} taken to power of {exponent} is {result}");

            Helpers.AddToHistory(TypeOfCalculation.TakingToPower, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

        internal static void SquareRoot()
        {
            Console.WriteLine("For all negative values the calculation will be based on their absolute values!\nPress any key to continue...");
            Console.ReadKey();
            double number = Math.Abs(Helpers.GetNumber());

            double result = Math.Sqrt(number);

            Console.WriteLine($"Square root of a {number} is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.SquareRoot, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        internal static void Sinus()
        {
            double number = Helpers.GetNumber();
            double angleInRadian = Helpers.AngleInRadian(number);

            double result = Math.Sin(angleInRadian);

            Console.WriteLine($"Sinus of an {number} angle is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.Sinus, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        internal static void Cosinus()
        {
            double number = Helpers.GetNumber();
            double angleInRadian = Helpers.AngleInRadian(number);

            double result = Math.Cos(angleInRadian);

            Console.WriteLine($"Cosinus of an {number} angle is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.Cosinus, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        internal static void Tangens()
        {
            double number = Helpers.GetNumber();
            double angleInRadian = Helpers.AngleInRadian(number);

            double result = Math.Sin(angleInRadian) / Math.Cos(angleInRadian);

            Console.WriteLine($"Tangens of an {number} angle is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.Tangens, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        internal static void Cotangens()
        {
            double number = Helpers.GetNumber();
            double angleInRadian = Helpers.AngleInRadian(number);

            double result = Math.Cos(angleInRadian) / Math.Sin(angleInRadian);

            Console.WriteLine($"Cotangens of an {number} angle is: {result}");

            Helpers.AddToHistory(TypeOfCalculation.Cotangens, result);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

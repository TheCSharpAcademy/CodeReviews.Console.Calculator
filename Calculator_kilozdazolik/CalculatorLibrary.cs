namespace Calculator_kilozdazolik
{
    public class Calculator
    {
        private int _calculatorUsed;
        public int CalculatorUsed => _calculatorUsed;
        private void IncrementUsage() => _calculatorUsed++;
        
        private static readonly CalculatorEngine Engine = new CalculatorEngine();
        private readonly Helpers _helper = new();

        public static double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

            switch (op)
            {
                case "a":
                    result = Engine.AddOperand(num1, num2);
                    break;
                case "s":
                    result = Engine.SubtractOperand(num1, num2);
                    break;
                case "m":
                    result = Engine.MultiplyOperand(num1, num2);
                    break;
                case "d":
                    result = Engine.DivideOperand(num1, num2);
                    break;
                case "sq":
                    result = Engine.SquareRootOperand(num1);
                    break;
                case "si":
                    result = Engine.SinOperand(num1);
                    break;
                case "c":
                    result = Engine.CosOperand(num1);
                    break;
                case "t":
                    result = Engine.TanOperand(num1);
                    break;
                case "p":
                    result =  Engine.PowerOfTen(num1);
                    break;
            }
            
            return result;
        }
        
        public void HandleUnaryOperation(string op)
        {
            Console.Write("Type a number, and then press Enter: ");
            string? input = Console.ReadLine();
    
            try
            {
                double operand = _helper.ValidateNumInput(input);
                double result = DoOperation(operand, 0, op);

                if (!double.IsNaN(result))
                {
                    IncrementUsage(); 
                    _helper.AddToHistory(CalculatorUsed, operand, 0, op, result);
                    Console.WriteLine($"Your result: {result:0.##}");
                }
                else
                {
                    Console.WriteLine("Invalid operation.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        
        public void HandleBinaryOperation(string op)
        {
            Console.Write("Type a number, and then press Enter: ");
            string? input1 = Console.ReadLine();
            Console.Write("Type another number, and then press Enter: ");
            string? input2 = Console.ReadLine();

            try
            {
                double operand1 = _helper.ValidateNumInput(input1);
                double operand2 = _helper.ValidateNumInput(input2);
                double result = DoOperation(operand1, operand2, op);

                if (!double.IsNaN(result))
                {
                    IncrementUsage();
                    _helper.AddToHistory(CalculatorUsed, operand1, operand2, op, result);
                    Console.WriteLine($"Your result: {result:0.##}");
                }
                else
                {
                    Console.WriteLine("This operation will result in a mathematical error.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
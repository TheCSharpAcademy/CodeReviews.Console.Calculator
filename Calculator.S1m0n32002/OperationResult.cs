using static Calculator.S1m0n32002.CalculatorController;

namespace Calculator.S1m0n32002
{
    internal class OperationResult()
    {
        public double Value { get; set; }
        public double[] Numbers { get; set; } = [];
        public CalculatorController.Operations Operation { get; set; } 

        public override string ToString()
        {
            string returnString = "";

            switch (Operation)
            {
                case Operations.Sum:
                    returnString = $"{Numbers[0]} + {Numbers[1]} = {Value}";
                    break;

                case Operations.Subtract:
                    returnString = $"{Numbers[0]} - {Numbers[1]} = {Value}";
                    break;

                case Operations.Multiply:
                    returnString = $"{Numbers[0]} * {Numbers[1]} = {Value}";
                    break;

                case Operations.Divide:
                    returnString = $"{Numbers[0]} / {Numbers[1]} = {Value}";
                    break;

                case Operations.SquareRoot:
                    returnString = $"√{Numbers[0]} = {Value}"; // Square root symbol might not be displayed correctly
                    break;

                case Operations.TenPower:
                case Operations.Power:
                    returnString = $"{Numbers[0]} ^ {Numbers[1]} = {Value}";
                    break;

                case Operations.Sin:
                    returnString = $"Sin({Numbers[0]}) = {Value}";
                    break;

                case Operations.Cos:
                    returnString = $"Cos({Numbers[0]}) = {Value}";
                    break;

                case Operations.Tan:
                    returnString = $"Tan({Numbers[0]}) = {Value}";
                    break;

                case Operations.Log:
                    returnString = $"Log({Numbers[0]}) = {Value}";
                    break;

                case Operations.Exp:
                    returnString = $"Exp({Numbers[0]}) = {Value}";
                    break;
            }

            return returnString;
        }
    }
}

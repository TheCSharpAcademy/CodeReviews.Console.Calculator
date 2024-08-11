namespace MansoorAZafar.CalculatorLibrary
{
    internal class Calculation
    {
        public int calculationNumber;
        public double firstOperand;
        public double secondOperand;
        public string operation;
        public double result;
        public Calculation(double firstOperand, double secondOperand, string operation, double result, int calculationNumber)
        {
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
            this.operation = operation;
            this.result = result;
            this.calculationNumber = calculationNumber;
        }

        public override string? ToString()
        {
            return $"Calculation #{this.calculationNumber}\n"
                + $"{"First Operand".PadRight(20)}: {this.firstOperand}\n"
                 + $"{"Second Operand".PadRight(20)}: {this.secondOperand}\n"
                 + $"{"Operation".PadRight(20)}: {this.operation}\n"
                 + $"{"Result".PadRight(20)}: {this.result:N2}\n";
        }
    }
}

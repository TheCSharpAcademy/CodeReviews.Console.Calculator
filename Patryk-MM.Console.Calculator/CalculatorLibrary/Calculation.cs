namespace CalculatorLibrary {
    public class Calculation {

        public double Num1 { get; set; } = double.NaN;
        public double Num2 { get; set; } = double.NaN;
        public char Operation { get; set; }
        public double Result { get; set; } = double.NaN;

        public Calculation(double num1, double num2, char op)
        {
            Num1 = num1;
            Num2 = num2;
            Operation = op;
        }

        public Calculation(double num1, char op) {
            Num1 = num1;
            Operation = op;
        }

        public Calculation()
        {
            
        }
    }
}

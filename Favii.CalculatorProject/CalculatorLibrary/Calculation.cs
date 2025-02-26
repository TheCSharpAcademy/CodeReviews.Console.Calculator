namespace CalculatorLibrary
{
    public class Calculation
    {
        public double Num1 { get; set; }
        public double Num2 { get; set; }
        public double Result { get; set; }
        public string Operation { get; set; }

        public string calculation = "";

        public Calculation(double num1, double num2, double result, string operation)
        {
            Num1 = num1;
            Num2 = num2;
            Result = result;
            Operation = operation;

            switch (Operation)
            {
                case "a":
                    calculation = $"{num1} + {num2} = {result}";
                    break;
                case "s":
                    calculation = $"{num1} - {num2} = {result}";
                    break;
                case "m":
                    calculation = $"{num1} * {num2} = {result}";
                    break;
                case "d":
                    calculation = $"{num1} / {num2} = {result}";
                    break;
                case "r":
                    calculation = $"{num1} * {num1} = {result}";
                    break;
                case "p":
                    calculation = $"{num1} ^ {num2} = {result}";
                    break;
            } 
        }

        public void DisplayCalculation()
        {
           Console.WriteLine(calculation);

        }
    }
}

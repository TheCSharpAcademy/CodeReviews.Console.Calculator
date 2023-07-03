using System.Collections.Generic;
using System.Linq;



namespace Calculator.Mo3ses
{
    public  class MathOperations
    {
        public string Operation { get; set; }
        public double ValueOfOperation { get; set; }

        public MathOperations()
        {
            
        }
        public MathOperations(string operation, double valueOfOperation)
        {
            Operation = operation;
            ValueOfOperation = valueOfOperation;
        }


        public  List<MathOperations> calculations = new List<MathOperations>();

        public  double Sum(double value1, double value2)
        {
            double answer = value1 + value2;
            calculations.Add(new MathOperations($"({value1} + {value2})", answer));
            return answer;
        }
        public  double Subtract(double value1, double value2)
        {
            double answer = value1 - value2;
            calculations.Add(new MathOperations($"({value1} - {value2})", answer));
            return answer;
        }
        public  double Multiply(double value1, double value2)
        {
            double answer = value1 * value2;
            calculations.Add(new MathOperations($"({value1} * {value2})", answer));
            return answer;
        }
        public  double Divide(double value1, double value2)
        {
            double answer = value1 / value2;
            calculations.Add(new MathOperations($"({value1} / {value2})", answer));
            return answer;
        }
        public double SquareRoot(double value1){
            double answer = Math.Sqrt(value1);
            calculations.Add(new MathOperations($"√{value1}", answer));
            return answer;
        }
        public double TakingPower(double value1, double value2){
            double answer = Math.Pow(value1, value2);
            calculations.Add(new MathOperations($"({value1} ^ {value2})", answer));
            return answer;
        }
        public double TenTimes(double value1){
            double answer = value1 * 10;
            calculations.Add(new MathOperations($"({value1} * 10)", answer));
            return answer;
        }
        public double Sine(double value1){
            double answer = Math.Sin(value1);
            calculations.Add(new MathOperations($"sin({value1})", answer));
            return answer;
        }
        public double Cosine(double value1){
            double answer = Math.Cos(value1);
            calculations.Add(new MathOperations($"cos({value1})", answer));
            return answer;
        }
        public double Tangent(double value1){
            double answer = Math.Tan(value1);
            calculations.Add(new MathOperations($"tan({value1})", answer));
            return answer;
        }
        public double OperationGetById(int id)
        {
            return calculations[id].ValueOfOperation;
        }
        public  void ListOperations()
        {
            int i = 0;
            if (calculations.Count > 0)
            {
                Console.WriteLine("-------------List of Calculations----------");
                foreach (var item in calculations)
                {
                    Console.WriteLine($"{i} - {item.Operation} - Answer: {item.ValueOfOperation}");
                    i++;
                }
                Console.WriteLine("-------------------------------------------");
            }
            else
            {
                Console.WriteLine("LIST OF CALCULATIONS IS EMPTY");
            }
        }
        public void DeleteOperationList()
        {
            
            Console.WriteLine($"CALCULATION LIST DELETED, REMOVED {calculations.Count()} ITEMS");
            calculations.Clear();
        }

    }
}

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

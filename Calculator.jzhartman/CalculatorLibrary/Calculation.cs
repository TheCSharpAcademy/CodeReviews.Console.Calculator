using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public class Calculation
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }
        public bool HasError { get; set; }

        public Calculation(double operand1, double operand2, string operation, double result, bool hasError)
        {
            this.Operand1 = operand1;
            this.Operand2 = operand2;
            this.Operation = operation;
            this.Result = result;
            this.HasError = hasError;
        }
    }
}

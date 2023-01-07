using System.Collections.Generic;

namespace Calculator.Lonchanick
{
    public class Operations
    {
        public double Operando1 { get; set; }
        public double Operando2 { get; set; }
        public double Result{ get; set;}
        public string Operation { get; set; }
        public Operations(double op1, double op2, double result, string operation)
        {
            this.Operando1 = op1;
            this.Operando2 = op2;
            this.Result = result;
            this.Operation = operation;
        }
        public Operations() { }
    }
}

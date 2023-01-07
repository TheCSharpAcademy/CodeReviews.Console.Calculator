using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Lonchanick
{
    public class Operations
    {
        public Operations(double op1, double op2, double result, string operation) 
        {
            this.Operando1=op1; this.Operando2=op2; this.Result=result; this.Operation=operation;
        }
        public double Operando1 { get; set; }
        public double Operando2 { get; set; }
        public double Result{ get; set;}
        public string Operation { get; set; }
    }
}

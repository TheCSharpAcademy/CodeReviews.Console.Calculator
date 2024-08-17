using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{

    // Template to store previous calculations
    public class PreviousCalculations
    {
        private double _num1 {  get; set; }
        private double _num2 { get; set; }
        private double _result { get; set; }
        private string _operand { get; set; }

       public PreviousCalculations(double num1, string operand ,double num2, double result)
        {
            _num1 = num1;
            _operand = operand;
            _num2 = num2;
            _result = result;
        }

       public double Result { get { return _result; } } 
       public string Operand { get { return _operand; } }
       public double Num1 { get { return _num1; } }
       public double num2 { get { return _num2; } }



    }
}

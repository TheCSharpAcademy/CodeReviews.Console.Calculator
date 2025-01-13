using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorLibrary.Enums;

namespace CalculatorLibrary.Models
{
    public class CalculationModel
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public Operation Operation { get; set; }
        public string OperationSymbol { get; set; }
        public double Result { get; set; }
        public bool HasError { get; set; }

        public string CalculationDisplayText { get; set; }

        public CalculationModel(double operand1, double operand2, string operation, double result, bool hasError)
        {
            Operand1 = operand1;
            Operand2 = operand2;
            //Operation = operation;
            Result = result;
            HasError = hasError;
        }

        public string SetCalculationDisplayText(double operand1, double operand2, string operation, double result, bool hasError)
        {
            switch (operation)
            {
                //case
            }

            return "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary;

public class CalculationsRecord
{
    public class OperationRecord //class that holds the list structure
    {
        public int Id { get; set; }
        public double numA1 { get; set; }
        public string operation { get; set; }
        public double NumA2 { get; set; }
        public double resultA { get; set; }
    }
}

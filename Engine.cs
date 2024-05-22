using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    internal class Engine
    {
        public DateTime Date { get; set; }
        public double Num1 { get; set; }
        public double Num2 { get; set; }
        public double TotalResult { get; set; }
        public CalculationType Type { get; set; }

    }

    internal enum CalculationType
    {
        Add,
        Substact, 
        Multiply, 
        Division, 
        SquareRoot, 
        Power, 
        Sine, 
        Cosine,
        Tangent
    }

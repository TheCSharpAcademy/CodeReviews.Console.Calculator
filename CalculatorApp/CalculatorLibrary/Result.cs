﻿
namespace CalculatorLibrary
{
    public class Result
    {
        public double Operand1 { get; set; }
        public double? Operand2 { get; set; }
        public string Operation { get; set; }
        public double Answer { get; set; }

        public void Display()
        {
            Console.WriteLine($"Operand1: {Operand1}\nOperand2: {Operand2}\nOperation: {Operation}\nResult: {Answer}");
        }
    }
}
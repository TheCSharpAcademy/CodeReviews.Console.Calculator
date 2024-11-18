namespace Calculator.BNAndras.CalculatorLibrary.Models;

public record CalculationResult(MathOperation Operation, List<double> Operands, double Result);

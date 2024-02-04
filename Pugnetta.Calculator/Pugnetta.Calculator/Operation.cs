namespace Pugnetta.Calculator;

public record Operation
{
    public Guid Id { get; }
    public decimal N1 { get; }
    public decimal N2 { get; }
    public Operator Operator { get; }
    public string? Result { get; init; } 
    private Operation(Guid id, decimal n1, decimal n2, Operator o) =>
        (Id, N1, N2, Operator) = (id, n1, n2, o);    
    public static Operation Create(decimal n1, decimal n2, Operator o) =>
        new(Guid.NewGuid(), n1, n2, o);
    public static Operation Create(Operation operation, decimal result) =>
        operation with { Result = result.ToString() };
}
public enum Operator
{
    Sum,
    Sub,
    Mol,
    Div
}
public static class OperationExtentions
{
    public static decimal Execute(this Operation operation, Operator o) => o switch
    {
        Operator.Sum => operation.N1 + operation.N2,
        Operator.Sub => operation.N1 - operation.N2,
        Operator.Mol => operation.N1 * operation.N2,
        Operator.Div when operation.N2 != 0 => operation.N1 / operation.N2,
        Operator.Div when operation.N2 == 0 =>
        throw new ArgumentException("Cant divide by 0"),
        _ => throw new InvalidOperationException(nameof(o))
    };
}
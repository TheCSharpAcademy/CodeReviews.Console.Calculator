namespace CalculatorLibrary;

public static class TextFormat {
    public static void WriteLine(string text, ConsoleColor color) {
        var temp = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = temp;
    }

    public static void Write(string text, ConsoleColor color) {
        var temp = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = temp;
    }
}

using CalculatorApp.BBualdo;

Console.WriteLine("C# Calculator App");
Console.WriteLine("-----------------");

bool isOn = true;

ProgramEngine engine = new ProgramEngine();

while (isOn)
{
  engine.MainMenu();
  double result = engine.DoCalculation();
  Console.WriteLine($"\n\tResult is: {result}");
  Console.WriteLine("Press 'q' and then Enter to exit, or press any other key to continue.");
  if (Console.ReadLine() == "q")
  {
    Console.WriteLine("Goodbye!");
    isOn = false;
  }
}
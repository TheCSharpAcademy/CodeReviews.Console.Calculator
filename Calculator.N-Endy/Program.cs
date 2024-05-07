using Calculator.N_Endy.CalculatorEngine;
using Calculator.N_Endy.UserInteractionRepository;

IUserInteraction userInteraction = new UserInteraction();
CalculatorLibrary.Calculator calculatorLibrary = new CalculatorLibrary.Calculator();

CalculatorEngine calculatorEngine = new CalculatorEngine(userInteraction, calculatorLibrary);

calculatorEngine.Run();
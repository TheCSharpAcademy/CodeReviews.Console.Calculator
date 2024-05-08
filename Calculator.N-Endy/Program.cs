using Calculator.N_Endy.CalculatorEngine;
using Calculator.N_Endy.UserInteractionRepository;
using MyCalculator = CalculatorLibrary.Calculator;

IUserInteraction userInteraction = new UserInteraction();
MyCalculator calculatorLibrary = new();

CalculatorEngine calculatorEngine = new(userInteraction, calculatorLibrary);

calculatorEngine.Run();
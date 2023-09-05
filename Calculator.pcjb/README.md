# Calculator
A very simple Calculator console app. Exercise by [The C# Academy](https://www.thecsharpacademy.com)

## Requirements
see https://www.thecsharpacademy.com/project/11
* Complete the following tutorial (parts 1 and 2): Create a Calculator App (Microsoft Docs).

## Optional Requirements
* Count the amount of times the calculator was used
* Store a list with the latest calculations
  * Give the users the ability delete the list
  * Allow the users to use results from the list to perform new calculations
* Add extra calculations: 
  * Square Root
  * Taking the Power
  * 10x
  * Trigonometry functions

## Hints for tutorial part 2 (requires Visual Studio) using only VS Code / dotnet CLI
see https://learn.microsoft.com/en-us/dotnet/core/tutorials/library-with-visual-studio-code?pivots=dotnet-7-0

Move existing calculator project files (Calculator.csproj, Program.cs, Calculator.cs) into new subdirectory 'Calculator'. Remove bin/, obj/.

Add solution, classlib and reference
```
Calculator.pcjb$ dotnet new sln
Calculator.pcjb$ dotnet new classlib -o CalculatorLibrary
Calculator.pcjb$ dotnet sln add CalculatorLibrary/CalculatorLibrary.csproj
Calculator.pcjb$ dotnet sln add Calculator/Calculator.csproj
Calculator$ dotnet add Calculator.csproj reference ../CalculatorLibrary/CalculatorLibrary.csproj
```
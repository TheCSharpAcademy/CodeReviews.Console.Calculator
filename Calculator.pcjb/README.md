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
Move existing calculator project files (Calculator.csproj, Program.cs, Calculator.cs) into new subdirectory 'Calculator'. Remove bin/, obj/.

Add solution, classlib and reference
see https://learn.microsoft.com/en-us/dotnet/core/tutorials/library-with-visual-studio-code?pivots=dotnet-7-0
```
Calculator.pcjb$ dotnet new sln
Calculator.pcjb$ dotnet new classlib -o CalculatorLibrary
Calculator.pcjb$ dotnet sln add CalculatorLibrary/CalculatorLibrary.csproj
Calculator.pcjb$ dotnet sln add Calculator/Calculator.csproj
Calculator.pcjb$ dotnet add Calculator/Calculator.csproj reference CalculatorLibrary/CalculatorLibrary.csproj
```

Add NuGet Package 'Newtonsoft.Json' to CalculatorLibrary.csproj
see https://code.visualstudio.com/docs/csharp/package-management
```
Calculator.pcjb/CalculatorLibrary$ dotnet add package Newtonsoft.Json
```

'JsonWriter' not found:
```
CalculatorLibrary/CalculatorLibrary.cs(5,5): error CS0246: The type or namespace name 'JsonWriter' could not be found (are you missing a using directive or an assembly reference?)
```
Fix: Using directive is missing in tutorial code. Add `using Newtonsoft.Json;` to CalculatorLibrary.cs (and remove unnecessary `using System.Diagnostics;`).



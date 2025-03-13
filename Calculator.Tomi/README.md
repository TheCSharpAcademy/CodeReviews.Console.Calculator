# C# Academy Calculator

A console-based calculator written in C#.

## Features

- Supports basic arithmetic operations: Addition, Subtraction, Multiplication, Division
- Supports advanced operations: Square, Square Root, Exponentiation
- Allows reusing the previous result in subsequent calculations
- Logs operations in a JSON file (`calculator.log`)

## Project Structure

```
Calculator.Tomi/
│── CalculatorCore/             # Core calculator logic
│   ├── CalculatorCore.cs       # Main computation logic
│   ├── CalculatorCore.csproj   # Project file for CalculatorCore
│── CalculatorPrompts/          # User input handling
│   ├── CalculatorPrompts.cs    # Prompt handling logic
│   ├── CalculatorPrompts.csproj # Project file for CalculatorPrompts
│── Program.cs                  # Entry point for the application
│── Calculator.Tomi.csproj       # Main application project file
│── CodeReviews.Console.Calculator.sln # Solution file
```

## Installation

1. **Clone the repository:**
   ```sh
   git clone https://github.com/remioluwatomi/CodeReviews.Console.Calculator.git
   cd calculator
   ```
2. **Ensure you have .NET installed:**

   ```sh
   dotnet --version
   ```

   If not installed, download and install it from [dotnet.microsoft.com](https://dotnet.microsoft.com/)

3. **Restore dependencies:**
   ```sh
   dotnet restore
   ```

## Usage

To run the calculator, execute:

```sh
cd Calculator.Tomi
dotnet run
```

## How It Works

1. The program prompts the user to select an operation.
2. It asks for one or two operands based on the operation.
3. The `CalculatorEngine` processes the operation and returns the result.
4. The result is displayed and logged in `calculator.log`.
5. The user can choose to continue or exit the application.

## Dependencies

- [.NET 8.0](https://dotnet.microsoft.com/)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) (for logging operations)

## License

MIT License. Feel free to use and modify this project.

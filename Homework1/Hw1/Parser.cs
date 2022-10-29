namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args,
        out double val1,
        out CalculatorOperation operation,
        out double val2)
    {
        if (!IsArgLengthSupported(args))
            throw new ArgumentException("The number of arguments is not 3");

        if (!double.TryParse(args[0], out val1))
            throw new ArgumentException("Argument 1 is not a number");
        if (!double.TryParse(args[2], out val2))
            throw new ArgumentException("Argument 2 is not a number");
        operation = ParseOperation(args[1]);
        if (operation is CalculatorOperation.Undefined)
            throw new InvalidOperationException("Unsupported operation");
    }

    private static bool IsArgLengthSupported(IReadOnlyCollection<string> args) => args.Count == 3;

    private static CalculatorOperation ParseOperation(string arg)
    {
        return arg switch
        {
            "+" => CalculatorOperation.Plus,
            "-" => CalculatorOperation.Minus,
            "*" => CalculatorOperation.Multiply,
            "/" => CalculatorOperation.Divide,
            _ => CalculatorOperation.Undefined
        };
    }
}
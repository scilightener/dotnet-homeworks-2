namespace Hw8.Calculator;

public class Parser : IParser
{
    public ParseStatus TryParseArguments(string arg0, string arg1, string arg2, out ParseResult result)
    {
        result = new ParseResult();
        if (!double.TryParse(arg0, out var val1) || !double.TryParse(arg2, out var val2))
            return ParseStatus.ArgumentError;
        if (!Enum.TryParse<Operation>(arg1, true, out var operation) || operation is Operation.Invalid)
            return ParseStatus.OperationError;
        if (operation is Operation.Divide && val2 < double.Epsilon)
            return ParseStatus.ZeroDivision;
        result.Val1 = val1;
        result.Operation = operation;
        result.Val2 = val2;
        return ParseStatus.Ok;
    }
}
namespace Hw8.Calculator;

public interface IParser
{
    public ParseStatus TryParseArguments(string arg1, string operation, string arg2, out ParseResult result);
}
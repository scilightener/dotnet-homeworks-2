using System.Text.RegularExpressions;
using static Hw9.ErrorMessages.MathErrorMessager;

namespace Hw9.Services;

public static class ParserToPolishNotation
{
    private static readonly Regex Delimiters = new("(?<=[-+*/()])|(?=[-+*/()])");
    private static readonly Regex Numbers = new(@"^\d+");

    public static string Parse(string? input) => !string.IsNullOrEmpty(input)
        ? ToPolishNotation(input.Replace(" ", ""))
        : throw new Exception(EmptyString);
    
    private static string ToPolishNotation(string input)
    {
        // if (input.Length == 0) return "";
        var ops = new Stack<string>();
        var polish = new Stack<string>();
        var inputSplited = Delimiters.Split(input);
        var lastTokenIsOp = true;
        for (var i = 0; i < inputSplited.Length; i++)
        {
            var token = inputSplited[i];
            if (token.Length == 0) continue;
            if (Numbers.IsMatch(token))
            {
                polish.Push(token);
                lastTokenIsOp = false;
                continue;
            }
            if (token == "-" && i < inputSplited.Length - 1 && Numbers.IsMatch(inputSplited[i + 1]) && lastTokenIsOp)
            {
                polish.Push(token + inputSplited[++i]);
                lastTokenIsOp = false;
                continue;
            }
            switch (token)
            {
                case "(":
                    ops.Push(token);
                    lastTokenIsOp = true;
                    continue;
                case ")":
                {
                    while (ops.Peek() != "(")
                        PushOperation(ops, polish);
                    ops.Pop();
                    lastTokenIsOp = false;
                    continue;
                }
            }
            
            while (ops.Count > 0 && ops.Peek() != "(" && GetPriority(token) <= GetPriority(ops.Peek()))
                PushOperation(ops, polish);
            
            ops.Push(token);
            lastTokenIsOp = true;
        }

        while (ops.Count > 0)
            PushOperation(ops, polish);

        return polish.Pop();
    }

    private static void PushOperation(Stack<string> operations, Stack<string> polish)
    {
        var op = operations.Pop();
        var val1 = polish.Pop();
        var val2 = polish.Pop();
        polish.Push(val2 + " " + val1 + " " + op);
    }

    private static int GetPriority(string s) =>
        s switch
        {
            "+" => 1,
            "-" => 1,
            _ => 2
        };
}
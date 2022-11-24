using System.Text.RegularExpressions;
using Hw11.Exceptions;
using static Hw11.ErrorMessages.MathErrorMessager;

namespace Hw11.Services;

public static class ParserToReversePolishNotation
{
    private static readonly Regex Delimiters = new("(?<=[-+*/()])|(?=[-+*/()])");
    private static readonly Regex Numbers = new(@"^\d+");

    private static readonly Dictionary<string, int> Priority = new()
    {
        { "(", 0 },
        { ")", 0 },
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 }
    };

    public static string Parse(string input) => ToReversePolishNotation(input);
    
    // Lasciate ogne speranza, voi ch'entrate
    private static string ToReversePolishNotation(string input)
    {
        var ops = new Stack<string>();
        var polish = new Stack<string>();
        var inputSplited = Delimiters.Split(input.Replace(" ", ""));
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
            if (token == "-" && lastTokenIsOp)
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

            while (ops.Count > 0 && Priority[token] <= Priority[ops.Peek()])
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

    public static void Validate(string? input)
    {
        if (string.IsNullOrEmpty(input))
            throw new Exception(EmptyString);
        if (!CheckCorrectParenthesis(input))
            throw new InvalidSyntaxException(IncorrectBracketsNumber);
        var inputSplited = Delimiters.Split(input.Replace(" ", ""))
            .Where(c => c is not "").ToArray();
        foreach (var c in input.Where(c => 
                     !Numbers.IsMatch(c.ToString()) && !new[] { '+', '-', '*', '/', '(', ')', '.', ' ' }.Contains(c)))
            throw new InvalidSymbolException(UnknownCharacterMessage(c));
        if (!Numbers.IsMatch(inputSplited[0]) && !new[] { "-", "(" }.Contains(inputSplited[0]))
            throw new InvalidSyntaxException(StartingWithOperation);
        if (!Numbers.IsMatch(inputSplited[^1]) && inputSplited[^1] != ")")
            throw new InvalidSyntaxException(EndingWithOperation);
        var lastToken = "";
        var lastTokenIsOp = true;
        foreach (var token in inputSplited)
        {
            if (Numbers.IsMatch(token))
            {
                lastToken = token;
                lastTokenIsOp = false;
                if (!double.TryParse(token, out _))
                    throw new InvalidNumberException(NotNumberMessage(token));
                continue;
            }
            if (token == "-" && lastTokenIsOp)
            {
                lastToken = token;
                lastTokenIsOp = false;
                continue;
            }
            switch (token)
            {
                case "(":
                    lastToken = token;
                    lastTokenIsOp = true;
                    continue;
                case ")":
                {
                    if (lastTokenIsOp)
                        throw new InvalidSyntaxException(OperationBeforeParenthesisMessage(lastToken));
                    lastToken = token;
                    lastTokenIsOp = false;
                    continue;
                }
            }

            if (lastTokenIsOp)
            {
                if (lastToken == "(")
                    throw new InvalidSyntaxException(InvalidOperatorAfterParenthesisMessage(token));
                throw new InvalidSyntaxException(TwoOperationInRowMessage(lastToken, token));
            }

            lastToken = token;
            lastTokenIsOp = true;
        }
    }

    private static bool CheckCorrectParenthesis(string input)
    {
        var openedParenthesis = 0;
        foreach (var c in input)
            switch (c)
            {
                case '(':
                    openedParenthesis++;
                    break;
                case ')' when openedParenthesis == 0:
                    return false;
                case ')':
                    openedParenthesis--;
                    break;
            }

        return openedParenthesis == 0;
    }
}
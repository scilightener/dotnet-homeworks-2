using System.Linq.Expressions;
using Hw11.Expressions;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public Task<double> CalculateMathExpressionAsync(string? expression)
    {
        ParserToReversePolishNotation.Validate(expression);
        var polishString = ParserToReversePolishNotation.Parse(expression!);
        var expr = ExpressionTree.ConvertToExpression(polishString);
        return ExpressionTreeVisitor.VisitExpression((dynamic)expr);
    }
}
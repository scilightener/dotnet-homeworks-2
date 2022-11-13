using System.Linq.Expressions;
using static Hw10.ErrorMessages.MathErrorMessager;

namespace Hw10.Services.Expressions;

public class ExpressionTreeVisitor : ExpressionVisitor
{
    protected override Expression VisitBinary(BinaryExpression root)
    {
        var result = CompileTreeAsync(root.Left, root.Right).Result;

        return root.NodeType switch
        {
            ExpressionType.Add => Expression.Add(Expression.Constant(result[0]), Expression.Constant(result[1])),
            ExpressionType.Subtract => Expression.Subtract(Expression.Constant(result[0]),
                Expression.Constant(result[1])),
            ExpressionType.Multiply => Expression.Multiply(Expression.Constant(result[0]),
                Expression.Constant(result[1])),
            _ => result[1] < double.Epsilon
                ? throw new Exception(DivisionByZero)
                : Expression.Divide(Expression.Constant(result[0]), Expression.Constant(result[1]))
        };
    }

    public static Task<Expression> VisitExpression(Expression expr) =>
        Task.Run(() => new ExpressionTreeVisitor().Visit(expr));

    private static async Task<double[]> CompileTreeAsync(Expression left, Expression right)
    {
        await Task.Delay(1000);
        var task1 = Task.Run(() => Expression.Lambda<Func<double>>(left).Compile().Invoke());
        var task2 = Task.Run(() => Expression.Lambda<Func<double>>(right).Compile().Invoke());
        return await Task.WhenAll(task1, task2);
    }
}
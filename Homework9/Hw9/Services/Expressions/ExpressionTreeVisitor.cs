using System.Linq.Expressions;
using static Hw9.ErrorMessages.MathErrorMessager;

namespace Hw9.Services.Expressions;

public class ExpressionTreeVisitor : ExpressionVisitor
{
    protected override Expression VisitBinary(BinaryExpression root)
    {
        var result = CompileTreeAsync(root.Left, root.Right).Result;

        switch (root.NodeType)
        {
            case ExpressionType.Add:
                return Expression.Add(Expression.Constant(result[0]), Expression.Constant(result[1]));
            case ExpressionType.Subtract:
                return Expression.Subtract(Expression.Constant(result[0]), Expression.Constant(result[1]));
            case ExpressionType.Multiply:
                return Expression.Multiply(Expression.Constant(result[0]), Expression.Constant(result[1]));
            default:
                if (result[1] < double.Epsilon)
                    throw new Exception(DivisionByZero);
                return Expression.Divide(Expression.Constant(result[0]), Expression.Constant(result[1]));
        }
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
namespace Hw8.Calculator
{
    public class Calculator : ICalculator
    {
        public double Calculate(double value1, Operation operation, double value2)
            => operation switch
            {
                Operation.Plus => Plus(value1, value2),
                Operation.Minus => Minus(value1, value2),
                Operation.Multiply => Multiply(value1, value2),
                Operation.Divide => Divide(value1, value2),
                _ => throw new InvalidOperationException(Messages.InvalidOperationMessage)
            };

        public double Plus(double val1, double val2) => val1 + val2;

        public double Minus(double val1, double val2) => val1 - val2;

        public double Multiply(double val1, double val2) => val1 * val2;

        public double Divide(double val1, double val2) => val2 < double.Epsilon
            ? throw new InvalidOperationException(Messages.DivisionByZeroMessage)
            : val1 / val2;
    }
}
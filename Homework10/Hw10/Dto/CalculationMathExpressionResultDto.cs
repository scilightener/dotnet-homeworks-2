namespace Hw10.Dto;

public class CalculationMathExpressionResultDto
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = null!;
    public double Result { get; set; }
    
    public CalculationMathExpressionResultDto(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
    
    public CalculationMathExpressionResultDto(double result)
    {
        IsSuccess = true;
        Result = result;
    }
    
    // without it everything breaks... i don't know why.......
    public CalculationMathExpressionResultDto() { }
}
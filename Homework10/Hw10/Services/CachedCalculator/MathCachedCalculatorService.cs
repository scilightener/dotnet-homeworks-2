using Hw10.DbModels;
using Hw10.Dto;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly ApplicationContext _dbContext;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(ApplicationContext dbContext, IMathCalculatorService simpleCalculator)
	{
		_dbContext = dbContext;
		_simpleCalculator = simpleCalculator;
	}

	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		var res = _dbContext.SolvingExpressions
			.Where(e => e.Expression.Equals(expression));
		if (res.Any())
			return new CalculationMathExpressionResultDto(res.First().Result);
		var calculated = await _simpleCalculator.CalculateMathExpressionAsync(expression);
		if (!calculated.IsSuccess)
			return calculated;
		_dbContext.SolvingExpressions.Add(new SolvingExpression(expression!, calculated.Result));
		await _dbContext.SaveChangesAsync();
		return calculated;
	}
}
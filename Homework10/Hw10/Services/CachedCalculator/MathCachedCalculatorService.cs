using Hw10.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly IMemoryCache _cache;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(IMemoryCache cache, IMathCalculatorService simpleCalculator)
	{
		_cache = cache;
		_simpleCalculator = simpleCalculator;
	}

	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		if (expression is null)
			return await _simpleCalculator.CalculateMathExpressionAsync(expression);
		if (_cache.TryGetValue(expression, out double res))
			return new CalculationMathExpressionResultDto(res);
		var calculated = await _simpleCalculator.CalculateMathExpressionAsync(expression);
		if (!calculated.IsSuccess)
			return calculated;
		_cache.Set(expression, calculated.Result);
		return calculated;
	}
}
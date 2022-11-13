﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Hw10.DbModels;

[ExcludeFromCodeCoverage]
public class SolvingExpression
{
	public int SolvingExpressionId { get; set; }

	[Required] public string Expression { get; set; }

	[Required] 
	public double Result { get; set; }

	public SolvingExpression(string expression, double result)
	{
		Expression = expression;
		Result = result;
	}
}
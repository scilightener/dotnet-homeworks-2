using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    private readonly IParser _parser;
    public CalculatorController(IParser parser) => _parser = parser;

    public ActionResult<double> Calculate(
        [FromServices] ICalculator calculator,
        [FromQuery] string val1,
        [FromQuery] string operation,
        [FromQuery] string val2)
        => _parser.TryParseArguments(val1, operation, val2, out var result) switch
        {
            ParseStatus.ArgumentError => BadRequest(Messages.InvalidNumberMessage),
            ParseStatus.OperationError => BadRequest(Messages.InvalidOperationMessage),
            ParseStatus.ZeroDivision => Ok(Messages.DivisionByZeroMessage),
            _ => Ok(calculator.Calculate(result.Val1, result.Operation, result.Val2))
        };
    
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}
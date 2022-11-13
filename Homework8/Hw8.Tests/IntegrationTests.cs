using System.Net.Http;
using System.Threading.Tasks;
using Hw8.Calculator;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Hw8.Tests;

public class IntegrationTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly string _url = "https://localhost:7008";

    public IntegrationTests(WebApplicationFactory<Program> fixture)
    {
        _client = fixture.CreateClient();
    }
    
    [Theory]
    [InlineData("9", Operation.Plus, "4", "13")]
    [InlineData("1000", Operation.Minus, "4.53", "995.47")]
    [InlineData("63", Operation.Divide, "21", "3")]
    [InlineData("56", Operation.Multiply, "7", "392")]
    public async Task Calculate_CorrectArguments_CorrectResultReturned(string val1, Operation operation, string val2, string expected)
    {
        var response = await _client.GetAsync($"{_url}/Calculator/Calculate?val1={val1}&operation={operation}&val2={val2}");
        var actual = await response.Content.ReadAsStringAsync();
        Assert.Equal(expected, actual);
    }
        
    [Theory]
    [InlineData("a", Operation.Plus, "4", Messages.InvalidNumberMessage)]
    [InlineData("1000", Operation.Minus, "b",  Messages.InvalidNumberMessage)]
    [InlineData("63", Operation.Invalid, "3", Messages.InvalidOperationMessage)]
    [InlineData("63", Operation.Divide, "0", Messages.DivisionByZeroMessage)]
    public async Task Calculate_IncorrectArguments_ExceptionStringReturned(string val1, Operation operation, string val2, string expected)
    {
        var response = await _client.GetAsync($"{_url}/Calculator/Calculate?val1={val1}&operation={operation}&val2={val2}");
        var actual = await response.Content.ReadAsStringAsync();
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("1", "qwerty", "4", Messages.InvalidOperationMessage)]
    public async Task Calculate_UnavailableOperation_InvalidOperationMessageReturned(string val1, string operation, string val2, string expected)
    {
        var response = await _client.GetAsync($"{_url}/Calculator/Calculate?val1={val1}&operation={operation}&val2={val2}");
        var actual = await response.Content.ReadAsStringAsync();
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("val1=1", Messages.InvalidNumberMessage)]
    [InlineData("val2=1", Messages.InvalidNumberMessage)]
    [InlineData("operation=plus", Messages.InvalidNumberMessage)]
    [InlineData("val1=1&operation=plus", Messages.InvalidNumberMessage)]
    [InlineData("operation=plus&val2=1", Messages.InvalidNumberMessage)]
    [InlineData("val1=1&val2=1", Messages.InvalidOperationMessage)]
    [InlineData("", Messages.InvalidNumberMessage)]
    public async Task Calculate_ArgsLengthLessThan3_ErrorMessageReturned(string query, string expected)
    {
        var response = await _client.GetAsync($"{_url}/Calculator/Calculate?{query}");
        var actual = await response.Content.ReadAsStringAsync();

        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("val124=1&operation=plus&val2=2", Messages.InvalidNumberMessage)]
    [InlineData("val1=1&operiowgdsvsa=plus&val2=2", Messages.InvalidOperationMessage)]
    [InlineData("val1=1&operation=plus&val21=2", Messages.InvalidNumberMessage)]
    [InlineData("", Messages.InvalidNumberMessage)]
    public async Task Calculate_WrongArgumentsNames_ErrorMessageReturned(string query, string expected)
    {
        var response = await _client.GetAsync($"{_url}/Calculator/Calculate?{query}");
        var actual = await response.Content.ReadAsStringAsync();

        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("val1=1&val2=2&operation=plus", "3")]
    [InlineData("val2=1&val1=2&operation=plus", "3")]
    [InlineData("operation=plus&val1=1&val2=2", "3")]
    [InlineData("operation=plus&val2=1&val1=2", "3")]
    public async Task Calculate_IncorrectArgsOrder_Ok(string query, string expected)
    {
        var response = await _client.GetAsync($"{_url}/Calculator/Calculate?{query}");
        var actual = await response.Content.ReadAsStringAsync();

        Assert.Equal(expected, actual);
    }
}
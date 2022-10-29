module Hw5Tests.ParserTests

open Hw5
open Hw5.Calculator
open Hw5.Parser
open Microsoft.FSharp.Core
open Xunit
open Xunit.Sdk

let epsilon: decimal = 0.001m
    
[<Theory>]
[<InlineData("15", "+", "5", 20)>]
[<InlineData("15", "-", "5", 10)>]
[<InlineData("15", "*", "5", 75)>]
[<InlineData("15", "/", "5",  3)>]
[<InlineData("15.6", "+", "5.6", 21.2)>]
[<InlineData("15.6", "-", "5.6", 10)>]
[<InlineData("15.6", "*", "5.6", 87.36)>]
[<InlineData("15.6", "/", "5.6", 2.7857)>]
let ``values parsed correctly`` (value1, operation, value2, expectedValue) =
    //arrange
    let values = [|value1;operation;value2|]
    
    //act
    let result = parseCalcArguments values
    
    //assert
    match result with
    | Ok resultOk ->
        match resultOk with
        | arg1, operation, arg2 -> Assert.True((abs (expectedValue - Calculator.calculate arg1 operation arg2)) |> decimal < epsilon)
    | Error er -> XunitException "$Expected ok but was {er}" |> raise
        
[<Theory>]
[<InlineData("f", "+", "3")>]
[<InlineData("3", "+", "f")>]
[<InlineData("a", "+", "f")>]
let ``Incorrect values return Error`` (value1, operation, value2) =
    //arrange
    let args = [|value1;operation;value2|]
    
    //act
    let result = parseCalcArguments args
    
    //assert
    match result with
    | Ok _ -> XunitException "$Expected Message.WrongArgFormat but was ok" |> raise
    | Error resultError -> Assert.Equal(resultError, Message.WrongArgFormat)
    
[<Theory>]
[<InlineData("1", ".", "1")>]
[<InlineData("1", "%", "1")>]
[<InlineData("1", "^", "1")>]
[<InlineData("1.0", ".", "1.0")>]
[<InlineData("1.0", ".", "1")>]
[<InlineData("1", ".", "1.0")>]
let ``Incorrect operations return Error`` (value1, operation, value2) =
    //arrange
    let args = [|value1;operation;value2|]
    
    //act
    let result = parseCalcArguments args
    
    //assert
    match result with
    | Ok _ -> XunitException "$Expected Message.WrongArgFormatOperation but was ok" |> raise
    | Error resultError -> Assert.Equal(resultError, Message.WrongArgFormatOperation)
    
[<Fact>]
let ``Incorrect argument count  -> Error(Message.WrongArgLength)`` () =
    //arrange
    let args = [|"3";"+";"4";"5"|]
    
    //act
    let result = parseCalcArguments args
    
    //assert
    match result with
    | Ok _ -> XunitException "$Expected Message.WrongArgLength but was ok" |> raise
    | Error resultError -> Assert.Equal(resultError, Message.WrongArgLength)
    
[<Theory>]
[<InlineData("1", "/", "0")>]
[<InlineData("1", "/", "0.0")>]
[<InlineData("1.0", "/", "0")>]
[<InlineData("1.0", "/", "0.0")>]
let ``any / 0 -> Error(Message.DivideByZero)`` (value1, operation, value2) =
    //arrange
    let args = [|value1;operation;value2|]
    
    //act
    let result = parseCalcArguments args
    
    //assert
    match result with
    | Ok _ -> XunitException "$Expected Message.DivideByZero but was ok" |> raise
    | Error resultError -> Assert.Equal(resultError, Message.DivideByZero)
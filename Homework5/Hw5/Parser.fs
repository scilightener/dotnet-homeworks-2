module Hw5.Parser

open System
open Hw5.Calculator

let (|Decimal|_|) arg =
    match Decimal.TryParse (arg: string) with
    | true, decimal -> Some decimal
    | _ -> None

let (|CalculatorOperation|_|) arg =
    match arg with
    | Calculator.plus -> Some CalculatorOperation.Plus
    | Calculator.minus -> Some CalculatorOperation.Minus
    | Calculator.multiply -> Some CalculatorOperation.Multiply
    | Calculator.divide -> Some CalculatorOperation.Divide
    | _ -> None

let isArgLengthSupported (args:string[]): bool =
    match args.Length with
    | 3 -> true
    | _ -> false

let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    if not(isArgLengthSupported args) then Error Message.WrongArgLength
    else 
        match args[0] with
        | Decimal arg1 ->
            match args[1] with
            | CalculatorOperation operation ->
                match args[2] with
                | Decimal arg2 -> Ok (arg1, operation, arg2)
                | _ -> Error Message.WrongArgFormat
            | _ -> Error Message.WrongArgFormatOperation
        | _ -> Error Message.WrongArgFormat

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1: decimal, operation, arg2: decimal): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | CalculatorOperation.Divide ->
        if arg2 = Decimal.Zero then Error Message.DivideByZero
        else Ok (arg1, operation, arg2)
    | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    MaybeBuilder.maybe
        {
            let! argsParsed = parseArgs args
            let! argsNoZeroDivision = isDividingByZero argsParsed
            return argsNoZeroDivision
        }
module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    args.Length = 3

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> CalculatorOperation.Undefined
    
let parseCalcArguments(args : string[]) =
    if not (isArgLengthSupported args) then
        ArgumentException "The number of arguments is not 3"
        |> raise
    let ok, val1 = System.Double.TryParse args[0]
    if not ok then
        ArgumentException "Argument 1 is not a number"
        |> raise
    let ok, val2 = System.Double.TryParse args[2]
    if not ok then
        ArgumentException "Argument 2 is not a number"
        |> raise
    let op = parseOperation args[1]
    if op = CalculatorOperation.Undefined then
        ArgumentException "Unsupported operation"
        |> raise
    {arg1 = val1; arg2 = val2; operation = op}
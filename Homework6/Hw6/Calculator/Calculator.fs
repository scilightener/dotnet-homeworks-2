module Hw6.Calculator

type CalculatorOperation =
     | Plus = 0
     | Minus = 1
     | Multiply = 2
     | Divide = 3

[<Literal>] 
let Plus = "+"

[<Literal>] 
let Minus = "-"

[<Literal>] 
let Multiply = "*"

[<Literal>] 
let Divide = "/"

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline calculate (value1: decimal, operation: string, value2: decimal) =
    match operation with
    | Plus -> Ok $"{value1 + value2}"
    | Minus -> Ok $"{value1 - value2}"
    | Multiply -> Ok $"{value1 * value2}"
    | Divide -> match value2 with
        | 0m -> Ok "DivideByZero"
        | _ -> Ok $"{value1 / value2}"
    | _ -> Error $"Could not parse value '{operation}'"
module Hw6.Handlers

open Giraffe
open Calculator
open Arguments

let convertOperation operation =
    match operation with
    | "Plus" -> Plus
    | "Minus" -> Minus
    | "Multiply" -> Multiply
    | "Divide" -> Divide
    | _ -> operation
    
let calculatorHandler : HttpHandler =
    fun next ctx ->
        let result = MaybeBuilder.maybe {
            let! args = ctx.TryBindQueryString<Arguments>()
            let! parsed = calculate (args.value1, args.operation |> string |> convertOperation, args.value2)
            return parsed
        }
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx
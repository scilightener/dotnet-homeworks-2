open Hw5
open Parser
open Calculator

[<EntryPoint>]
let main (args: string[]) =
    match parseCalcArguments args with
    | Ok result ->
        match result with
        | arg1, operation, arg2 -> printf $"{calculate arg1 operation arg2}"
    | Error error -> printf $"Return {error} on {args}"
    0
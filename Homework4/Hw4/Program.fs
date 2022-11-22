open Hw4

[<EntryPoint>]
let main (args: string[]) =
    let result = Parser.parseCalcArguments args
    printfn $"%f{Calculator.calculate result.arg1 result.operation result.arg2}"
    0
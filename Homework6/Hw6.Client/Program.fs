module Hw6.Client
open System
open System.Net.Http

let convertOperation operation =
    match operation with
    | "+" -> "Plus"
    | "-" -> "Minus"
    | "*" -> "Multiply"
    | "/" -> "Divide"
    | _ -> operation

let handleQueryAsync(client : HttpClient) (url : string) =
    async {
        let! response = Async.AwaitTask (client.GetAsync url)
        let! res = Async.AwaitTask (response.Content.ReadAsStringAsync())
        return res
    }
    
[<EntryPoint>]
let main args =
    use handler = new HttpClientHandler()
    use client = new HttpClient(handler)
    let mutable input = Console.ReadLine()
    while not(input = "quit") do
        let args = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        match args.Length with
        | 3 ->
            let url = $"http://localhost:5000/calculate?value1={args[0]}&operation={args[1] |> convertOperation}&value2={args[2]}";
            printfn $"result: {handleQueryAsync client url |> Async.RunSynchronously}"
        | _ -> printfn "wrong argument length, try again"
        input <- Console.ReadLine()
    0
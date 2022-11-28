module Hw6.App

open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Handlers

let webApp =
    choose [
        GET >=> choose [
             route "/" >=> text "Use /calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
             route "/calculate" >=> calculatorHandler
        ]
        setStatusCode 404 >=> text "Not Found" 
    ]
    
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore
        services.AddMiniProfiler() |> ignore

    member _.Configure (app : IApplicationBuilder) (_ : IHostEnvironment) (_ : ILoggerFactory) =
        app.UseMiniProfiler()
            .UseGiraffe webApp
        
[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()
    0
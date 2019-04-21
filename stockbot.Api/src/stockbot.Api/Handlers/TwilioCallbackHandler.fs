module Handlers.TwilioCallbackHandler

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks
open Giraffe
open Models.Notification
open stockbot.Parser
open stockbot.Common
open stockbot.Visualise
open stockbot.Data

let twilioCallbackHandler (next : HttpFunc) (ctx : HttpContext) =    
    let notification = ctx.BindFormAsync<Notification>() 
    let parserOutcome = notification.Result.Body |> Parse
    match parserOutcome with
    | Outcome.Success parserOutcome ->
        let apiOutcome = parserOutcome |> StockData.GetData
        match apiOutcome with
        | Outcome.Success apiOutcome ->
            GenerateChart apiOutcome            
            setStatusCode 200 next ctx            
        | Outcome.Failure apiOutcome ->
            printfn "%s" apiOutcome
            setStatusCode 400 next ctx
    | Outcome.Failure parserOutcome ->
        printfn "%s"parserOutcome
        setStatusCode 400 next ctx     
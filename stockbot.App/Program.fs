open stockbot.Common
open stockbot.Data
open stockbot.Parser
open stockbot.Visualise
open Twilio

[<EntryPoint>]
let main argv =
    let parserOutcome = "@StockBot MSFT 01/01/2000 01/01/2018" |> Parse
    match parserOutcome with
    | Outcome.Success parserOutcome ->
        let apiOutcome = parserOutcome |> StockData.GetData
        match apiOutcome with
        | Outcome.Success apiOutcome ->
            GenerateChart apiOutcome            
            1            
        | Outcome.Failure apiOutcome ->
            printfn "%s" apiOutcome
            0
    | Outcome.Failure parserOutcome ->
        printfn "%s"parserOutcome
        0

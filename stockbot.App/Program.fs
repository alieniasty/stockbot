open stockbot.Common
open stockbot.Data
open stockbot.Parser

[<EntryPoint>]
let main argv =
    let parserOutcome = "@StockBot MSFT 2015" |> Parse
    match parserOutcome with
    | Outcome.Success parserOutcome ->
        let apiOutcome = parserOutcome |> StockData.GetData
        match apiOutcome with
        | Outcome.Success apiOutcome ->
            printfn "%A" apiOutcome
            1
        | Outcome.Failure apiOutcome ->
            printfn "%s" apiOutcome
            1
    | Outcome.Failure parserOutcome ->
        printfn "%s"parserOutcome
        1

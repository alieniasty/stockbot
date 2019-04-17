module stockbot.Visualise

open stockbot.Data.StockData
open FSharp.Charting

    let GenerateChart (pricesAggregation : Prices) =
        let x = pricesAggregation.Prices |> Array.map (fun p -> p.Close)
        let prices = pricesAggregation.Prices
        [ for date in (prices |> Array.map (fun l -> l.Date)) -> (date, (prices |> Array.find (fun f -> f.Date = date)).Close)]
        |> Chart.Line
        |> Chart.Show
    
    
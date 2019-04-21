module stockbot.Visualise

open stockbot.Data.StockData
open XPlot.GoogleCharts

    let GenerateChart (pricesAggregation : Prices) =
        let prices = pricesAggregation.Prices
        [ for date in (prices |> Array.map (fun l -> l.Date)) -> (date, (prices |> Array.find (fun f -> f.Date = date)).Close)]
        |> Chart.Line
        |> Chart.Show
    
    
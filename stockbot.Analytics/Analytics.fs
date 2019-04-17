module stockbot.Analytics

open stockbot.Common
open stockbot.Data.StockData

    type Summary =
        {
            Ticker : string
            High : Price
            Low : Price
            Delta : double
        }
    with
        override this.ToString() =
            sprintf "%s High: %.2f (%s) Low: %.2f (%s) Δ: %.2f"
                this.Ticker
                this.High.Close (this.High.Date.ToShortDateString())
                this.Low.Close (this.Low.Date.ToShortDateString())
                this.Delta

    let Summarize (prices : Prices) =
        match prices with
        | { Ticker = _; Prices = [||] } -> Outcome.Failure "No data to summarize"
        | _ ->
            let high = prices.Prices |> Array.maxBy (fun p -> p.Close)
            let low = prices.Prices |> Array.minBy (fun p -> p.Close)
            let opening = prices.Prices.[0].Close
            let close = (prices.Prices |> Array.last).Close
            let delta = close - opening
            {
                Ticker = prices.Ticker
                High = high
                Low = low
                Delta = delta            
            } |> Outcome.Success

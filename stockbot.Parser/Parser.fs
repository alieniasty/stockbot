module stockbot.Parser

open System
open System.Globalization
open Common    
open stockbot.Data
    
    let private parseDate (s : string) =
        DateTime.ParseExact(s,
                            "d/M/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.AssumeLocal)
      
    let Parse (text : string) =
        let elements = text.Split ' '
        if TickerData.Parse elements.[1] = true
        then
            match elements with
            | [|sender; ticker; year|] ->
                let year = Int32.Parse year
                {
                    Sender = sender
                    Ticker = ticker
                    From = DateTime(year, 1, 1)
                    Until = DateTime(year, 12, 31)
                } |> Outcome.Success
            | [|sender; ticker; from; until|] ->
                {
                    Sender = sender
                    Ticker = ticker
                    From = from |> parseDate
                    Until = until |> parseDate
                } |> Outcome.Success
            | _ -> Outcome.Failure "Invalid text"
        else
            Outcome.Failure "Invalid ticker"
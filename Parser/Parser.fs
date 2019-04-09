module Parser

open System
open System.Globalization

    type Outcome<'S> =
    | Success of result:'S
    | Failure of message:string

    type Query =
        {
            Sender : string
            Ticker : string
            From : DateTime
            Until : DateTime
        }
    
    let private parseDate (s : string) =
        DateTime.ParseExact(s,
                            "d/M/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.AssumeLocal)
      
    let Parse (text : string) =
        let elements = text.Split ' '
        match elements with
        | [|sender; ticker; from; until|] ->
            {
                Sender = elements.[0]
                Ticker = elements.[1]
                From = elements.[2] |> parseDate
                Until = elements.[3] |> parseDate
            } |> Outcome.Success
        | _ -> Outcome.Failure "Invalid text"

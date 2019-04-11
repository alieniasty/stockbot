module stockbot.Common

open System

type Outcome<'S, 'F> =
    | Success of result:'S
    | Failure of result:'F
    
    
type Query =
        {
            Sender : string
            Ticker : string
            From : DateTime
            Until : DateTime
        }
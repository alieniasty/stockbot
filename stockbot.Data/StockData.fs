module stockbot.Data.StockData

open stockbot.Common
open System
open FSharp.Data

    type Price =
        {
            Date : DateTime
            Close : double
        }
        
    type Prices =
        {
            Ticker : string
            Prices : Price []
        }

    let private BuildUrl ticker from until =
        let (~~) (d : DateTime) = d.ToString("yyyy-MM-dd")
        sprintf "http://www.quandl.com/api/v3/datasets/wiki/%s.csv?start_date=%s&end_date=%s&order=asc" ticker ~~from ~~until
        
    [<Literal>]
    let template = __SOURCE_DIRECTORY__ + @"\StockPrices.csv"
    
    type Quandl = CsvProvider<template>
    
    let GetData (query : Query) =
        let url = BuildUrl query.Ticker query.From query.Until
        try
            let data = Quandl.Load url
            let prices =
                data.Rows
                |> Seq.map (fun r ->
                    {
                        Date = r.Date
                        Close = r.Close |> double
                    })
                |> Array.ofSeq
            {
                Ticker = query.Ticker
                Prices = prices
            }
            |> Outcome.Success
        with
        | e -> Outcome.Failure e.Message
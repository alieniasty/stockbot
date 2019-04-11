module stockbot.Data.TickerData

    open System.IO.Compression
    open FSharp.Data
    open System.Net
    open System.IO
    
    type Tickers = CsvProvider<Schema="code,name,description,refreshed_at,from_date,to_date", HasHeaders=false>
    
    let Parse (ticker : string) =
        let mutable ticker = ticker.ToUpper()
        
        use webClient = new WebClient()
        let bytes = webClient.DownloadData("https://www.quandl.com/api/v3/databases/EOD/metadata")
        use stream = new MemoryStream(bytes)
        use archive = new ZipArchive(stream)
        let entry = archive.Entries.[0]
        use dataStream = entry.Open()
        
        let data = Tickers.Load dataStream
        data.Rows
        |> Seq.map (fun r -> r.Code)
        |> List.ofSeq
        |> List.exists ((=) ticker)
        


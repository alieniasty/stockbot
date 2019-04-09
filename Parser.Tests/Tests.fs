module Parser.Tests

open Xunit
open Parser
open System

type ``Parser tests``() =
    
    [<Theory>]
    [<InlineData("@AnotherBot LNKD 1/1/2000 31/12/2015", "@AnotherBot", "LNKD")>]
    [<InlineData("@StockBot MSFT 1/1/2000 31/12/2015", "@StockBot", "MSFT")>]
    member this.``Given correct tweet, Parser constructs proper record.``(text, sender, ticker) =
        let expected =
            {
                Sender = sender
                Ticker = ticker
                From = DateTime(2000, 1, 1)
                Until = DateTime(2015, 12, 31)
            } |> Outcome.Success
        let actual = Parse text
        Assert.Equal(expected, actual)
        
    [<Theory>]
    [<InlineData("@Ano", "@AnotherBot", "LNKD")>]
    [<InlineData("/1/2000 31/12/2015", "@StockBot", "MSFT")>]
    member this.``Given incorrect tweet, Parser constructs proper record.``(text, sender, ticker) =
        let expected = Outcome.Failure "Invalid text"
        let actual = Parse text
        Assert.Equal(expected, actual)
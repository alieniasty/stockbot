module Models.Notification

open System

[<CLIMutable>]
type Notification =
    {
        SmsMessageSid   : string
        NumMedia   : string
        SmsSid   : string
        SmsStatus   : string
        Body   : string
        To   : string
        NumSegments   : string
        MessageSid   : string
        AccountSid   : string
        From   : string
        ApiVersion   : string
    }
open System
open System.IO

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

if not (File.Exists ".paket/paket.exe") then
    let url = "https://github.com/fsprojects/Paket/releases/download/0.26.3/paket.exe"
    use wc = new Net.WebClient() in let tmp = Path.GetTempFileName() in wc.DownloadFile(url, tmp); File.Move(tmp,Path.GetFileName url)

#r ".paket/paket.exe"

Paket.Dependencies.Install """
    source https://nuget.org/api/v2
    nuget Suave
    nuget FSharp.Data 
    nuget FSharp.Charting
""";;

#r "packages/Suave/lib/net40/Suave.dll"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "packages/FSharp.Charting/lib/net40/FSharp.Charting.dll"

let ctxt = FSharp.Data.WorldBankData.GetDataContext()

let data = ctxt.Countries.Algeria.Indicators.``GDP (current LCU)``

open Suave
open Suave.Successful
open Suave.Web
open Suave.Http

startWebServer defaultConfig (OK (sprintf "Hello World! In 2010 Algeria earned %f " data.[2010]))

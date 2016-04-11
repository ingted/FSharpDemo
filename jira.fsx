#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data

type Jira = CsvProvider<"issues.csv">

let issues = Jira.GetSample()

let head = issues.Rows |> Seq.map(fun i -> i.)

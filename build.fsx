#r "packages/FAKE/tools/FakeLib.dll"

open System
open Fake
open Fake.Testing.NUnit3
open Fake.SlackNotificationHelper

let buildDir = "./build/"
let testDir = "./test/"
let dataProj = !! "FSharpDemo.Data/FSharpDemo.Data.fsproj"
let webProj = !! "FSharpDemo.Web/FSharpDemo.Web.csproj"
let testProj = !! "FSharpDemo.Test/FSharpDemo.Test.fsproj"
let endToEndProj = !! "FSharpDemo.EndToEndTest/FSharpDemo.EndToEndTest.fsproj"
let slackWebhookUrl = "https://hooks.slack.com/services/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"

Target "BuildData" (fun _ ->
    dataProj
        |> MSBuildDebug buildDir "Build"
        |> Log "Building Data Project"
)

Target "BuildWeb" (fun _ ->
    webProj
        |> MSBuildDebug buildDir "Build"
        |> Log "Building Web Project"
)

Target "BuildTest" (fun _ ->
    testProj
        |> MSBuildDebug testDir "Build"
        |> Log "Building Test Project"
)

Target "BuildEndToEndTest" (fun _ ->
    endToEndProj
        |> MSBuildDebug testDir "Build"
        |> Log "Building End to End Test Project"
)

Target "RunTests" (fun _ ->
    !! (testDir + "/FSharpDemo.Test.dll")
        |> NUnit3 (fun p ->
            {p with
                ShadowCopy = false; })
)

Target "SendSlackMessage" (fun _ -> 
    SlackNotification slackWebhookUrl (fun p ->
        {p with Text = "FAKE build ran"
                Icon_Emoji = ":boom:"
                Channel = "@jwood"
        })
        |> ignore
)

"BuildData"
    ==> "BuildWeb"
    ==> "BuildTest"
    ==> "BuildEndToEndTest"
    ==> "RunTests"
    ==> "SendSlackMessage"

RunTargetOrDefault "RunTests"

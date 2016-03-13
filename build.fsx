#r "packages/FAKE/tools/FakeLib.dll"
#r "packages/FAKE.IIS/tools/Fake.IIS.dll"

open System
open Fake
open Fake.Testing.NUnit3
open Fake.SlackNotificationHelper
open Fake.IISExpress

let buildDir = "./build/"
let testDir = "./test/"
let webSiteDir = "./web/"
let dataProj = !! "FSharpDemo.Data/FSharpDemo.Data.fsproj"
let webProj = !! "FSharpDemo.Web/FSharpDemo.Web.fsproj"
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
        |> MSBuildDebug webSiteDir "Build"
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
            {p with ShadowCopy = false })
)

Target "SendSlackMessage" (fun _ -> 
    SlackNotification slackWebhookUrl (fun p ->
        {p with Text = "FAKE build ran"
                Icon_Emoji = ":boom:"
                Channel = "@jwood"
        })
        |> ignore
)

Target "RunCanopyTests" (fun _ ->
    let host = "localhost"
    let port = 48213
    let project = "FSharpDemo.Web"

    let config = createConfigFile(project, 1, "iisexpress-template.config", webSiteDir, host, port)
    let webSiteProcess = HostWebsite id config 1

    let result =
        ExecProcess (fun info ->
            info.FileName <- (testDir @@ "FSharpDemo.EndToEndTest.exe")
            info.WorkingDirectory <- testDir
        ) (System.TimeSpan.FromMinutes 5.)

    ProcessHelper.killProcessById webSiteProcess.Id
 
    if result <> 0 then failwith "Failed result from canopy tests"
)

"BuildData"
    ==> "BuildWeb"
    ==> "BuildTest"
    ==> "BuildEndToEndTest"
    ==> "RunTests"
    ==> "SendSlackMessage"

RunTargetOrDefault "RunTests"

#r "packages/FAKE/tools/FakeLib.dll"

open System
open Fake
open Fake.Testing.NUnit3

let buildDir = "./build/"
let testDir = "./test/"
let dataProj = !! "FSharpDemo.Data/FSharpDemo.Data.fsproj"
let webProj = !! "FSharpDemo.Web/FSharpDemo.Web.csproj"
let testProj = !! "FSharpDemo.Test/FSharpDemo.Test.fsproj"

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

Target "RunTests" (fun _ ->
    !! (testDir + "/FSharpDemo.Test.dll")
        |> NUnit3 (fun p ->
            {p with
                ShadowCopy = false; })
)

"BuildData"
    ==> "BuildWeb"
    ==> "BuildTest"
    ==> "RunTests"

RunTargetOrDefault "RunTests"

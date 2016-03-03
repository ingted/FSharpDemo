#r "packages/FAKE/tools/FakeLib.dll"

open System
open Fake

let buildDir = "./build/"
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
        |> MSBuildDebug buildDir "Build"
        |> Log "Building Test Project"
)

"BuildData"
    ==> "BuildWeb"
    ==> "BuildTest"

RunTargetOrDefault "BuildTest"

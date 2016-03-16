open canopy
open runner
open System

start chrome
pin FullScreen

let testUrl = "http://localhost:48213/"

let homePage =
    context "Home page"

    once (fun _ ->
        url testUrl
    )

    "Test home page loads with data" &&& fun _ ->
        displayed ".table"

let editPage =
    context "Edit page"

    once (fun _ ->
        url testUrl
        click (first "#edit")
    )

    "Test on the edit page of ID 1" &&& fun _ ->
        "#ProductName" == "Red shirt"

run()

printfn "press [enter] to exit"
Console.ReadLine() |> ignore

quit()
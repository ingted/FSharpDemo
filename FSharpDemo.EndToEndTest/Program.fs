open canopy
open runner
open System

start chrome

let testUrl = "http://localhost:48213/"

"Test home page" &&& fun _ ->
    url testUrl

    "#name" == "Product Name"
    "#count" == "Product Count"
run()

printfn "press [enter] to exit"
System.Console.ReadLine() |> ignore

quit()
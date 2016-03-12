open canopy
open runner
open System

//let (¯\_(ツ)_/¯) = ignore

start chrome

let testUrl = "http://localhost:48213/"

"Test home page loads" &&& fun _ ->
    url testUrl

    "#name" == "Product Name"
    "#count" == "Product Count"
run()

"Test data gets displayed" &&! skipped

printfn "press [enter] to exit"
Console.ReadLine() |> ignore

quit()
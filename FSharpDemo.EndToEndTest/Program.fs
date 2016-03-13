open canopy
open runner
open System
open reporters

start chrome
pin FullScreen

let testUrl = "http://localhost:48213/"

"Test home page loads" &&&& fun _ ->
    url testUrl

    "#name" == "Name"
    "#count" == "Count"

"Test data gets displayed" &&! skipped

run()

//printfn "press [enter] to exit"
//Console.ReadLine() |> ignore

quit()
open canopy
open runner
open System

start chrome

"Test canopy" &&& fun _ ->
    url "http://localhost:48213/"

run()

printfn "press [enter] to exit"
System.Console.ReadLine() |> ignore

quit()
namespace FSharpDemo.Test

open Xunit
open FsUnit

type Class1() = 

    [<Fact>]
    member this.X() =
        let x = 4
        x |> should equal 4

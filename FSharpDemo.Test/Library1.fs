namespace FSharpDemo.Test

open NUnit.Framework
open FsUnit

[<TestFixture>]
type Class1() = 
    [<Test>]
    member this.X() =
        let x = 4
        x |> should equal 4

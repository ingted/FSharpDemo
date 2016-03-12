namespace FSharpDemo.Test

open NUnit.Framework
open FsUnit

[<TestFixture>]
type Tests() = 
    [<Test>]
    member this.``getProducts returns two items``() =
        let products = Products.getProducts() |> Seq.toList
        products.Length |> should equal 2

    [<Test>]
    member this.``getProducts doesn't return empty``() =
        let products = Products.getProducts()
        products |> should not' (equal Seq.empty)
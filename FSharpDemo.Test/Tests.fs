module Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``getProducts returns two items``() =
    let products = Products.getProducts() |> Seq.toList
    products.Length |> should equal 2

[<Test>]
let ``getProducts doesn't return empty``() =
    let products = Products.getProducts()
    products |> should not' (equal Seq.empty)

[<Test>]
let ``getProductById returns a product with ID 1``() =
    let product = Products.getProductById 1
    product.ProductName |> should equal "Red shirt"
    product.ProductId |> should equal 1
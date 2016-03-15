namespace FSharpDemo.Web.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax

type HomeController() =
    inherit Controller()
    member this.Index () = 
        let products = Products.getProducts() |> Seq.toList
        this.View(products)

    member this.Edit id =
        let product = Products.getProductById id
        this.View(product)

    [<HttpPut>]
    member this.Edit id =
        printfn ""

    member this.Add () =
        this.View()

    [<HttpPost>]
    member this.Add product =
        printfn ""
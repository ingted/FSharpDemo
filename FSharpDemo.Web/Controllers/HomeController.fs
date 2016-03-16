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
        Products.getProducts() |> Seq.toList |> this.View

    member this.Edit id =
        Products.getProductById id |> this.View

    [<HttpPost>] 
    member this.Edit product =
        Products.updateProduct product

        this.RedirectToAction("Index")

    member this.Add () =
        this.View()

    [<HttpPost>]
    member this.Add product =
        printfn ""
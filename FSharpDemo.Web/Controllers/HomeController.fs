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


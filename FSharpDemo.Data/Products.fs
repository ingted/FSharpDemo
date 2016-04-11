module Products

#if INTERACTIVE
#r "System.Data.dll"
#r "FSharp.Data.TypeProviders.dll"
#r "System.Data.Linq.dll"
#endif

open System
open System.Linq
open Microsoft.FSharp.Data.TypeProviders

let [<Literal>] connectionString = @"Data Source=JONWOOD3FEB;Initial Catalog=FSharpDemo;Integrated Security=True"

type Sql = SqlDataConnection<connectionString>
let context = Sql.GetDataContext()

context.DataContext.Log <- Console.Out

[<CLIMutable>]
type Product = {
    ProductId: int
    ProductName: string
    ProductCount: int
    ProductPrice: string
}

let private mapProduct product =
    let lastId = context.Product |> Seq.last |> fun p -> p.Product_id

    new Sql.ServiceTypes.Product
        (Product_id = lastId + 1, Product_name = product.ProductName, Product_count = Nullable<int>(product.ProductCount), Product_price = product.ProductPrice)

let getProducts() = query { 
    for c in context.Product do
    where c.Product_count.HasValue
    select (c.Product_id, c.Product_name, c.Product_count.Value, c.Product_price) } |> Seq.map(fun (id, name, count, price) -> 
        { ProductId = id; ProductName = name.Trim(); ProductCount = count; ProductPrice = price.ToString() })

let getProductById id = query {
    for c in context.Product do
    where (c.Product_id = id)
    select (c.Product_id, c.Product_name, c.Product_count.Value, c.Product_price) } |> Seq.head |> (fun (id, name, count, price) -> 
        { ProductId = id; ProductName = name.Trim(); ProductCount = count; ProductPrice = price.ToString() })

let insertProduct product =
    product |> mapProduct |> context.Product.InsertOnSubmit

    try
        context.DataContext.SubmitChanges()
    with
        | ex -> printfn "Error when inserting - %s" ex.Message

let updateProduct product = 
    let currentProduct = getProductById product.ProductId

    let record = currentProduct |> mapProduct

    record.Product_name <- product.ProductName
    record.Product_count <- Nullable<int>(product.ProductCount)
    record.Product_price <- product.ProductPrice

    try
        context.DataContext.SubmitChanges()
    with
        | ex -> printfn "Error when inserting - %s" ex.Message

let deleteProduct product =
    let currentProduct = getProductById product.ProductId

    currentProduct |> mapProduct |> context.Product.DeleteOnSubmit

    try
        context.DataContext.SubmitChanges()
    with
        | ex -> printfn "Error when deleting - %s" ex.Message

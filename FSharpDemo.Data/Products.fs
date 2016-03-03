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
let private context = Sql.GetDataContext()

type Product = {
    ProductId: int
    ProductName: string
    ProductCount: int
}

let getProducts() = query { 
    for c in context.Product do
    where c.Product_count.HasValue
    select (c.Product_id, c.Product_name, c.Product_count.Value) } |> Seq.map(fun (id, name, count) -> { ProductId = id; ProductName = name.Trim(); ProductCount = count })
open System.IO

let left, right =
    File.ReadLines "input.txt"
    |> Seq.map (fun line -> line.Split("   "))
    |> Seq.map (fun parts -> (int parts.[0], int parts.[1]))
    |> Seq.fold (fun (xs, ys) (x, y) -> (x::xs, y::ys)) ([],[])
    
let difference (x, y) = x - y |> abs

Seq.zip (left |> Seq.sort) (right |> Seq.sort)
|> Seq.map difference
|> Seq.sum
|> printfn "%i"

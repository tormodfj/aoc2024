open System.IO

let left, right =
    File.ReadLines "input.txt"
    |> Seq.map (fun line -> line.Split("   "))
    |> Seq.map (fun parts -> (int parts.[0], int parts.[1]))
    |> Seq.fold (fun (xs, ys) (x, y) -> (x::xs, y::ys)) ([],[])

let appearanceMap =
    right
    |> Seq.countBy id
    |> Map.ofSeq
    
let score x =
    x
    |> appearanceMap.TryFind
    |> Option.defaultValue 0
    |> (*) x

left
|> Seq.map score
|> Seq.sum
|> printfn "%i"

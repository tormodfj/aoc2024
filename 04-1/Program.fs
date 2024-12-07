open System
open System.IO
open System.Text.RegularExpressions

let map =
    File.ReadLines "input.txt"
    |> array2D
    
let getChar (x,y) = try map[x,y] with | _ -> '.'

let N (x, y) = $"{getChar (x,y)}{getChar (x,y-1)}{getChar (x,y-2)}{getChar (x,y-3)}"
let NE (x, y) = $"{getChar (x,y)}{getChar (x+1,y-1)}{getChar (x+2,y-2)}{getChar (x+3,y-3)}"
let E (x, y) = $"{getChar (x,y)}{getChar (x+1,y)}{getChar (x+2,y)}{getChar (x+3,y)}"
let SE (x, y) = $"{getChar (x,y)}{getChar (x+1,y+1)}{getChar (x+2,y+2)}{getChar (x+3,y+3)}"
let S (x, y) = $"{getChar (x,y)}{getChar (x,y+1)}{getChar (x,y+2)}{getChar (x,y+3)}"
let SW (x, y) = $"{getChar (x,y)}{getChar (x-1,y+1)}{getChar (x-2,y+2)}{getChar (x-3,y+3)}"
let W (x, y) = $"{getChar (x,y)}{getChar (x-1,y)}{getChar (x-2,y)}{getChar (x-3,y)}"
let NW (x, y) = $"{getChar (x,y)}{getChar (x-1,y-1)}{getChar (x-2,y-2)}{getChar (x-3,y-3)}"

let countXmas x y ch =
    let count (x,y) =
        [| N(x,y); NE(x,y); E(x,y); SE(x,y); S(x,y); SW(x,y); W(x,y); NW(x,y) |]
        |> Array.filter (fun input -> input = "XMAS")
        |> Array.length
    
    match ch with
    | 'X' -> count (x,y)
    | _ -> 0
    
map
|> Array2D.mapi countXmas
|> Seq.cast<int>
|> Seq.sum
|> printfn "%i"

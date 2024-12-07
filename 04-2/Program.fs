open System
open System.Diagnostics
open System.IO
open System.Text.RegularExpressions

let map =
    File.ReadLines "input.txt"
    |> array2D
    
let getChar (x,y) = try map[x,y] with | _ -> '.'

let NWSE (x, y) = $"{getChar (x-1,y-1)}{getChar (x,y)}{getChar (x+1,y+1)}"
let NESW (x, y) = $"{getChar (x+1,y-1)}{getChar (x,y)}{getChar (x-1,y+1)}"

let findXmas x y ch =
    let isXmas coord =
        match NWSE(coord), NESW(coord) with
        | "MAS","MAS" -> true
        | "MAS","SAM" -> true
        | "SAM","MAS" -> true
        | "SAM","SAM" -> true
        | _ -> false

    match ch with
    | 'A' when isXmas(x,y) -> 1
    | _ -> 0
    
map
|> Array2D.mapi findXmas
|> Seq.cast<int>
|> Seq.sum
|> printfn "%i"

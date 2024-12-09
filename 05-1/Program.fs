open System.IO

let input = File.ReadLines "input.txt" |> Array.ofSeq
let dividerIndex = input |> Array.findIndex ((=) "")

let rules =
    input[..dividerIndex-1]
    |> Array.map (fun line -> (int line[0..1], int line[3..4]))
        
let prints =
    input[dividerIndex+1..]
    |> Array.map (fun line -> line.Split(',') |> Array.map int)
    
let rulesComparer pageA pageB =
    if rules |> Array.contains (pageA, pageB) then -1
    else if rules |> Array.contains (pageB, pageA) then 1
    else 0
    
let sortPrint pages = pages |> Array.sortWith rulesComparer

let isCorrectOrder pages = pages = (sortPrint pages)

let findMiddlePage pages =
    let length = Array.length pages
    pages[length/2]

prints
|> Array.filter isCorrectOrder
|> Array.map findMiddlePage
|> Array.sum
|> printfn "%i"

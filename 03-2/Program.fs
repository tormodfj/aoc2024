open System.IO
open System.Text.RegularExpressions

let memory =
    File.ReadLines "input.txt"
    |> String.concat ""
    
let sanitizedMemory =
    Regex.Replace(memory, "don't\(\).*?do\(\)", "")

let validInstructions =
    Regex.Matches(sanitizedMemory, "mul\(\d{1,3},\d{1,3}\)")
    |> Seq.map (_.Value)

let evaluateInstruction ins =
    Regex.Matches(ins, "\d+")
    |> Seq.map (_.Value)
    |> Seq.map int
    |> Seq.reduce (fun x y -> x * y)

validInstructions
|> Seq.map evaluateInstruction
|> Seq.sum
|> printfn "%i"

open System.IO

let reports =
    File.ReadLines "input.txt"
    |> Seq.map (fun line -> line.Split(" ") |> Array.map int)
    
let isReportOrdered nums =
    nums = Array.sort nums ||
    nums = Array.sortDescending nums
 
let allReportGapsSafe nums =
    let diff (x,y) = x-y |> abs
    nums
    |> Seq.pairwise
    |> Seq.map diff
    |> Seq.forall (fun diff -> diff >= 1 && diff <= 3)
    
let isReportSafe report = isReportOrdered report && allReportGapsSafe report

reports
|> Seq.filter isReportSafe
|> Seq.length
|> printfn "%i"

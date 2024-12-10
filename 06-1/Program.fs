open System.IO

type Coord = int * int  
type State =
    { GuardPosition: Coord
      GuardVelocity: int * int
      MapSize: int * int
      Obstacles: Set<Coord>
      Visited: Set<Coord> }

let input = File.ReadLines "input.txt" |> array2D
    
let obstacles =
    input
    |> Array2D.mapi (fun y x char -> if char = '#' then Some(x,y) else None)
    |> Seq.cast<Coord option>
    |> Seq.choose id
    
let guardPosition =
    input
    |> Array2D.mapi (fun y x char -> if char = '^' then Some(x,y) else None)
    |> Seq.cast<Coord option>
    |> Seq.pick id

let state =
    { GuardPosition = guardPosition
      GuardVelocity = (0,-1) // Start heading north
      MapSize = (Array2D.length2 input, Array2D.length1 input)
      Obstacles = obstacles |> Set.ofSeq
      Visited = guardPosition |> Set.singleton }
    
let rec solve state =
    
    let isDone state =
        let x,y = state.GuardPosition
        let width,height = state.MapSize
        x < 0 || y < 0 || x >= width || y >= height
                    
    let moveGuard state =
        let x,y = state.GuardPosition
        let dx,dy = state.GuardVelocity
        let newPosition = (x+dx,y+dy)
        let newVisited = state.Visited |> Set.add newPosition
        { state with GuardPosition = newPosition; Visited = newVisited }
        
    let canMoveGuard state =
        let moved = moveGuard state
        state.Obstacles
        |> Set.contains moved.GuardPosition
        |> not

    let turnGuard state =
        let newVelocity =
            match state.GuardVelocity with
            | (1,0) -> (0,1)   // E -> S
            | (0,1) -> (-1,0)  // S -> W
            | (-1,0) -> (0,-1) // W -> N
            | _ -> (1,0)       // N -> E
        { state with GuardVelocity = newVelocity }
        
    match (isDone state, canMoveGuard state) with
    | (false,false) -> solve (state |> turnGuard) 
    | (false,true) -> solve (state |> moveGuard)
    | (true,_) -> state

state
|> solve
|> _.Visited
|> Set.count
|> printfn "%i"

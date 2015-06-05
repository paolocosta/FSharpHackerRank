// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv =
    FilterPositionsInList.FilterPositionsInList  // return an integer exit code
    |> List.iter (printfn "%d")

    ignore(System.Console.ReadLine())

    0
// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv =

    Utils.procinput []
    |> FilterPositionsInList.FilterPositionsInList
    |> List.iter (printfn "%d")

    ignore(System.Console.ReadLine())

    0
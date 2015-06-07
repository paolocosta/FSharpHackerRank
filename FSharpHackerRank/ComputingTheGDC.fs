module ComputingTheGDC

let extractList (s:string) =
    s.Split(' ')
    |>  Array.toList
    |> List.map System.Int32.Parse

let rec GDC (X:int) (Y:int) : int =
    if X = Y then X
    else if X > Y then (GDC (X - Y) Y)
    else (GDC (Y - X) X)

let main i1  =
    let values = extractList(i1)

    GDC (List.nth values 0) (List.nth values 1)   
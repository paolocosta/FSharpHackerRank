module ComputingTheGDC

let extractList (s:string) =
    s.Split(' ')
    |> Array.toList
    |> List.map System.Int32.Parse

let rec GDC X Y =
    if X = Y then X
    else if X > Y then (GDC (X - Y) Y)
    else (GDC (Y - X) X)
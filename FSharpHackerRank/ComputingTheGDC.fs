module ComputingTheGDC

let rec GDC X Y =
    if X = Y then X
    else if X > Y then (GDC (X - Y) Y)
    else (GDC (Y - X) X)
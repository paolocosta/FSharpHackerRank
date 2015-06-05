module EvaluatingEAtX

let fact n =
    [1..n]
    |> List.fold (fun acc elem -> acc * elem) 1

let raise v =
    let f (index,acc) elem =
        match index with
        | 0.0 -> (1.0,1.0)
        | n -> (n + 1.0, acc + (System.Math.Pow(v, index) / float((fact (int index)))))
    [0..9]
    |> List.fold f (0.0,0.0)
    |> snd

let evaluatingEAtX arr =
    List.map (fun x -> raise x) arr
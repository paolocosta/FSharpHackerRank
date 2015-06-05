module SumOfOddElements

let sumOfOddElements arr =
    arr
    |> List.filter (fun a -> System.Math.Abs( a % 2) = 1)
    |> List.sum
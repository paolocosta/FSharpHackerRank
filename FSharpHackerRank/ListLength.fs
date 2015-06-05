module ListLength

let listLength list =
    let elementSoFar = 0
    list
    |> List.fold (fun acc elem -> acc + 1) 0
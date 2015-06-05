module CreateNElementsArray

    let CreateNElementsArray n =
        match n with
        |0 -> [||]
        |n -> [| for i in 1..n -> i |]
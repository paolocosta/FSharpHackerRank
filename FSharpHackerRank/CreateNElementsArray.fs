module CreateNElementsArray

    let CreateNElementsArray n =
        [| for i in 1..n -> i |]
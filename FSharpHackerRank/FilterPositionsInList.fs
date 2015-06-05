module FilterPositionsInList

    let FilterPositionsInList values =

        let onlyOdd lines =
            let rec onlyOdd2 lines odd =
                match odd, lines with
                    | _, [] -> []
                    | true, l  ->  (List.head l) :: (onlyOdd2 (Utils.rest l) false)
                    | false, l -> onlyOdd2 (Utils.rest l) true

            onlyOdd2 lines true

        values
        |> onlyOdd
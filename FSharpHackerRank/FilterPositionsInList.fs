module FilterPositionsInList

    let FilterPositionsInList  =
        let rec procinput lines=
            match System.Console.ReadLine() |> System.Int32.TryParse with
            | false, _ -> List.rev lines
            | true, n -> procinput (n::lines)

        let values = procinput []

        let rest lst =
            match lst with
            | h::t -> t
            | _ -> []

        let onlyOdd lines =
            let rec onlyOdd2 lines odd =
                match odd, lines with
                    | _, [] -> []
                    | true, l  ->  (List.head l) :: (onlyOdd2 (rest l) false)
                    | false, l -> onlyOdd2 (rest l) true

            onlyOdd2 lines false

        values
        |> onlyOdd
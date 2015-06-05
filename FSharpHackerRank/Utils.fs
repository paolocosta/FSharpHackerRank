module Utils

    let rec procinput lines=
            match System.Console.ReadLine() |> System.Int32.TryParse with
            | false, _ -> List.rev lines
            | true, n -> procinput (n::lines)

    let rest lst =
            match lst with
            | h::t -> t
            | _ -> []
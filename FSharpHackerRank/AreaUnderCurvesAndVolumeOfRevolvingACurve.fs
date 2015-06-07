﻿module AreaUnderCurvesAndVolumeOfRevolvingACurve

let extractList (s:string) =
    s.Split(' ')
    |>  Array.toList
    |> List.map System.Int32.Parse
    |> List.map float

let tenRaised exponent = 10.0 ** float exponent

let GetPolinomValue multipliers exponents x =
    let calc mult exp =
        match mult with
        |0.0 -> 0.0
        |_ -> mult * (x ** exp)
    [0.. (List.length multipliers)-1]
    |> List.fold (
        fun acc elem ->
            acc +
                calc (float(List.nth multipliers elem)) (float(List.nth exponents elem))) 0.0

let fineList L R grane = [ for i in L * (tenRaised grane) + ( 1.0 / (tenRaised grane)) .. R * (tenRaised grane)  -> i / (tenRaised grane)]

let GetArea multipliers exponents L R precision =
    fineList L R precision
    |> List.fold (fun acc elem -> acc + (GetPolinomValue multipliers exponents elem)/ (10.0 ** float precision)) 0.0

let GetVolume multipliers exponents L R precision =
    fineList L R precision
    |> List.fold (fun acc elem -> acc + System.Math.PI * ((GetPolinomValue multipliers exponents elem) ** 2.0 / (10.0 ** float precision) )) 0.0

let main i1 i2 i3 =
    let multipliers = extractList(i1)
    let exponents = extractList (i2)
    let values = extractList (i3)
    let L = List.nth values 0
    let R = List.nth values 1

    GetArea multipliers exponents L R 3,GetVolume multipliers exponents L R 3
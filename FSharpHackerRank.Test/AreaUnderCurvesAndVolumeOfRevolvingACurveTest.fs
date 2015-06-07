module AreaUnderCurvesAndVolumeOfRevolvingACurveTest

open NUnit.Framework
open FsUnit

let isRelativeDifferenceMinus001 t =
    System.Math.Abs((float (fst t)) / (float (snd t))) > 0.99

[<Test>]
let ``should extract list work correctly``() =
    AreaUnderCurvesAndVolumeOfRevolvingACurve.extractList "1 2"  |> should equal [1.0;2.0]

[<Test>]
let ``should main work correctly with a complex text case``() =
        let result = (AreaUnderCurvesAndVolumeOfRevolvingACurve.main
                "-1 2 0 2 -1 -3 -4 -1 -3 -4 -999 1 2 3 4 5 1 2 0 2 -1 -3 -4 -1 -3 -4 -999 1 2 3 4 5"
                "-16 -15 -14 -13 -12 -11 -10 -9 -8 -7 -6 -5 -4 -3 -2 -1 0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15"
                "1 2")
        isRelativeDifferenceMinus001 (fst result, -152853.7) |> should equal true
        isRelativeDifferenceMinus001 (snd result, 196838966733.0) |> should equal true

[<Test>]
let ``should main work correctly with a simple text case``() =
        let result = AreaUnderCurvesAndVolumeOfRevolvingACurve.main "1 2 3 4 5" "6 7 8 9 10" "1 4"
        isRelativeDifferenceMinus001 (fst result, 2435300.3) |> should equal true
        isRelativeDifferenceMinus001 (snd result, 26172951168940.8) |> should equal true

[<Test>]
let ``should get value work correctly with a two elements list``() =
    AreaUnderCurvesAndVolumeOfRevolvingACurve.GetPolinomValue [0.0;1.0] [-1.0;2.0] 1.0 |> should equal 1.0

[<Test>]
let ``should get value work correctly with a three elements list``() =
    AreaUnderCurvesAndVolumeOfRevolvingACurve.GetPolinomValue [1.0;2.0;3.0] [1.0;2.0;3.0] 2.0 |> should equal 34.0
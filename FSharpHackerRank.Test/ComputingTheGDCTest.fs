module ComputingTheGDCTest

open NUnit.Framework
open FsUnit

[<Test>]
let ``should calculate GCD of 1 5 give 1``() =
    ComputingTheGDC.GDC 1 5  |> should equal 1

[<Test>]
let ``should calculate GCD of 10 100 give 10``() =
    ComputingTheGDC.GDC 10 100  |> should equal 10

[<Test>]
let ``should calculate GCD of 22 131 give 10``() =
    ComputingTheGDC.GDC 22 131  |> should equal 1
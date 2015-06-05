module SumOfOddElementsTest

open NUnit.Framework
open FsUnit

[<Test>]
let ``should empty list return 0``() =
    SumOfOddElements.sumOfOddElements [] |> should equal 0

[<Test>]
let ``should single element list return itsself``() =
    SumOfOddElements.sumOfOddElements [1] |> should equal 1

[<Test>]
let ``should work correctly on a multiple list``() =
    SumOfOddElements.sumOfOddElements [1;2;3;4;5] |> should equal 9

[<Test>]
let ``should work correctly with negative numbers``() =
    SumOfOddElements.sumOfOddElements [11;25;18;-1;26;-20;-19;23;-24;-8] |> should equal 39
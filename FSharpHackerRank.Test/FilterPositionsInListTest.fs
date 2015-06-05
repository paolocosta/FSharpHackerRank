module FilterPositionsInListTest

open NUnit.Framework
open FsUnit

[<Test>]
let ``should an empty string return an_empty string``() =
    FilterPositionsInList.FilterPositionsInList [] |> should equal  []

[<Test>]
let ``should a single element string return itself``() =
    FilterPositionsInList.FilterPositionsInList [1] |> should equal [1]

[<Test>]
let ``should work correctly with a long list``() =
    FilterPositionsInList.FilterPositionsInList [1;2;3;4] |> should equal [1;3]
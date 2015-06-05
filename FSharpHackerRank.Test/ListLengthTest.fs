module ListLengthTest

open NUnit.Framework
open FsUnit

[<Test>]
let ``should empty list return 0``() =
    ListLength.listLength [] |> should equal 0

[<Test>]
let ``should single list return 1``() =
    ListLength.listLength [1] |> should equal 1

[<Test>]
let ``should long list return the length``() =
    ListLength.listLength [1;1;1;1;1] |> should equal 5
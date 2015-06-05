module UpdateListTest

open NUnit.Framework
open FsUnit

[<Test>]
let ``should empty list return empty list``() =
    UpdateList.updateList [] |> should equal []

[<Test>]
let ``should work correctly``() =
    UpdateList.updateList [1;1;-10;4;2;1;-56] |> should equal [1;1;10;4;2;1;56]
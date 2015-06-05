module EvaluatingEAtX

open NUnit.Framework
open FsUnit

[<Test>]
let ``should empty list return empty list``() =
    UpdateList.updateList [] |> should equal []
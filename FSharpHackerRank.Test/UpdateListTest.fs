module UpdateListTest

open NUnit.Framework
open FsUnit

[<Test>]
let ``should raise 20 work crrectly``() =
    EvaluatingEAtX.raise 20.0 |> should equal 2423600.1887125224
module CreateNElementsArrayTest

open NUnit.Framework
open FsUnit

[<Test>]
let ``should 0 return an empty array``() =
    CreateNElementsArray.CreateNElementsArray 0 |> should equal  [||]

[<Test>]
let ``should 1 return a single element array``() =
    CreateNElementsArray.CreateNElementsArray 1
    |> Array.length
    |> should equal  1

[<TestCase(5)>]
[<TestCase(10)>]
let ``should work correctly with all values``(length) =
    CreateNElementsArray.CreateNElementsArray length
    |> Array.length
    |> should equal  length
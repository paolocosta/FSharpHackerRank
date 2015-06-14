module _99Problems

open NUnit.Framework
open FsUnit


let rec myLast l =
    match l with
    |[] -> None
    |[x] -> Some x
    |_ -> myLast (List.tail l)

let r = myLast [1;3;4;5]

[<Test>]
let ``should myLast work with a short list  ``() = 
    myLast [2;3;4;5] |> should equal (Some 5)

[<Test>]
let ``should myLast work with a single element list``() = 
    myLast [5] |> should equal (Some 5)




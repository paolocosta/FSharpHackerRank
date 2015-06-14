module FunTest

open NUnit.Framework
open FsUnit

type State = New | Draft | Published | Inactive | Discontinued

[<Test>]
let should_() = 
    let (|Int|_|) str =
       match System.Int32.TryParse(str) with
       | (true,int) -> Some(int)
       | _ -> None

    let (|Int64|_|) str =
       match System.Int64.TryParse(str) with
       | (true,int) -> Some(int)
       | _ -> None

    // create an active pattern
    let (|Bool|_|) str =
       match System.Boolean.TryParse(str) with
       | (true,bool) -> Some(bool)
       | _ -> None

    let testParse str = 
        match str with
        | Int i -> printfn "The value is an int '%i'" i
        | Int64 i -> printfn "The value is an int64 '%i'" i
        | Bool b -> printfn "The value is a bool '%b'" b
        | _ -> printfn "The value '%s' is something else" str

    testParse "1267567567567"
    testParse "true"
    testParse "abc"

    0 |> should equal 0
    
[<Test>]
let fizzBuzz() = 
    // setup the active patterns
    let (|MultOf3|_|) i = if i % 3 = 0 then Some 1 else None
    let (|MultOf5|_|) i = if i % 5 = 0 then Some 1 else None
    let (|MultOf5and3|_|) i = if i % 5 = 0 && i % 3 = 0 then Some 1 else None
    // the main function
    let fizzBuzz i = 
      match i with
      | MultOf5and3 l -> printf "FizzBuzz, " 
      | MultOf3 l -> printf "Fizz, " 
      | MultOf5 l -> printf "Buzz, " 
      | _ -> printf "%i, " i
           
    // test
    [1..20] |> List.iter fizzBuzz 

[<Test>]
let practice() =
    let rec movingAverages list = 
        match list with
        // if input is empty, return an empty list
        | [] -> []
        // otherwise process pairs of items from the input 
        | x::y::rest -> 
            let avg = (x+y)/2.0 
            //build the result by recursing the rest of the list
            avg :: movingAverages (y::rest)
        // for one item, return an empty list
        | [_] -> []
    //0

    movingAverages [1.0]
    |> List.iter (printfn "%f") 
    movingAverages [1.0; 2.0]
    |> List.iter (printfn "%f") 
    movingAverages [1.0; 2.0; 3.0]
    |> List.iter (printfn "%f") 
    0 |> should equal 0
    
    




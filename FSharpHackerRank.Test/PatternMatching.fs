module PatternMatching

open NUnit.Framework
open FsUnit

[<Measure>]
type cm

[<Measure>] 
type inches

[<Measure>] 
type feet =
   // add a conversion function
   static member toInches(feet : float<feet>) : float<inches> = 
      feet * 12.0<inches/feet>

type Result<'a, 'b> = 
    | Success of 'a  // 'a means generic type. The actual type
                     // will be determined when it is used.
    | Failure of 'b  // generic failure type as well
    

// define all possible errors
type FileErrorReason = 
    | FileNotFound of string
    | DirectoryNotFound of string
    | UnauthorizedAccess of string * System.Exception

[<Test>]
let unitOfMeasure() =
    // define some values
    let meter = 100.0<cm>
    let yard = 3.0<feet>
        
    //convert to different measure
    let yardInInches = feet.toInches(yard)

    0



[<Test>]
let myTest() = 
    let performActionOnFile action filePath =
       try
          //open file, do the action and return the result
          use sr = new System.IO.StreamReader(filePath:string)
          let result = action sr  //do the action to the reader
          sr.Close()
          Success (result)        // return a Success
       with      // catch some exceptions and convert them to errors
          | :? System.IO.FileNotFoundException as ex 
              -> Failure (FileNotFound filePath)      
          | :? System.Security.SecurityException as ex 
              -> Failure (UnauthorizedAccess (filePath,ex))
          | :? System.IO.DirectoryNotFoundException as ex 
              -> Failure (DirectoryNotFound filePath) 
          | :? System.ArgumentException as ex 
              -> Failure (UnauthorizedAccess (filePath,ex)) 

    let middleLayerDo action filePath = 
        let fileResult = performActionOnFile action filePath
        // do some stuff
        fileResult //return

    // a function in the top layer
    let topLayerDo action filePath = 
        let fileResult = middleLayerDo action filePath
        // do some stuff
        fileResult //return
              
    /// get the first line of the file
    let printFirstLineOfFile filePath = 
        let fileResult = topLayerDo (fun fs->fs.ReadLine()) filePath

        match fileResult with
        | Success result -> 
            // note type-safe string printing with %s
            printfn "first line is: '%s'" result   
        | Failure reason -> 
           match reason with  // must match EVERY reason
           | FileNotFound file -> 
               printfn "File not found: %s" file
           | DirectoryNotFound file -> 
               printfn "Dir not found: %s" file 
           | UnauthorizedAccess (file,_) -> 
               printfn "You do not have access to the file: %s" file


    printFirstLineOfFile "C:\Users\Paolo\Source\Repos\FSharpHackerRank\FSharpHackerRank.Test\PatternMatching.fs"

    0 |> should equal 0

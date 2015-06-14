module UrlDownload

open System.Net
open System
open System.IO
open NUnit.Framework
open FsUnit

let fetchUrl url = async{       
    let req = WebRequest.Create(Uri(url)) 
    use resp = req.GetResponse() 
    use stream = resp.GetResponseStream() 
    use reader = new IO.StreamReader(stream) 
    let html = reader.ReadToEnd() 
    printfn "finished downloading %s" url 
    }

let seriesDownload() = 
    // a list of sites to fetch
    let sites = ["http://www.bing.com";
                 "http://www.google.com";
                 "http://www.microsoft.com";
                 "http://www.amazon.com";
                 "http://www.yahoo.com"]

    sites                     // start with the list of sites
    |> List.map fetchUrl      // loop through each site and download
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

[<Test>]
let parallelComputation() =
    let childTask() = 
    // chew up some CPU. 
        for i in [1..1000] do 
            for i in [1..1000] do 
                do "Hello".Contains("H") |> ignore 
    
    let asyncChildTask = async { return childTask() }
        
    let asyncParentTask = 
        asyncChildTask
        |> List.replicate 20
        |> Async.Parallel

    asyncParentTask 
    |> Async.RunSynchronously |> ignore
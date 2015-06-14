module Async

open NUnit.Framework
open FsUnit
open System
open System.Threading

let userTimerWithCallback() = 
    // create a event to wait on
    let event = new System.Threading.AutoResetEvent(false)

    let stopEvent (event:System.Threading.AutoResetEvent) =
        event.Set() |> ignore
        printfn "Event set %O" DateTime.Now.TimeOfDay
    
    // create a timer and add an event handler that will signal the event
    let timer = new System.Timers.Timer(1000.0)
    timer.Elapsed.Add (fun _ -> stopEvent event |> ignore )

    //start
    printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
    timer.Start()

    System.Threading.Thread.Sleep(4000);

    // keep working
    printfn "Doing something useful while waiting for event"

    // block on the timer via the AutoResetEvent
    event.WaitOne() |> ignore

    //done
    printfn "Timer ticked at %O" DateTime.Now.TimeOfDay


let userTimerWithAsync() = 

    // create a timer and associated async event
    let timer = new System.Timers.Timer(2000.0)
    let timerEvent = Async.AwaitEvent (timer.Elapsed) |> Async.Ignore

    // start
    printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
    timer.Start()

    // keep working
    printfn "Doing something useful while waiting for event"

    // block on the timer event now by waiting for the async to complete
    Async.RunSynchronously timerEvent

    // done
    printfn "Timer ticked at %O" DateTime.Now.TimeOfDay

let fileWriteWithAsync() = 

    // create a stream to write to
    use stream = new System.IO.FileStream("test.txt",System.IO.FileMode.Create)

    // start
    printfn "Starting async write"
    
    
    let toByteArray (s:string) = 
        let byteAt (s:string) i = Convert.ToByte(s.Chars(i))
        [0..s.Length - 1] 
        |> List.map (byteAt s)
        |> List.toArray
    
    let b = toByteArray "234234234243243234234234"

    let asyncResult = stream.BeginWrite(toByteArray("2342342342432"),0,"2342342342432".Length,null,null)
    
    // create an async wrapper around an IAsyncResult
    let async = Async.AwaitIAsyncResult(asyncResult) |> Async.Ignore

    // keep working
    printfn "Doing something useful while waiting for write to complete"

    // block on the timer now by waiting for the async to complete
    Async.RunSynchronously async 

    // done
    printfn "Async write completed"

let asyncWorkflow() =
    let sleepWorkflow  = async{
        printfn "Starting sleep workflow at %O" DateTime.Now.TimeOfDay
        do! Async.Sleep 2000
        printfn "Finished sleep workflow at %O" DateTime.Now.TimeOfDay
        }

    Async.RunSynchronously sleepWorkflow  

let cancellationAsyncWorkflow() =
    let testLoop = async {
        for i in [1..100] do
            // do something
            printf "%i before.." i
        
            // sleep a bit 
            do! Async.Sleep 100  
            printfn "..after"
        }
    
    let cancellationSource = new CancellationTokenSource()

    // start the task, but this time pass in a cancellation token
    Async.Start (testLoop,cancellationSource.Token)

    // wait a bit
    Thread.Sleep(200)  

    // cancel after 200ms
    cancellationSource.Cancel()

// create a workflow to sleep for a time
let series() =
    let sleepWorkflowMs ms = async {
        printfn "%i ms workflow started" ms
        do! Async.Sleep ms
        printfn "%i ms workflow finished" ms
        }

    let workflowInSeries = async {
        let! sleep1 = sleepWorkflowMs 1000
        printfn "Finished one" 
        let! sleep2 = sleepWorkflowMs 2000
        printfn "Finished two" 
    }

    Async.RunSynchronously workflowInSeries 

[<Test>]
let inParallel() =
    let sleepWorkflowMs ms = async {
        printfn "%i ms workflow started" ms
        do! Async.Sleep ms
        printfn "%i ms workflow finished" ms
        }

    // Create them
    let sleep1 = sleepWorkflowMs 1000
    let sleep2 = sleepWorkflowMs 2000

    // run them in parallel
    //#time
    [sleep1; sleep2] 
        |> Async.Parallel
        |> Async.RunSynchronously 
        |> ignore
    //#time




    





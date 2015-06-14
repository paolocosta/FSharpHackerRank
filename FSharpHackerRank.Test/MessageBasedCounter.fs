module MessageBasedCounter

open System.Net
open System
open System.IO
open NUnit.Framework
open FsUnit
open System.Threading
open System.Diagnostics

// a utility function
type Utility() = 
    static let rand = new Random()
    
    static member RandomSleep() = 
        let ms = rand.Next(1,3000)
        printfn "Random sleep for %d ms" ms
        Thread.Sleep ms

type MessageBasedCounter () = 

    static let updateState (count,sum) msg = 

        // increment the counters and...
        let newSum = sum + msg
        let newCount = count + 1
        printfn "Count is: %i. Sum is: %i" newCount newSum 

        // ...emulate a short delay
        Utility.RandomSleep()

        // return the new state
        (newCount,newSum)

    // create the agent
    static let agent = MailboxProcessor.Start(fun inbox -> 
        // the message processing function
        let rec messageLoop oldState = async{

            // read a message
            let! msg = inbox.Receive()

            // do the core logic
            let newState = updateState oldState msg

            // loop to top
            return! messageLoop newState 
            }

        // start the loop 
        messageLoop (0,0)
        )

    static member Add i = agent.Post i

let makeCountingTask addFunction taskId  = async {
    let name = sprintf "Task%i" taskId
    for i in [1..3] do 
        addFunction i
    }

[<Test>]
let MessageTest() =
    let task = makeCountingTask MessageBasedCounter.Add 1
    Async.RunSynchronously task


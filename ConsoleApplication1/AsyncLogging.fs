module AsyncLogging

let slowConsoleWrite msg = 
    msg |> String.iter (fun ch->
        System.Console.Write ch
        )
    System.Console.WriteLine("")

let makeTask logger taskId = async {
    let name = sprintf "Task%i" taskId
    for i in [1..3] do 
        let msg = sprintf "-%s:Loop%i-" name i
        logger msg 
    }

type UnserializedLogger() = 
    // interface
    member this.Log msg = slowConsoleWrite msg
// test in isolation

type SerializedLogger() = 

    // create the mailbox processor
    let agent = MailboxProcessor.Start(fun inbox -> 

        // the message processing function
        let rec messageLoop w = async{

            // read a message
            let! msg = inbox.Receive()

            // write it to the log
            slowConsoleWrite msg

            // loop to top
            return! messageLoop ()
            }

        // start the loop
        messageLoop ()
        )

    // public interface
    member this.Log msg = agent.Post msg

let main argv = 
//    let unserializedExample = 
//        let logger = new UnserializedLogger()
//        [1..5]
//            |> List.map (fun i -> makeTask logger.Log i)
//            |> Async.Parallel
//            |> Async.RunSynchronously
//            |> ignore
//    let serializedLogger = SerializedLogger()
//    serializedLogger.Log "hello"

    let serializedExample = 
        let logger = new SerializedLogger()
        [1..50000]
            |> List.map (fun i -> makeTask logger.Log i)
            |> Async.Parallel
            |> Async.RunSynchronously
            |> ignore

        
    System.Console.WriteLine(true) |> ignore
    System.Console.ReadLine() |> ignore
    0
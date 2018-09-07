#load "./packages/FSharp.Formatting/FSharp.Formatting.fsx"
#load "./posts.fsx"

open Posts.Operations
open System.IO

let args = System.Environment.GetCommandLineArgs() |> List.ofArray
let sourcePath = Path.Combine(__SOURCE_DIRECTORY__ + "/src")
let files = Directory.GetFiles(sourcePath)

match args with
| [_;_;_;command] -> 
    match command.ToLower() with
    | "build" -> writePosts files
    | _ -> failwith "Not a valid command"
| _ -> failwith "Cannot process your command"

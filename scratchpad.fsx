#r "./packages/HtmlAgilityPack/lib/netstandard2.0/HtmlAgilityPack.dll"
#r "./packages/Fue/lib/netstandard2.0/Fue.dll"


open Fue.Data
open Fue.Compiler
open System
open System.IO

let args = Environment.GetCommandLineArgs()

let processCommand = 
    match args with
    | [|_;_;_;command|] -> printfn "The command is %s" command
    | _ -> failwith "Could not process command"

processCommand



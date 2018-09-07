#load "./packages/FSharp.Formatting/FSharp.Formatting.fsx"
#r "./packages/HtmlAgilityPack/lib/netstandard2.0/HtmlAgilityPack.dll"
#r "./packages/Fue/lib/netstandard2.0/Fue.dll"

namespace Posts

    open System
    open FSharp.Markdown
    open System.Text.RegularExpressions

    type Blog = {Title:string;PostDate:DateTime;Content:MarkdownDocument}

    module Operations =

        open System
        open System.IO
        open System.Text.RegularExpressions
        open Fue.Data
        open Fue.Compiler

        let getTitle (line:MarkdownParagraph) =
            match line with
            | Heading (1,heading) -> 
                match heading with
                | [Literal title] -> title
                | _ -> failwith "Title"
            | _ -> failwith "No Heading"
        
        let getPostedDate (line: MarkdownParagraph) = 
            match line with
            | Heading (6,heading) ->
                match heading with
                | [Literal date] -> DateTime.Parse date
                | _ -> failwith "No Date"
            | _ -> failwith "No Date"

        let getIndex (myString:string) = 
            match myString.Length with
            | x when x < 45 -> myString.Length
            | _ -> 45


        let getSlug (title:string) = 
            let lowercase = title.ToLower()
            let invalidChars = Regex.Replace(lowercase,@"[^a-z0-9\s-]", "")
            let removeMultipleSpaces = Regex.Replace(invalidChars,@"\s+", " ").Trim()
            let trimmed = removeMultipleSpaces.Substring(0, (getIndex removeMultipleSpaces)).Trim();
            let hyphenate = Regex.Replace(trimmed, @"\s", "-");
            hyphenate

        let createPost (content:MarkdownDocument) = 
            let post = 
                match (content.Paragraphs:MarkdownParagraphs) with
                | title::date::postcontent ->
                    { Title=(getTitle title)
                      PostDate=(getPostedDate date)  
                      Content=  new MarkdownDocument(postcontent,content.DefinedLinks)}  
                | _ -> failwith "Post not created"
            post    

        let processFile = File.ReadAllText >> Markdown.Parse >> createPost  

        let writePosts files = 
            files
            |> Array.map(fun file -> sprintf "%s" file)
            |> Array.map processFile
            |> List.ofArray
            |> List.map(fun post -> post.Title,post.PostDate,(Markdown.WriteHtml post.Content))
            |> List.map(fun (title,date,content) -> 
                let filename = sprintf "%s.html" (getSlug title)
             
                let template = __SOURCE_DIRECTORY__ + "/templates/post.html"
             
                let compiledHTML = 
                    init
                    |> add "title" title
                    |> add "date" (date.ToShortDateString())
                    |> add "content" content
                    |> fromFile template

                File.WriteAllText((sprintf "./public/%s" filename),compiledHTML,System.Text.Encoding.UTF8)
            )
            

            
            
            
        
            

           

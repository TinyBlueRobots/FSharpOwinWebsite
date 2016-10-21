#load "References.fsx"

open System.IO
open HandlebarsDotNet

type Template = Home | Hello | Error | NotFound

let compileTemplate currentDirectory =
  let master = sprintf "%s/templates/master.html" currentDirectory |> File.ReadAllText
  let compiledTemplates =
    [ for template in Reflection.FSharpType.GetUnionCases typeof<Template> do
      use sr = new StringReader(template.Name.ToLower() |> sprintf "%s/templates/%s.html" currentDirectory |> File.ReadAllText)
      Handlebars.RegisterTemplate(template.Name, Handlebars.Compile sr)
      let compile = master.Replace("%template%", template.Name) |> Handlebars.Compile
      yield Reflection.FSharpValue.MakeUnion(template, [||]) |> unbox, compile.Invoke ]
    |> Map.ofList
  fun (template : Template) -> Map.find template compiledTemplates
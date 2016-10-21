#load "References.fsx"

open Templates

type HelloModel =
  { ViewBag: Map<string,string>
    Name: string }

let render name write =
  { HelloModel.ViewBag =
      [ "Title", "Hello"; "Description", "The Hello World Page" ]
      |> Map.ofList
    Name = name }
  |> write Hello
#load "References.fsx"

open Templates

type HomeModel = { ViewBag: Map<string,string> }

let render write =
  { HomeModel.ViewBag = [ "Title", "Hurray"; "Description", "Boom" ] |> Map.ofList } |> write Home
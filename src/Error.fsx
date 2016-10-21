#load "References.fsx"

open Templates

type ErrorModel = { ViewBag: Map<string,string> }

let render write =
  { ErrorModel.ViewBag = [ "Title", "Fail"; "Description", "Fail" ] |> Map.ofList } |> write Error
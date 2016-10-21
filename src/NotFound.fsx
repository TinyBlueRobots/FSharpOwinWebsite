#load "References.fsx"

open Templates

type NotFoundModel = { ViewBag: Map<string,string> }

let render write =
  { NotFoundModel.ViewBag = [ "Title", "Not found"; "Description", "Page not found" ] |> Map.ofList } |> write NotFound
#load "References.fsx"

open Owin
open Route
open OwinContext
open System.IO

let (?) (map: Map<string, string>) index = map |> Map.find index

type RouteNames =
  | Img
  | Content
  | Home
  | Hello

let routes =
  [ Img, GET, "/img/{_}"
    Content, GET, "/content/{file}"
    Home, GET, "/"
    Hello, GET, "/hello/{name}" ]
  |> List.map (fun (name, httpMethod, route) -> name, matchRoute httpMethod route)

let currentDirectory =
  #if INTERACTIVE
  __SOURCE_DIRECTORY__
  #else
  System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase
  #endif

let compileTemplate = Templates.compileTemplate currentDirectory
let writeString ctx template (model : obj) = compileTemplate template model |> writeString ctx

let route ctx =
  let writeFile = writeFile ctx
  let route =
    routes
    |> Seq.map(fun (name, value) -> name, value ctx)
    |> Seq.tryFind (fun (_, parameters) -> parameters |> Option.isSome)
  match route with
  | Some(name, Some parameters) ->
    match name with
    | Img ->
      sprintf "%s%s" currentDirectory ctx.Request.Path.Value
      |> writeFile
    | Content ->
      match parameters?file with
      | file when file.EndsWith ".css" ->
        ctx.Response.ContentType <- "text/css"
      | _ -> ()
      sprintf "%s%s" currentDirectory ctx.Request.Path.Value
      |> writeFile
    | Home -> writeString ctx |> Home.render
    | Hello -> writeString ctx |> Hello.render parameters?name
  | _ ->
    ctx.Response.StatusCode <- 404
    writeString ctx |> NotFound.render

type Startup() =
  member __.Configuration(app: IAppBuilder) =
    app.Use(fun ctx next ->
      try
        next.Invoke()
      with ex ->
        ctx.Response.StatusCode <- 500
        createContext ctx |> writeString |> Error.render)
      |> ignore
    app.Run(fun ctx -> createContext ctx |> route)
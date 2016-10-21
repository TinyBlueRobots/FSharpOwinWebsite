#load "References.fsx"

open Microsoft.Owin
open System

[<NoComparison ; NoEquality>]
type OwinContext =
  { Authentication : Security.IAuthenticationManager
    Environment : Collections.Generic.IDictionary<string, obj>
    Get : string -> obj
    Request : IOwinRequest
    Response : IOwinResponse
    Set : string * obj -> IOwinContext
    TraceOutput : IO.TextWriter }

let createContext (ctx : IOwinContext) =
  { Authentication = ctx.Authentication
    Environment = ctx.Environment
    Get = ctx.Get
    Request = ctx.Request
    Response = ctx.Response
    Set = ctx.Set
    TraceOutput = ctx.TraceOutput }

let writeString ctx (text : string) = ctx.Response.WriteAsync text

let writeFile ctx fileName =
  System.IO.File.ReadAllBytes fileName |> ctx.Response.WriteAsync
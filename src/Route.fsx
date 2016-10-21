#load "References.fsx"

open System.Text.RegularExpressions
open Microsoft.Owin
open OwinContext

type HttpMethod =
  | DELETE
  | GET
  | HEAD
  | METHOD
  | OPTIONS
  | POST
  | PUT
  | TRACE

let matchRoute (httpMethod: HttpMethod) (route: string) =
  let pattern = route.Replace("{", "(?<").Replace("}", ">.+)") |> sprintf "^%s$"
  let regex = new Regex(pattern, RegexOptions.IgnoreCase)
  fun ctx ->
    match ctx.Request.Method = sprintf "%A" httpMethod with
    | false -> None
    | true ->
      let mtch = regex.Match ctx.Request.Path.Value
      match mtch.Success with
      | true ->
          let groups = mtch.Groups
          regex.GetGroupNames()
          |> Array.skip 1
          |> Array.map (fun x -> x, groups.[x].Value)
          |> Array.filter (fun (_, x) -> x.Length > 0)
          |> Map.ofArray
          |> Some
      | _ -> None
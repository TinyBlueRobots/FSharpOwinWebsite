#load "References.fsx"

open Microsoft.Owin.Hosting

let webApp = WebApp.Start<Main.Startup> "http://localhost:8080"
stdin.ReadLine() |> ignore
#load "References.fsx"

open Microsoft.Owin

[<assembly: OwinStartup(typeof<Main.Startup>)>]
do()
module Main
open Fern
open MandelbrotSet
open System.Drawing
open System.Windows.Forms

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    do Application.Run(drawFern())
    0

 (*
 [<EntryPoint>]
let main argv =  
    printfn "%A" argv
    renderMandel()
    0
   *)
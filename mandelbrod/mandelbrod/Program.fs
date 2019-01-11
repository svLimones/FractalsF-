module Main
open Fern
open MandelbrotSet
open MandelbrotSet2
open TriangleSept
open System.Drawing
open System.Windows.Forms

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    do Application.Run(drawFern())
    //renderMandel()
    //do Application.Run(createChaosImage(400, 10000))
    //do Application.Run(createMandelbrot(0.5, 5.0, 5.0, 10000))
    0

module MandelbrotSet2
open System
open System.Drawing
open System.Windows.Forms

let rec rpt n f=
    if(n=0) then fun x -> x
    else f >> (rpt (n-1) f)

let mandelf (c: complex) (z:complex) = z*z+c
let scale (x: float, y: float) (u, v) n = float(n-u)/float(v-u)*(y-x)+x
let ismandel c = Complex.Abs(rpt 20 (mandelf c) (Complex.Zero))<1.

let renderMandel() =
    for i=0 to 60 do
        for j=0 to 60 do
            let lscale = scale (-1.2, 1.2) (1, 60)
            let t = complex (lscale j) (lscale i)
            Console.Write (if ismandel t then "*" else " ")
        Console.Write("");

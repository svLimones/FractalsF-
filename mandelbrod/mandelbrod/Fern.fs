module Fern

open Microsoft.FSharp.Math
open System
open System.Drawing
open System.Windows.Forms

let probability = [0.01; 0.06; 0.08; 0.85]
let k = [|[| 0.0;  0.0;  0.0;  0.16; 0.0; 0.0|]
          [|-0.15; 0.28; 0.26; 0.24; 0.0; 0.44|]
          [| 0.2; -0.26; 0.23; 0.22; 0.0; 1.6|]
          [| 0.85; 0.04;-0.04; 0.85; 0.0; 1.6|]|]
let random = Random()

let getRandFunc () = 
    let r = random.NextDouble()
    let rec getRandRec i sum prob =
        if sum+(List.head prob)>r then i
        else getRandRec (i+1) (sum+(List.head prob)) (prob.Tail)
    let funcIndex = getRandRec 0 0. probability
    k.[funcIndex]

let fern (x, y) = 
    let f = getRandFunc()
    let _x = f.[0]*x + f.[1]*y + f.[4]
    let _y = f.[2]*x + f.[3]*y + f.[5]
    (_x,_y)
    
let colorize c =
    let r = (40 * c) % 255
    let g = (60 * c) % 255
    let b = (80 * c) % 255
    Color.FromArgb(r,g,b)

let drawFern () =
    let minX = -6.0
    let maxX = 6.0
    let minY = 0.1
    let maxY = 14.0
    let imageWidth = 800.0
    let imageHeight = 800.0
    let width = imageWidth/(maxX-minX)
    let height = imageHeight/(maxY-minY)
    let image = new Bitmap(imageWidth|>int, imageHeight|>int)
    let rec drawRec xy i col= 
        let (x,y) = fern xy
        let px = x*width + imageWidth*0.5 |> int
        let py = y*height |> int
        if px<(imageWidth|>int) && py<(imageHeight|>int) 
            then image.SetPixel(px, py, col)
        if i>0 then drawRec (x,y) (i-1) col
    
    let rec multiDrawRec i = 
        if i>0 then 
            multiDrawRec (i-1)
            drawRec (0.,0.) 7000 (colorize i)
    multiDrawRec 40
    
    let temp = new Form() in
    temp.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
    temp

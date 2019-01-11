module TriangleSept

open System
open System.Drawing
open System.Windows.Forms

let createChaosImage (size, iterCount) =
    let ax = size/2 
    let ay = 0
    let bx = 0
    let by = size
    let cx = size
    let cy = size
    let random = Random()
    let image = new Bitmap(size, size)
    let rx = random.Next(size)
    let ry = random.Next(size)    
    let inline lerp x y t = x + (y - x) * t

    let getAttractor () = 
        let r = random.Next(3)
        match r with 
            | 0 -> (ax, ay, Color.Orange)
            | 1 -> (bx, by, Color.Red)
            | _ -> (cx, cy, Color.Blue)
        
    let rec draw x' y' iterCount = 
        let (atx,aty,col) = getAttractor()
        let x = lerp (x'|>float) (atx|>float) 0.5 |> int
        let y = lerp (y'|>float) (aty|>float) 0.5 |> int
        image.SetPixel(x,y,col)
        if iterCount>0 then draw x y (iterCount-1)

    draw rx ry iterCount |> ignore
    let temp = new Form() in
    temp.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
    temp
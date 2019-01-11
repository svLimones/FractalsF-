module MandelbrotSet

open System.Drawing
open System.Windows.Forms


let cMax = complex 1.0 1.0
let cMin = complex -1.0 -1.0

let rec isInMandelbrotSet (z, c, iter, count) =
    if (cMin < z) && (z < cMax) && (count < iter) then
        isInMandelbrotSet ( ((z * z) + c), c, iter, (count + 1) )
    else count

let scalingFactor s = s * 1.0 / 200.0
let offsetX = -1.0
let offsetY = -1.0

let mapPlane (x, y, s, mx, my) =
    let fx = ((float x) * scalingFactor s) + mx
    let fy = ((float y) * scalingFactor s) + my
    complex fx fy

let colorize c =
    let r = (4 * c) % 255
    let g = (6 * c) % 255
    let b = (8 * c) % 255
    Color.FromArgb(r,g,b)

let createMandelbrot (scale, mx, my, iter) =
    let image = new Bitmap(400, 400)
    for x = 0 to image.Width - 1 do
        for y = 0 to image.Height - 1 do
            let count = isInMandelbrotSet( Complex.Zero, (mapPlane (x, y, scale, mx, my)), iter, 0)
            if count = iter then
                image.SetPixel(x,y, Color.Black)
            else
                image.SetPixel(x,y, colorize( count ) )
    let temp = new Form() in
    temp.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
    temp
#r "FSharp.PowerPack.dll"
open Microsoft.FSharp.Math;;
open System
open System.Drawing
open System.Windows.Forms
open System.Runtime.Remoting.Metadata.W3cXsd2001


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

let createImage (s, mx, my, iter) =
    let image = new Bitmap(400, 400)
    for x = 0 to image.Width - 1 do
        for y = 0 to image.Height - 1 do
            let count = isInMandelbrotSet( Complex.Zero, (mapPlane (x, y, s, mx, my)), iter, 0)
            if count = iter then
                image.SetPixel(x,y, Color.Black)
            else
                image.SetPixel(x,y, colorize( count ) )
    let temp = new Form() in
    temp.Paint.Add(fun e -> e.Graphics.DrawImage(image, 0, 0))
    temp

#time "on";;
let rec fact (n:bigint) =
    if n<2I then 1I
    else n*fact(n-1I)
printfn "odd sum=%A" (fact 542I)
#time "off";;

let gameGuess max =
    let mutable failsCount = 0
    let random = System.Random()
    let target = random.Next(0, max)
    
    let game = fun guess ->
                   if guess = target then
                      System.Console.WriteLine("You win! Fails Count = " + failsCount.ToString())
                   else
                      failsCount <- failsCount + 1
                      System.Console.WriteLine("Wrong. Try again.  Fails Count = " + failsCount.ToString())
                      
    game

let playGame = gameGuess 7
playGame 3

let timeOfDay = DateTime.Now.ToString("hh:mm tt")



let mutable sum = 0
for i = 0 to 10000 do
    if i%2 <> 0 then sum <- sum + i
printfn "odd sum=%A" sum


let l = [0..100]
let ``odd summ`` l = 
    let oddList = List.filter(fun a->a%2<>0) l
    List.length oddList

printfn "odd sum=%A" (``odd summ`` l)


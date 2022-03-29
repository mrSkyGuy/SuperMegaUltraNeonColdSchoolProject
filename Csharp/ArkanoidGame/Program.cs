using System;
using System.Linq;
using System.Diagnostics;

// cd C:\Users\nurma\myLife-xD\SuperMegaUltraNeonColdSchoolProject\Csharp\ArkanoidGame\ && dotnet run
struct ConsoleSettings {
    static public int height = 40,
                      width = 110, 
                      fps = 10;
}


class Program {
    static void Main(string[] args) {
        Console.CursorVisible = false;
        Console.Clear();
        Console.SetWindowSize(ConsoleSettings.width, ConsoleSettings.height);
        
        bool run = true;
        Ball ball = new Ball();
        while (run) {
            ball.move();
            System.Threading.Thread.Sleep(1000 / ConsoleSettings.fps);
            if (!ball.isAlive) run = false;
        }
    }
}

 
class Ball {
    public int speedX { get; set; } 
    public int speedY { get; set; }
    public int coordX { get; set; }
    public int coordY { get; set; }
    public bool isAlive;
 
    private char symb;
    private ConsoleColor symbColor;
 
    public Ball(int coordX, int coordY) {
        this.coordX = coordX;
        this.coordY = coordY;
 
        this.speedX = 1;
        this.speedY = 1;
 
        this.symb = 'o';
        this.symbColor = ConsoleColor.Red;
        this.isAlive = true;
    } public Ball(): this(ConsoleSettings.width / 2, ConsoleSettings.height / 2) {}

    public void move() {
        hide();
        coordX += speedX;
        coordY += speedY;
        show();

        if ((coordX >= ConsoleSettings.width - 1) || (coordX <= 2)) speedX *= -1;
        if (coordY >= ConsoleSettings.height) { remove(); hide(); }
        if (coordY <= 2) speedY *= -1;
    }
 
    public void show() {
        Console.SetCursorPosition(coordX, coordY);
        Console.ForegroundColor = symbColor;
        Console.Write(symb);
        Console.ResetColor();
    }

    public void hide() {
        Console.SetCursorPosition(coordX, coordY);
        Console.Write(' ');
    }

    public void remove() {
        isAlive = false;
        symb = ' ';
    }
}

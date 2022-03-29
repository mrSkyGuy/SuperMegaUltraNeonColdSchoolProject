using System;
using System.Linq;
using System.Diagnostics;

// cd C:\Users\nurma\myLife-xD\SuperMegaUltraNeonColdSchoolProject\Csharp\ArkanoidGame\ && dotnet run
struct ConsoleSettings {
    static public int height = 35,
                      width = 70, 
                      fps = 10;
}


class Program {
    static void Main(string[] args) {
        Console.CursorVisible = false;
        Console.Clear();
        Console.SetWindowSize(ConsoleSettings.width, ConsoleSettings.height);
        
        bool run = true;
        Ball ball = new Ball();
        Bar bar = new Bar();
        while (run) {
            if (Console.KeyAvailable) switch (Console.ReadKey().Key) {
                case ConsoleKey.LeftArrow:
                    bar.moveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    bar.moveRight();
                    break;
            }
            ball.move();
            ball.checkColideWithBar(bar);
            bar.show();
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
        if (coordY >= ConsoleSettings.height - 1) { remove(); hide(); }
        if (coordY <= 0) speedY *= -1;
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

    public void checkColideWithBar(Bar bar) {
        if (bar.coordsX.Contains(this.coordX) && bar.coordY == this.coordY) speedY *= -1;
    }
}


class Bar {
    public int width { get; set; } 
    public int coordX { get; set; } 
    public int[] coordsX { get; set; }
    public int coordY { get; set; } 
    public int speedX { get; set; } 

    private char symb = '=';

    public Bar() {
        width = 10;
        coordX = ConsoleSettings.width / 2 - width / 2;
        coordY = ConsoleSettings.height - 2;
        speedX = 2;

        updateCoordsX();
    }

    public void show() {
        Console.SetCursorPosition(coordX, coordY);
        Console.Write(":");
        for (int i = 0; i < width - 2; i++) Console.Write(symb);
        Console.Write(":");
    }

    public void hide() {
        Console.SetCursorPosition(coordX, coordY);
        for (int i = 0; i < width; i++) Console.Write(' ');
    }

    public void moveLeft() {
        hide();
        coordX -= speedX;
        if (coordX <= 1) coordX = 2; 
        updateCoordsX();
    }

    public void moveRight() {
        hide();
        coordX += speedX;
        if (coordX >= ConsoleSettings.width - width - 1) coordX = ConsoleSettings.width - width - 2;
        updateCoordsX();
    }

    private void updateCoordsX() {
        coordsX = new int[width];
        for (int i = 0; i < width; i++) coordsX[i] = coordX + i;
    } 
}

using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

// cd C:\Users\nurma\myLife-xD\SuperMegaUltraNeonColdSchoolProject\Csharp\ArkanoidGame\ && dotnet run
struct GameSettings {
    static public int consoleHeight = 35,
                      consoleWidth = 70, 
                      fps = 10,
                      bricksCount = 16 * 10,
                      brickWidth = 3,
                      barWidth = 10;
}


class Program {
    static void Main(string[] args) {
        Console.CursorVisible = false;
        Console.Clear();
        Console.SetWindowSize(GameSettings.consoleWidth, GameSettings.consoleHeight);

        bool run = true;
        Ball ball = new Ball();
        Bar bar = new Bar();
        List<Brick> bricks = new List<Brick>() {};
        generateBricks(ref bricks);
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
            foreach (Brick brick in bricks) { 
                brick.show();
                brick.checkCollideWithBall(ball);
            } 
            System.Threading.Thread.Sleep(1000 / GameSettings.fps);
            if (!ball.isAlive) run = false;
        }
    }

    static private void generateBricks(ref List<Brick> bricks) {
        int x = 2, y = 2;
        for (int i = 0; i < GameSettings.bricksCount; i++) {
            bricks.Add(new Brick(x, y));
            x += GameSettings.brickWidth + 1;
            if (x >= GameSettings.consoleWidth - GameSettings.brickWidth - 1) {
                x = 2;
                y += 2;
            }
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
    } public Ball(): this(GameSettings.consoleWidth / 2, GameSettings.consoleHeight / 2) {}

    public void move() {
        hide();
        coordX += speedX;
        coordY += speedY;
        show();

        if ((coordX >= GameSettings.consoleWidth - 1) || (coordX <= 1)) speedX *= -1;
        if (coordY >= GameSettings.consoleHeight - 1) { remove(); hide(); }
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
        if (bar.coordsX.Contains(this.coordX) && bar.coordY - 1 == this.coordY) speedY *= -1;
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
        width = GameSettings.barWidth;
        coordX = GameSettings.consoleWidth / 2 - width / 2;
        coordY = GameSettings.consoleHeight - 2;
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
        if (coordX >= GameSettings.consoleWidth - width - 1) coordX = GameSettings.consoleWidth - width - 2;
        updateCoordsX();
    }

    private void updateCoordsX() {
        coordsX = new int[width];
        for (int i = 0; i < width; i++) coordsX[i] = coordX + i;
    } 
}


class Brick {
    public int width;
    public int coordX { get; set; }
    public int[] coordsX { get; set; }
    public int coordY { get; set; } 
    public ConsoleColor color;

    private char symb;
    private bool isAlive;

    public Brick(int x, int y, ConsoleColor color = ConsoleColor.Blue) {
        coordX = x;
        coordY = y;
        this.color = color;
        width = GameSettings.brickWidth;

        symb = '■';
        isAlive = true;

        updateCoordsX();
    }

    public void show() {
        if (!isAlive) return;

        Console.ForegroundColor = color;
        Console.SetCursorPosition(coordX, coordY);
        for (int i = 0; i < width; i++) Console.Write(symb);
        Console.ResetColor();
    }

    public void hide() {
        Console.SetCursorPosition(coordX, coordY);
        for (int i = 0; i < width; i++) Console.Write(' ');
    }

    public void remove() {
        hide();
        isAlive = false;
        symb = ' ';
    }

    public void checkCollideWithBall(Ball ball) {
        if (  // Снизу и сверху
            (coordsX.Contains(ball.coordX) && coordY - 1 == ball.coordY) ||
            (coordsX.Contains(ball.coordX) && coordY + 1 == ball.coordY)
        ) {
            remove();
            ball.speedY *= -1;
        }

        if (  // Слева и справа
            (coordsX.Contains(ball.coordX + 1) && coordY == ball.coordY) ||
            (coordsX.Contains(ball.coordX - 1) && coordY == ball.coordY)
        ) {
            remove();
            ball.speedX *= -1;
        }
    }

    private void updateCoordsX() {
        coordsX = new int[width];
        for (int i = 0; i < width; i++) coordsX[i] = coordX + i;
    } 
}

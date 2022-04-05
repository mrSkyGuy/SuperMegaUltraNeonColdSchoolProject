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
    static public string mapName;
}
 
 
class Program {
    static public void Main(string[] args) {
        Console.CursorVisible = false;
 
        showStartGameWindow();
        runGame();
        endGame();
    }
 
    static void showStartGameWindow() {
        Console.Clear();
 
        StartGameWindow startGameWindow = new StartGameWindow();
        while (!startGameWindow.isGameStarted) {
            if (Console.KeyAvailable) switch (Console.ReadKey().Key) {
                case ConsoleKey.LeftArrow:
                    startGameWindow.cursorIndex -= 1;
                    break;
                case ConsoleKey.RightArrow:
                    startGameWindow.cursorIndex += 1;
                    break;
                case ConsoleKey.Enter:
                    startGameWindow.clickCurrentButton();
                    break;
            }
 
            startGameWindow.show();
 
            System.Threading.Thread.Sleep(1000 / GameSettings.fps);
        }
    }
 
    static void runGame() {
        Console.SetWindowSize(GameSettings.consoleWidth, GameSettings.consoleHeight);
        Console.Clear();
 
        bool run = true;
        bool pause = false;
        Ball ball = new Ball(
            new Random().Next(GameSettings.consoleWidth / 2 - 10, GameSettings.consoleWidth / 2 + 10), GameSettings.consoleHeight / 1.7
        );
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
                case ConsoleKey.Escape:
                    run = false;
                    break;
                case ConsoleKey.Spacebar:
                    pause = !pause;
                    break;
            }
            if (!run) break;
            if (pause) continue;
 
            bar.show();
            ball.move();
            ball.checkColideWithBar(bar);
 
            foreach (Brick brick in bricks) { 
                brick.show();
                if (brick.checkCollideWithBall(ball)) {
                    bricks.Remove(brick);
                    break;
                }
            } 
 
            System.Threading.Thread.Sleep(1000 / GameSettings.fps);
 
            if (!ball.isAlive) run = false;
            if (bricks.Count == 0) run = false;
        }
 
 
    }
 
    static void generateBricks(ref List<Brick> bricks) {
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
 
    static void endGame() {
        Console.Clear();
        Console.SetCursorPosition(1, 1);
        Console.WriteLine("The end");
    }
}
 
 
class Ball {
    public int speedX { get; set; } 
    public int speedY { get; set; }
    public int coordX { get; set; }
    public int coordY { get; set; }
    public bool isAlive;
 
    char symb;
    ConsoleColor symbColor;
 
    public Ball(double coordX, double coordY) {
        this.coordX = Convert.ToInt32(coordX);
        this.coordY = Convert.ToInt32(coordY);
 
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
 
    char symb = '=';
 
    public Bar() {
        width = GameSettings.barWidth;
        coordX = GameSettings.consoleWidth / 2 - width / 2;
        coordY = GameSettings.consoleHeight - 2;
        coordsX = new int[width];
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
        if (coordX > GameSettings.consoleWidth - width) coordX = GameSettings.consoleWidth - width;
        updateCoordsX();
    }
 
    void updateCoordsX() {
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
 
    char symb;
 
    public Brick(int x, int y, ConsoleColor color = ConsoleColor.Blue) {
        coordX = x;
        coordY = y;
        coordsX = new int[width];
        this.color = color;
        width = GameSettings.brickWidth;
 
        symb = '■';
 
        updateCoordsX();
    }
 
    public void show() {
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
        symb = ' ';
    }
 
    public bool checkCollideWithBall(Ball ball) {
        if (  // Снизу и сверху
            (coordsX.Contains(ball.coordX) && coordY - 1 == ball.coordY) ||
            (coordsX.Contains(ball.coordX) && coordY + 1 == ball.coordY)
        ) {
            remove();
            ball.speedY *= -1;
            return true;
        }
 
        if (  // Слева и справа
            (coordsX.Contains(ball.coordX + 1) && coordY == ball.coordY) ||
            (coordsX.Contains(ball.coordX - 1) && coordY == ball.coordY)
        ) {
            remove();
            ball.speedX *= -1;
            return true;
        }
 
        return false;
    }
 
    void updateCoordsX() {
        coordsX = new int[width];
        for (int i = 0; i < width; i++) coordsX[i] = coordX + i;
    } 
}
 
 
class StartGameWindow {
    public int cursorIndex { get; set; }
    public ConsoleColor cursorColor { get; set; }
    public ConsoleColor gameNameTextColor { get; set; }
    public string choseMapName { get; }
    public bool isGameStarted { get; set; }
 
    int windowWidth;
    int windowHeight;
    string gameNameText;
    string startText;
 
    public StartGameWindow() {
        cursorIndex = 0;
        cursorColor = ConsoleColor.Red;
        gameNameTextColor = ConsoleColor.Blue;
        choseMapName = "classic.map";
        isGameStarted = false;
 
        windowWidth = 120;
        windowHeight = 20;
        Console.SetWindowSize(windowWidth, windowHeight);
 
        gameNameText = "▒█▀▀▀█ █░█ █░░█ ░█▀▀█ █▀▀█ █▀▀ █▀▀█ █▀▀▄ █▀▀█ ░▀░ █▀▀▄\n░▀▀▀▄▄ █▀▄ █▄▄█ ▒█▄▄█ █▄▄▀ █░░ █▄▄█ █░░█ █░░█ ▀█▀ █░░█\n▒█▄▄▄█ ▀░▀ ▄▄▄█ ▒█░▒█ ▀░▀▀ ▀▀▀ ▀░░▀ ▀░░▀ ▀▀▀▀ ▀▀▀ ▀▀▀░";
        /*
            ▒█▀▀▀█ █░█ █░░█ ░█▀▀█ █▀▀█ █▀▀ █▀▀█ █▀▀▄ █▀▀█ ░▀░ █▀▀▄ 
            ░▀▀▀▄▄ █▀▄ █▄▄█ ▒█▄▄█ █▄▄▀ █░░ █▄▄█ █░░█ █░░█ ▀█▀ █░░█ 
            ▒█▄▄▄█ ▀░▀ ▄▄▄█ ▒█░▒█ ▀░▀▀ ▀▀▀ ▀░░▀ ▀░░▀ ▀▀▀▀ ▀▀▀ ▀▀▀░
        */
        startText = "█▀ ▀█▀ ▄▀█ █▀█ ▀█▀\n▄█ ░█░ █▀█ █▀▄ ░█░"; 
        /*
            █▀ ▀█▀ ▄▀█ █▀█ ▀█▀
            ▄█ ░█░ █▀█ █▀▄ ░█░
        */
    }
 
    public void show() {
        showGameNameText();
        showStartText();
    }
 
    public void clickCurrentButton() {
        if (cursorIndex % 2 == 0) {
            isGameStarted = true;
        }
    }
 
    void showStartText() {
        if (cursorIndex % 2 == 0) {  // Вывод вехней обводки
            Console.ForegroundColor = cursorColor;
 
            Console.SetCursorPosition(windowWidth / 3 - startText.Length / 2 - 1, windowHeight / 2);
            Console.Write("+");
            for (int i = 0; i < startText.Length / 2; i++) Console.Write("-");
            Console.Write("+");
 
            Console.ResetColor();
        } else {  // Стираем верхнюю обводку
            Console.SetCursorPosition(windowWidth / 3 - startText.Length / 2 - 1, windowHeight / 2);
            Console.Write(string.Join("", Enumerable.Repeat(" ", startText.Length / 2 + 2)));
        }
 
        int k = 1;  // Для того, чтобы перемещать курсор (Надпись начать игру многострочная, поэтому приходится использовать данный способ)
        foreach (string part in startText.Split('\n')) {
            Console.SetCursorPosition(
                windowWidth / 3 - startText.Length / 2 - 1,
                windowHeight / 2 + k
            );
            if (cursorIndex % 2 == 0) { 
                Console.ForegroundColor = cursorColor;
                Console.Write("|");
                Console.ResetColor();
            } else Console.Write(" ");
 
            Console.Write(part);
 
            if (cursorIndex % 2 == 0) {
                Console.ForegroundColor = cursorColor;
                Console.Write("|");
                Console.ResetColor();
            } else Console.Write(" ");
            k++;
        }
 
        if (cursorIndex % 2 == 0) {
            Console.ForegroundColor = cursorColor;
 
            Console.SetCursorPosition(
                windowWidth / 3 - startText.Length / 2 - 1, 
                windowHeight / 2 + (startText.Length / (startText.Length / 2)) + 1
            );
            Console.Write("+");
            for (int i = 0; i < startText.Length / 2; i++) Console.Write("-");
            Console.Write("+");
 
            Console.ResetColor();
        }  else {  // Стираем верхнюю обводку
            Console.SetCursorPosition(windowWidth / 3 - startText.Length / 2 - 1, windowHeight / 2 + (startText.Length / (startText.Length / 2)) + 1);
            Console.Write(string.Join("", Enumerable.Repeat(" ", startText.Length / 2 + 2)));
        }
    }
 
    void showGameNameText() {
        Console.ForegroundColor = gameNameTextColor;
        int k = 1;
        foreach (string part in gameNameText.Split('\n')) {
            Console.SetCursorPosition(
                windowWidth / 2 - gameNameText.Length / gameNameText.Split('\n').Length / 2,
                (windowHeight / 2 - windowHeight / 4) - 2 + k
            );
            Console.Write(part);
            k++;
        }
        Console.ResetColor();
    }
}
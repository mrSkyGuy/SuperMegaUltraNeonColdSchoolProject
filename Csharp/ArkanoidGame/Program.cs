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
    static public string mapName = "smile.map";
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
                    startGameWindow.cursorIndex--;
                    break;
                case ConsoleKey.RightArrow:
                    startGameWindow.cursorIndex++;
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
            new Random().Next(
                GameSettings.consoleWidth / 2 - 15, 
                GameSettings.consoleWidth / 2 + 15
            ), 
            GameSettings.consoleHeight / 1.7
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
    public string choseMapName { get; set; }
    public bool isGameStarted { get; set; }
 
    int windowWidth;
    int windowHeight;
    string gameNameText;
    string startText;
    string chooseMapText;
 
    public StartGameWindow() {
        cursorIndex = 0;
        cursorColor = ConsoleColor.Red;
        gameNameTextColor = ConsoleColor.Blue;
        choseMapName = GameSettings.mapName;
        isGameStarted = false;
 
        windowWidth = 120;
        windowHeight = 20;
        Console.SetWindowSize(windowWidth, windowHeight);
 
        gameNameText = "▒█▀▀▀█ █░█ █░░█ ░█▀▀█ █▀▀█ █▀▀ █▀▀█ █▀▀▄ █▀▀█ ░▀░ █▀▀▄\n"
                     + "░▀▀▀▄▄ █▀▄ █▄▄█ ▒█▄▄█ █▄▄▀ █░░ █▄▄█ █░░█ █░░█ ▀█▀ █░░█\n"
                     + "▒█▄▄▄█ ▀░▀ ▄▄▄█ ▒█░▒█ ▀░▀▀ ▀▀▀ ▀░░▀ ▀░░▀ ▀▀▀▀ ▀▀▀ ▀▀▀░";

        startText = "█▀ ▀█▀ ▄▀█ █▀█ ▀█▀\n"
                  + "▄█ ░█░ █▀█ █▀▄ ░█░";
        chooseMapText = "";
    }
 
    public void show() {
        Console.ForegroundColor = gameNameTextColor;
        showText(gameNameText, windowWidth / 2, windowHeight / 2 - 1);
        Console.ResetColor();

        showText(
            startText, 
            windowWidth / 3 - 5, 
            windowHeight / 2 + windowHeight / 4, 
            true,
            Math.Abs(cursorIndex) % 2 == 0
        );

        chooseMapText = "█▀▀ █░█ █▀█ █▀█ █▀ █▀▀  █▀▄▀█ ▄▀█ █▀█\n"
                      + "█▄▄ █▀█ █▄█ █▄█ ▄█ ██▄  █░▀░█ █▀█ █▀▀";
        chooseMapText += '\n' + addSpaceAround(
            string.Join("", choseMapName.Split('.')[..^1]), 
            chooseMapText.Split('\n')[0].Length
        );

        showText(
            chooseMapText,
            windowWidth * 2 / 3,
            windowHeight / 2 + windowHeight / 4,
            true,
            Math.Abs(cursorIndex) % 2 == 1
        );
    }

    void showText(string text, int x, int y, bool mayBeCursor=false, bool cursorOn=false) {
        // MayBeCursor - параметр, нужный для того, чтобы знать, может ли элемент быть под курсором
        // Нужно это для того, чтобы знать, окружать ли элемент пробелами или нет (очистка от курсора)

        // Верхняя обводка
        if (cursorOn) {
            Console.ForegroundColor = cursorColor;
 
            Console.SetCursorPosition(
                x - text.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0), 
                y - text.Split('\n').Length / 2 - (mayBeCursor ? 1 : 0)
            );
            Console.Write(
                "+"
                + string.Join("", Enumerable.Repeat("-", text.Split('\n')[0].Length))
                + "+"
            );
 
            Console.ResetColor();
        } else if (mayBeCursor) {
            Console.SetCursorPosition(
                x - text.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0), 
                y - text.Split('\n').Length / 2 - (mayBeCursor ? 1 : 0)
            );
            Console.Write(
                string.Join(
                    "", 
                    Enumerable.Repeat(" ", text.Split('\n')[0].Length + 2)
                )
            );
        }

        // Контент (текст)
        int k = 1;
        foreach (string part in text.Split('\n')) {
            Console.SetCursorPosition(
                x - part.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0),
                y - text.Split('\n').Length / 2 - (mayBeCursor ? 1 : 0) + k
            );
            if (cursorOn) { 
                Console.ForegroundColor = cursorColor;
                Console.Write("|");
                Console.ResetColor();
            } else if (mayBeCursor) Console.Write(" ");
 
            Console.Write(part);
 
            if (cursorOn) {
                Console.ForegroundColor = cursorColor;
                Console.Write("|");
                Console.ResetColor();
            } else if (mayBeCursor) Console.Write(" ");
            k++;
        }

        // Нижняя обводка
        if (cursorOn) {
            Console.ForegroundColor = cursorColor;
 
            Console.SetCursorPosition(
                x - text.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0), 
                y - text.Split('\n').Length / 2 + text.Split('\n').Length
            );
            Console.Write(
                "+"
                + string.Join("", Enumerable.Repeat("-", text.Split('\n')[0].Length))
                + "+"
            );
 
            Console.ResetColor();
        } else if (mayBeCursor) {
            Console.SetCursorPosition(
                x - text.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0), 
                y - text.Split('\n').Length / 2 + text.Split('\n').Length
            );
            Console.Write(
                string.Join(
                    "", 
                    Enumerable.Repeat(" ", text.Split('\n')[0].Length + 2)
                )
            );
        }
    }

    public void clickCurrentButton() {
        if (Math.Abs(cursorIndex) % 2 == 0) {
            isGameStarted = true;
        } else if (Math.Abs(cursorIndex) % 2 == 1) {
            Console.Clear();
            showChooseMapWindow();
            Console.Clear();
            Console.SetWindowSize(windowWidth, windowHeight);
        }
    }

    void showChooseMapWindow() {
        ChooseMapWindow chooseMapWindow = new ChooseMapWindow();
        
        bool run = true;
        while (run) {
            if (Console.KeyAvailable) switch (Console.ReadKey().Key) {
                case ConsoleKey.Escape:
                    run = false;
                    break;
                case ConsoleKey.Enter:
                    GameSettings.mapName = chooseMapWindow.choseMapName;
                    choseMapName = chooseMapWindow.choseMapName;
                    run = false;
                    break;
                case ConsoleKey.UpArrow:
                    chooseMapWindow.cursorDown();
                    break;
                case ConsoleKey.DownArrow:
                    chooseMapWindow.cursorUp();
                    break;
            }
            if (!run) break;

            chooseMapWindow.show();
        }
    }

    string addSpaceAround(string text, int len) {
        string result = "";

        int spaceCount = len - text.Length;
        if (spaceCount > 0) result += string.Join("", Enumerable.Repeat(
                                                        " ", spaceCount / 2
                                                      ));
        result += text;
        if (spaceCount > 0) result += string.Join("", Enumerable.Repeat(
                                                        " ", spaceCount - spaceCount / 2
                                                      ));
        return result;
    }
}


class ChooseMapWindow {
    public int cursorIndex { get; set; }
    public ConsoleColor cursorColor { get; set; }
    public List<string> mapList { get; set; }
    public string choseMapName { get; set; }
    public bool isWindowClosed { get; set; }
 
    int windowWidth;
    int windowHeight;
    string chooseMapText;

    public ChooseMapWindow() {
        cursorIndex = 0;
        cursorColor = ConsoleColor.Red;
        mapList = new List<string>();
        fillMapList();
        choseMapName = GameSettings.mapName;
        isWindowClosed = false;

        windowWidth = 120;
        windowHeight = 40;
        Console.SetWindowSize(windowWidth, windowHeight);

        chooseMapText = "█▀▀ █░█ █▀█ █▀█ █▀ █▀▀  █▀▄▀█ ▄▀█ █▀█\n"
                      + "█▄▄ █▀█ █▄█ █▄█ ▄█ ██▄  █░▀░█ █▀█ █▀▀";
    }

    public void show() {
        showText(
            chooseMapText, 
            windowWidth / 2,
            chooseMapText.Split('\n').Length
        );

        int yCoord = chooseMapText.Split('\n').Length + 2;
        foreach(string mapName in mapList) {
            showText(
                string.Join("", mapName.Split('.')[..^1]), 
                3 + mapName.Length / 2,
                yCoord,
                true,
                mapName == choseMapName
            );
            yCoord += 3;
        }

        showCurrentMap();
    }

    void fillMapList() {
        foreach (string file in Directory.GetFiles("./maps/")) {
            if (file.Split('.')[^1] == "map") {
                mapList.Add(file.Split("/")[^1]);
            }
        }
    }

    public void cursorDown() {
        cursorIndex--;
        if (cursorIndex < 0) cursorIndex = mapList.Count() - 1;
        choseMapName = mapList[cursorIndex];

        // Очистка от прошло карты
        for (int i = 0; i < 20; i++) {
            showText(
                string.Join("", Enumerable.Repeat(" ", 20)), 
                windowWidth / 2, 
                windowHeight / 2 - 10 + i
            );
        }
    }
    public void cursorUp() {
        cursorIndex++;
        if (cursorIndex >= mapList.Count()) cursorIndex = 0;
        choseMapName = mapList[cursorIndex];

        // Очистка от прошлой карты
        for (int i = 0; i < 20; i++) {
            showText(
                string.Join("", Enumerable.Repeat(" ", 20)), 
                windowWidth / 2, 
                windowHeight / 2 - 10 + i
            );
        }
    }

    void showCurrentMap() {
        using (StreamReader file = new StreamReader($"maps/{choseMapName}")) {
            string[] text = file.ReadToEnd().Split('\n');
            if (text.Length == 1) return;

            int lineCount = 0;
            foreach(string line in text) {
                showText(
                    line, 
                    windowWidth / 2 - (lineCount == text.Length - 1 ? 1 : 0), 
                    windowHeight / 2 - text.Length / 2 + lineCount
                );
                lineCount++;
            }
        }
    }

    void showText(string text, int x, int y, bool mayBeCursor=false, bool cursorOn=false) {
        // MayBeCursor - параметр, нужный для того, чтобы знать, может ли элемент быть под курсором
        // Нужно это для того, чтобы знать, окружать ли элемент пробелами или нет (очистка от курсора)

        // Верхняя обводка
        if (cursorOn) {
            Console.ForegroundColor = cursorColor;
 
            Console.SetCursorPosition(
                x - text.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0), 
                y - text.Split('\n').Length / 2 - (mayBeCursor ? 1 : 0)
            );
            Console.Write(
                "+"
                + string.Join("", Enumerable.Repeat("-", text.Split('\n')[0].Length))
                + "+"
            );
 
            Console.ResetColor();
        } else if (mayBeCursor) {
            Console.SetCursorPosition(
                x - text.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0), 
                y - text.Split('\n').Length / 2 - (mayBeCursor ? 1 : 0)
            );
            Console.Write(
                string.Join(
                    "", 
                    Enumerable.Repeat(" ", text.Split('\n')[0].Length + 2)
                )
            );
        }

        // Контент (текст)
        int k = 1;
        foreach (string part in text.Split('\n')) {
            Console.SetCursorPosition(
                x - part.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0),
                y - text.Split('\n').Length / 2 - (mayBeCursor ? 1 : 0) + k
            );
            if (cursorOn) { 
                Console.ForegroundColor = cursorColor;
                Console.Write("|");
                Console.ResetColor();
            } else if (mayBeCursor) Console.Write(" ");
 
            Console.Write(part);
 
            if (cursorOn) {
                Console.ForegroundColor = cursorColor;
                Console.Write("|");
                Console.ResetColor();
            } else if (mayBeCursor) Console.Write(" ");
            k++;
        }

        // Нижняя обводка
        if (cursorOn) {
            Console.ForegroundColor = cursorColor;
 
            Console.SetCursorPosition(
                x - text.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0), 
                y - text.Split('\n').Length / 2 + text.Split('\n').Length
            );
            Console.Write(
                "+"
                + string.Join("", Enumerable.Repeat("-", text.Split('\n')[0].Length))
                + "+"
            );
 
            Console.ResetColor();
        } else if (mayBeCursor) {
            Console.SetCursorPosition(
                x - text.Split('\n')[0].Length / 2 - (mayBeCursor ? 1 : 0), 
                y - text.Split('\n').Length / 2 + text.Split('\n').Length
            );
            Console.Write(
                string.Join(
                    "", 
                    Enumerable.Repeat(" ", text.Split('\n')[0].Length + 2)
                )
            );
        }
    }
}

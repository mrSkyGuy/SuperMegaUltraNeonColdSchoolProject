using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
 
// cd C:\Users\nurma\myLife-xD\SuperMegaUltraNeonColdSchoolProject\Csharp\ArkanoidGame\ && dotnet run
struct GameSettings {
    static public int consoleHeight = 35,
                      consoleWidth = 70, 
                      fps = 10,
                      brickSize = 1,
                      barWidth = 10,
                      marginBottom = 15,
                      marginLeft = 2;
    static public string mapName = "smile.map";

    static public Dictionary<char, Func<Dictionary<string, object>, object>> sprites 
        = new Dictionary<char, Func<Dictionary<string, object>, object>>() {
            {
                '=', (Dictionary<string, dynamic> kwargs) => {
                    if (kwargs.Keys.Contains("color")) 
                        return new Brick(kwargs["x"], kwargs["y"], kwargs["color"]);
                    return new Brick(kwargs["x"], kwargs["y"]);
                }
            }
    };
}
 
 
class Program {
    static public List<dynamic> items = new List<dynamic>();

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
        renderMap();

        Console.SetWindowSize(GameSettings.consoleWidth, GameSettings.consoleHeight);
        Console.Clear();
 
        bool run = true;
        bool pause = false;
        Ball ball = new Ball(
            new Random().Next(
                GameSettings.consoleWidth / 2 - GameSettings.consoleWidth / 4, 
                GameSettings.consoleWidth / 2 + GameSettings.consoleWidth / 4
            ), 
            GameSettings.consoleHeight - (GameSettings.marginBottom - 1)
        );
        Bar bar = new Bar();

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
            ball.checkCollideWithBar(bar);

            bool wasCollide = false;
            foreach (var item in items) { 
                item.show();
                if (item.checkCollideWithBall(ball)) {
                    wasCollide = true;
                    items.Remove(item);
                    break;
                }
            } 
            // Проверка на соприкосновение по диагонали, после того, как проверили 4 стороны (цикл выше) 
            if (!wasCollide) {
                foreach (var item in items) {
                    if (item.checkCollideWithBall(ball, true)) {
                        items.Remove(item);
                        break;
                    }
                }
            }
 
            System.Threading.Thread.Sleep(1000 / GameSettings.fps);
 
            if (!ball.isAlive) run = false;
            if (items.Count == 0) run = false;
        }
    }

    static void renderMap() {
        // Генерация выбранной карты (GameSettings.mapName)

        using (StreamReader mapFile = new StreamReader($"maps/{GameSettings.mapName}")) {
            string[] map = mapFile.ReadToEnd().Split('\n');

            int height = map.Length * GameSettings.brickSize;
            int width = map.Select(item => item.Length).Max() * GameSettings.brickSize;
            GameSettings.consoleWidth = width + GameSettings.marginLeft * 2;
            GameSettings.consoleHeight = height + GameSettings.marginBottom;

            int y = 1;
            foreach (string line in map) {
                int x = GameSettings.marginLeft + 1;
                foreach (char symb in line) {
                    if (GameSettings.sprites.Keys.Contains(symb)) {
                        var item = GameSettings.sprites[symb](
                            new Dictionary<string, dynamic>{ {"x", x}, {"y", y} }
                        );
                        items.Add(item);
                    }
                    x += GameSettings.brickSize;
                }
                y += GameSettings.brickSize;
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
 
    public void checkCollideWithBar(Bar bar) {
        if (bar.coordsX.Contains(this.coordX) && bar.coordY - 1 == this.coordY) {
            speedY *= -1;
        }
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
        coordsX = new int[width + 2];
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
        if (coordX < 0) coordX = 0; 
        updateCoordsX();
    }
 
    public void moveRight() {
        hide();
        coordX += speedX;
        if (coordX > GameSettings.consoleWidth - width) coordX = GameSettings.consoleWidth - width;
        updateCoordsX();
    }
 
    void updateCoordsX() {
        coordsX = new int[width + 2];
        for (int i = 0; i < width + 2; i++) coordsX[i] = coordX + i;
    } 
}
 
 
class Brick {
    public int size;
    public int coordX { get; set; }
    public int coordY { get; set; } 
    public int[] coordsX { get; set; }
    public int[] coordsY { get; set; }
    public ConsoleColor color;
 
    char symb;
 
    public Brick(int x, int y, ConsoleColor color = ConsoleColor.Blue) {
        size = GameSettings.brickSize;
        coordX = x;
        coordY = y;
        coordsX = new int[size];
        coordsY = new int[size];
        this.color = color;
 
        symb = '■';
 
        updateCoordsXY();
    }
 
    public void show() {
        Console.ForegroundColor = color;
        for (int y = 0; y < size; y++) {
            Console.SetCursorPosition(coordX, coordY + y);
            for (int x = 0; x < size; x++) {
                Console.Write(symb);
            }
        }
        Console.ResetColor();
    }
 
    public void remove() {
        symb = ' ';
        show();
    }
 
    public bool checkCollideWithBall(Ball ball, bool corners = false) {
        // StreamWriter log = new StreamWriter("log.txt", true);
        if (corners) {
            if ( // По углам
                (coordsX[0] == ball.coordX + 1 && coordsY[^1] == ball.coordY - 1) || // левый нижний
                (coordsX[^1] == ball.coordX - 1 && coordsY[^1] == ball.coordY - 1) || // правый нижний
                (coordsX[0] == ball.coordX + 1 && coordsY[0] == ball.coordY + 1) || // левый верхний
                (coordsX[^1] == ball.coordX - 1 && coordsY[0] == ball.coordY + 1) // правый верхний
            ) {
                remove();
                ball.speedY *= -1; 
                // log.WriteLine("Ударился по диагонали");
                // log.WriteLine($"X: [{string.Join(", ", coordsX)}] {ball.coordX} \nY: [{string.Join(", ", coordsY)}] {ball.coordY}\n");
                // log.Close();
                return true;
            }

            return false;
        }

        if (  // Снизу и сверху
            (coordsX.Contains(ball.coordX) && coordsY.Contains(ball.coordY + 1)) ||
            (coordsX.Contains(ball.coordX) && coordsY.Contains(ball.coordY - 1)) ||
            (coordsX.Contains(ball.coordX) && coordsY.Contains(ball.coordY))
        ) {
            remove();
            ball.speedY *= -1;
            // log.WriteLine("Ударился сверху или снизу");
            // log.WriteLine($"X: [{string.Join(", ", coordsX)}] {ball.coordX} \nY: [{string.Join(", ", coordsY)}] {ball.coordY}\n");
            // log.Close();
            return true;
        } else  if (  // Слева и справа
            (coordsX.Contains(ball.coordX + 1) && coordsY.Contains(ball.coordY)) ||
            (coordsX.Contains(ball.coordX - 1) && coordsY.Contains(ball.coordY))
        ) {
            remove();
            ball.speedX *= -1;
            // log.WriteLine("Ударился слева или справа");
            // log.WriteLine($"X: [{string.Join(", ", coordsX)}] {ball.coordX} \nY: [{string.Join(", ", coordsY)}] {ball.coordY}\n");
            // log.Close();
            return true;
        }
        // log.Close();
        return false;
    }
 
    void updateCoordsXY() {
        coordsX = new int[size];
        for (int i = 0; i < size; i++) coordsX[i] = coordX + i;

        coordsY = new int[size];
        for (int i = 0; i < size; i++) coordsY[i] = coordY + i;
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
        cursorColor = ConsoleColor.Red;
        mapList = new List<string>();
        fillMapList();
        choseMapName = GameSettings.mapName;
        cursorIndex = mapList.IndexOf(choseMapName);
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
        mapList.Reverse();
    }

    public void cursorDown() {
        cursorIndex--;
        if (cursorIndex < 0) cursorIndex = mapList.Count() - 1;
        choseMapName = mapList[cursorIndex];

        clearMap();
    }

    public void cursorUp() {
        cursorIndex++;
        if (cursorIndex >= mapList.Count()) cursorIndex = 0;
        choseMapName = mapList[cursorIndex];

        clearMap();
    }

    void clearMap() {
        // Очистка от прошлой карты
        int size = 30;
        for (int i = 0; i < size; i++) {
            showText(
                string.Join("", Enumerable.Repeat(" ", size)), 
                windowWidth / 2, 
                windowHeight / 2 - size / 2 + i
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

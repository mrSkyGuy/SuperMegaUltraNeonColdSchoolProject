using System;
using System.Linq;
 
namespace Snowfall {
    class Program {
        static void Main(string[] args) {
            Console.Clear();
 
            const int snowflakesCount = 5,  // Количество снежинок
                      consoleWidth = 110,
                      consoleHeight = 40;
            int snowflakesCountOnScreen = 0;
 
            Snowflake[] snowflakes = new Snowflake[consoleHeight * consoleWidth];  // Все снежинки
            Snowdrift snowdrift = new Snowdrift(consoleHeight * consoleWidth);  // Сугроб
 
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.CursorVisible = false;
 
            bool pause = false, run = true;
            int fps = 50;
            int d = 20;
            while (run) {
                if (Console.KeyAvailable) {
                    switch (Console.ReadKey().Key) {
                        case ConsoleKey.Escape:  // Завершение работы приложения
                            run = false;
                            break;
                        case ConsoleKey.Spacebar:  // Пауза
                            pause = !pause;
                            break;
                        case ConsoleKey.UpArrow:  // Ускорение
                            fps -= fps >= 40 ? 10 : 0;
                            break;
                        case ConsoleKey.DownArrow:  // Замедление
                            fps += fps <= 2000 ? 10 : 0;
                            break;
                        case ConsoleKey.Enter:  // Новая волна
                            d -= d >= 3 ? 1 : 0;
                            break;
                    }
                }
                if (pause) continue;

                if (snowflakesCountOnScreen + snowflakesCount < consoleHeight * consoleWidth / d) {
                    int temp = snowflakesCountOnScreen;
                    for (int i = snowflakesCountOnScreen; i < snowflakesCountOnScreen + snowflakesCount; i++) {
                        snowflakes[i] = new Snowflake(
                            snowdrift, consoleWidth, consoleHeight, symb: '+'
                        );
                        temp++;
                    }
                    snowflakesCountOnScreen = temp;
                }
                snowdrift.Update();
                foreach (var snowflake in snowflakes) {
                    if (!(snowflake is null)) snowflake.Fall();
                    else break;
                }
                System.Threading.Thread.Sleep(fps);
                Console.Clear();
            }
            Console.Clear();
            Console.WriteLine(@"
░██████╗██████╗░██████╗░██╗███╗░░██╗░██████╗░  ░█████╗░██╗░░░░░██████╗░███████╗░█████╗░██████╗░██╗░░░██╗
██╔════╝██╔══██╗██╔══██╗██║████╗░██║██╔════╝░  ██╔══██╗██║░░░░░██╔══██╗██╔════╝██╔══██╗██╔══██╗╚██╗░██╔╝
╚█████╗░██████╔╝██████╔╝██║██╔██╗██║██║░░██╗░  ███████║██║░░░░░██████╔╝█████╗░░███████║██║░░██║░╚████╔╝░
░╚═══██╗██╔═══╝░██╔══██╗██║██║╚████║██║░░╚██╗  ██╔══██║██║░░░░░██╔══██╗██╔══╝░░██╔══██║██║░░██║░░╚██╔╝░░
██████╔╝██║░░░░░██║░░██║██║██║░╚███║╚██████╔╝  ██║░░██║███████╗██║░░██║███████╗██║░░██║██████╔╝░░░██║░░░
╚═════╝░╚═╝░░░░░╚═╝░░╚═╝╚═╝╚═╝░░╚══╝░╚═════╝░  ╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝╚═════╝░░░░╚═╝░░░

██╗░██████╗  ██╗░░██╗███████╗██████╗░███████╗
██║██╔════╝  ██║░░██║██╔════╝██╔══██╗██╔════╝
██║╚█████╗░  ███████║█████╗░░██████╔╝█████╗░░
██║░╚═══██╗  ██╔══██║██╔══╝░░██╔══██╗██╔══╝░░
██║██████╔╝  ██║░░██║███████╗██║░░██║███████╗
╚═╝╚═════╝░  ╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝╚══════╝");
        }
    }
 
 
    class Snowflake {
        public int SpeedY { get; set; }
        public Snowdrift SnowDrift;
        public char Symb { get; set; }
        public ConsoleColor Color { get; set; }
 
        public int X { get; set; }
        public int Y { get; set; }
        private int Ww, Wh;
        private bool Wind, Right = false;
 
 
        public Snowflake(Snowdrift snowdrift, int ww, int wh, 
                         int speed = 1, bool wind = false, char symb = '*') {
            SpeedY = speed;
            SnowDrift = snowdrift;
            Wind = wind;
            Color = GetRandomColor();
 
            Random rnd = new Random();
            Ww = ww;
            Wh = wh;
            X = rnd.Next(1, Ww);
            Y = 1;
 
            Symb = symb;
        }
 
        public void Fall() {
            // Реализует падение снежинки
 
            if (SnowDrift.IsSnowflakeInSnowdrift(this)) {
                // Если эта снижиника в сугробе, то не падаем дальше
                return;
            } 
 
            int tempY = Y, tempX = X;
            bool tempRight = Right;
 
            tempY += SpeedY;
            if (Wind) tempX += 2;  // Если ветер, то только вправо
            else {  // Если ветра нет, то в стороны поочередно
                if (tempRight) tempX++;
                else tempX--;
                tempRight = !tempRight;
            }
 
            if (SnowDrift.IsCellFree(tempX, tempY)) {
                X = tempX; Y = tempY; Right = tempRight;
            } else if (SnowDrift.IsCellFree(X, tempY)) {
                Y = tempY;
            } else {
                SnowDrift.AddSnowflake(this);
                return;
            }
            if (Y >= Wh) {
                SnowDrift.AddSnowflake(this);
                return;
            }
            if (X >= Ww) X = 1; if (X <= 0) X = Ww - 1;
 
            ShowSnowflake();
        }
 
        public void ShowSnowflake() {
            // Отрисовка снежинки
 
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Color;
            Console.Write(Symb);
        }
 
        private ConsoleColor GetRandomColor() {
            // Получение случайного цвета
 
            ConsoleColor[] colors = {
                ConsoleColor.Gray, ConsoleColor.Blue, 
                ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Red
            };
            Random rnd = new Random();
            return colors[rnd.Next(0, colors.Length)];
        }
    }
 
 
    class Snowdrift {
        private int Volume;
        private Snowflake[] snowflakes; 
 
 
        public Snowdrift(int volume) {
            Volume = volume;
 
            snowflakes = new Snowflake[Volume];
        }
 
        public void Update() {
            // Отрисовка всех снежинок
 
            foreach (Snowflake snowflake in snowflakes) {
                if (!(snowflake is null)) snowflake.ShowSnowflake();
            }
            // foreach (Snowflake snowflake in snowflakes) snowflake.ShowSnowflake();
        }

        public void AddSnowflake(Snowflake snowflake) {
            snowflakes = snowflakes.Append(snowflake).ToArray();
        }
 
        public bool IsSnowflakeInSnowdrift(Snowflake snowflake) {
            // Есть ли снежинка в сугробе
 
            foreach (var snowflakeItem in snowflakes) {
                if (snowflakeItem == snowflake) return true;
            }
 
            return false;
        }
 
        public bool IsCellFree(int x, int y) {
            // Есть ли место для снежинки в данной ячейке
 
            foreach (var snowflake in snowflakes) {
                if (!(snowflake is null)) {
                    if (snowflake.X == x && snowflake.Y == y) return false;
                }
            }
 
            return true;
        }
    }
}
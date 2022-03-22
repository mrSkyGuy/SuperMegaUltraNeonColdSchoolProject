using System;
 
namespace Snowfall {
    class Program {
        static void Main(string[] args) {
            Console.Clear();
 
            const int snowflakesCount = 50;  // Количество снежинок
            Snowflake[] snowflakes = new Snowflake[snowflakesCount];  // Все снежинки
            Snowdrift snowdrift = new Snowdrift(snowflakesCount);  // Сугроб
 
            const int consoleWidth = 50;
            const int consoleHeight = 50;
 
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.CursorVisible = false;
 
            for (int i = 0; i < snowflakesCount; i++) snowflakes[i] = new Snowflake(
                snowdrift, ww: consoleWidth, wh: consoleHeight
            ); 

            while (true) {
                snowdrift.Update();
                foreach (var snowflake in snowflakes) snowflake.Fall();
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
 

    class Snowflake {
        public int SpeedY { get; set; }
        public Snowdrift SnowDrift;
        public char Symb { get; set; }
 
        public int X { get; set; }
        public int Y { get; set; }
        private int Ww, Wh;
        private bool Wind, Right = false;
 
 
        public Snowflake(Snowdrift snowdrift, int speed = 1, int ww = 20, 
                         int wh = 20, bool wind = false, char symb = '*') {
            SpeedY = speed;
            SnowDrift = snowdrift;
            Wind = wind;
 
            Random rnd = new Random();
            Ww = ww;
            Wh = wh;
            X = rnd.Next(1, Ww);
            Y = rnd.Next(1, Wh);
 
            Symb = symb;
        }
 
        public void Fall() {
            // Реализует падение снежинки

            if (SnowDrift.IsSnowflakeInSnowdrift(this)) return;  // Если эта снижиника в сугробе, то не падаем дальше
 
            Y += SpeedY;
            if (Wind) X += 2;  // Если ветер, то только вправо
            else {  // Если ветра нет, то в стороны по очередно
                if (Right) X++;
                else X--;
                Right = !Right;
            }
            if (X >= Ww) X = 1;
            if (Y >= Wh) Y = 1;
 
            ShowSnowflake();
        }
 
        public void ShowSnowflake() {
            // Отрисовка снежинки
            
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = GetRandomColor();
            Console.Write(Symb);
        }
 
        private ConsoleColor GetRandomColor() {
            // Получение случайного цвета

            ConsoleColor[] colors = {
                ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.Blue, 
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

            foreach (var snowflake in snowflakes) snowflake.ShowSnowflake();
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
                if (snowflake.X == x && snowflake.Y == y) return false;
            }
 
            return true;
        }
    }
}
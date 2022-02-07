using System;


namespace Project {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Введите сообщение: ");
            PrettyPrint(Console.ReadLine());
        }

        static void PrettyPrint(string? msg) {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}


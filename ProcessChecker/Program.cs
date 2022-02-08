using System;
using System.Diagnostics;


class Program {
    static void Main(string[] args) {
        Console.Clear();  // Чистим консоль от грязи
        PrintIntro();
        while(true) PrintInfo();
    }
 
    static void PrintInfo() {
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("\n+-------------------------------------------------------------+");
        Console.WriteLine("Введите название необходимой команды:");
        Console.WriteLine("  [0] - Завершить работу программы ");
        Console.WriteLine("  [1] - Завершить работу приложения по id");
        Console.WriteLine("  [2] - Завершить работу приложения по имени");
        Console.WriteLine("  [3] - Найти процессы по имени или его части");
        Console.WriteLine("  [4] - Вывод всех процессов");
        Console.WriteLine();
        Console.ResetColor();

        string[] commands = {"0", "1", "2", "3", "4"};

        string? commandName;
        while (true) {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("[+] ");
            Console.ResetColor();

            commandName = Console.ReadLine();
            if (!CheckItemInArray(commandName, commands)) PrintError("Команда не найдена. Повторите попытку");
            else break;
        }

        if (commandName == "0") Process.GetCurrentProcess().Kill();
        if (commandName == "1") KillProcessById();
        if (commandName == "2") KillProcessByName();
        if (commandName == "3") Search();
        if (commandName == "4") PrintListOfProcesses();
    }

    static void PrintIntro() {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@"░█▀▀█ █▀▀█ █▀▀█ █▀▀ █▀▀ █▀▀ █▀▀ ░█▀▀█ █──█ █▀▀ █▀▀ █─█ █▀▀ █▀▀█ 
░█▄▄█ █▄▄▀ █──█ █── █▀▀ ▀▀█ ▀▀█ ░█─── █▀▀█ █▀▀ █── █▀▄ █▀▀ █▄▄▀ 
░█─── ▀─▀▀ ▀▀▀▀ ▀▀▀ ▀▀▀ ▀▀▀ ▀▀▀ ░█▄▄█ ▀──▀ ▀▀▀ ▀▀▀ ▀─▀ ▀▀▀ ▀─▀▀");
        Console.ResetColor();
    }

    static void PrintListOfProcesses() {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("[-] Результат запроса:");

        var listOfProcesses = Process.GetProcesses();
        foreach(var i in listOfProcesses) {
            System.Console.WriteLine($"    * {i.Id}\t{i.ProcessName}");
        }
        Console.ResetColor();
    }

    static void KillProcessById() {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("[-] Введите id: ");
        Console.ResetColor();

        string? reqId = Console.ReadLine();
        Process.GetProcessById(int.Parse(reqId)).Kill();

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("   Выполено успешно");
        Console.ResetColor();
    }

    static void KillProcessByName() {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("[-] Введите имя процесса: ");
        Console.ResetColor();

        string? reqName = Console.ReadLine();
        Process.GetProcessesByName(reqName)[0].Kill();

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("   Выполено успешно");
        Console.ResetColor();
    }

    static void Search() {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("[-] Введите запрос: ");
        Console.ResetColor();
        
        string? req = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("[-] Результаты поиска:");
        foreach (var process in Process.GetProcesses()) {
            if (process.ProcessName.Contains(req)) {
                Console.WriteLine($"    {process.Id} -> {process.ProcessName}");
            }
        }
        Console.ResetColor();
    }

    static bool CheckItemInArray(string? item, string[] arr) {
        foreach(string i in arr) if (i == item) return true;
        return false;
    }

    static void PrintError(string msg) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg);
        Console.ResetColor();
    }
}

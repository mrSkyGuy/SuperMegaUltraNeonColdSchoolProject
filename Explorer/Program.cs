﻿using System;
using System.IO;
using System.Linq;
 
 
class Program {
    static void Main(string[] args) {
        bool updateConsole = true;
        string? command = null;  // Ну да, костыль
        while (true) {
            if (command != null) {
                string[] commandParts = command.Split(' ');
                if (commandParts[0] == "exit") return;
                else if (commandParts[0] == "cd") {
                    string reqDir = Join(commandParts[1..]);
                    Console.WriteLine(reqDir);
                    Directory.SetCurrentDirectory(reqDir);
                } else if (commandParts[0] == "md") {
                    string dirName = Join(commandParts[1..]);
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + dirName);
                } else if (commandParts[0] == "deldir") {
                    string dirName = Join(commandParts[1..]);
                    try {
                        Directory.Delete(Directory.GetCurrentDirectory() + "\\"  + dirName, true);
                    } catch (Exception) {
                        Console.WriteLine("Directory not found, bruh!");
                        updateConsole = false;
                    }
                } else if (commandParts[0] == "delf") {
                    string fileName = Join(commandParts[1..]);
                    try {
                        File.Delete(Directory.GetCurrentDirectory() + "\\"  + fileName);
                    } catch (Exception) {
                        Console.WriteLine("File not found, bruh!");
                        updateConsole = false;
                    }
                } else if (commandParts[0] == "help") {
                    Console.WriteLine("There are the commands: exit, cd, md, deldir, delf, help");
                    updateConsole = false;
                } else {
                    Console.WriteLine($"There is not the command: {command}");
                    updateConsole = false;
                }
            }
 
            if (updateConsole) {
                Console.Clear();
                string curDir = Directory.GetCurrentDirectory();
                string[] dirs = Directory.GetDirectories(curDir);
                string[] files = Directory.GetFiles(curDir);
                string[][] data = {dirs, files};
                string[] header = {"dirs", "files"};
                int tableWidth = 101;
 
                PrintHorizontalLineWithTitle('-', tableWidth, curDir);
                PrintHeader(header, tableWidth);
                PrintData(data, tableWidth, '-');
            }
            updateConsole = true;
 
            Console.Write("\n-> ");
            command = Console.ReadLine();
        }
 
    }
    static void PrintHorizontalLineWithTitle(char symb, int len, string title) {
        Console.Write(symb);
        Console.Write(symb);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(title);
        Console.ResetColor();
 
        len -= title.Length + 2;
        for (int i = 0; i < len; i++) Console.Write(symb);
        Console.WriteLine();
    }
 
    static void PrintHeader(string[] headers, int len) {
        Console.Write("| ");
        int columnWidth = len / headers.Length - 1;
        foreach (var header in headers) {
            int spaceCount =  columnWidth - header.Length;
            for (int i = 0; i < spaceCount / 2; i++) Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(header);
            Console.ResetColor();
            for (int i = 0; i < spaceCount / 2; i++) Console.Write(" ");
            Console.Write("|");
        }
        Console.WriteLine();
        Console.Write(string.Concat(Enumerable.Repeat("-", len)));
        Console.WriteLine();
    }
 
    static void PrintData(string[][] data, int len, char symb) {
        int rows = GetMaxLength(data);
        for (int i = 0; i < rows; i++) {
            int columnWidth = len / data.Length - 2;
 
            Console.Write("|");
            foreach (var dataFrame in data) {
                string item = GetArrayElem(dataFrame, i).Split('\\')[^1];
                int spaceCount = columnWidth - item.Length;
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(item);
                Console.ResetColor();
                for (int x = 0; x < spaceCount; x++) Console.Write(" ");
                Console.Write("|");
            }
            Console.WriteLine();
        }
        for (int i = 0; i < len; i++) Console.Write(symb);
    }
 
    private static int GetMaxLength(string[][] arrays) {
        int maxLength = 0;
        foreach (var array in arrays) if (array.Length > maxLength) maxLength = array.Length;
        return maxLength;
    }
 
    private static string GetArrayElem(string[] array, int index) {
        if (index < array.Length) return array[index];
        else return "";
    }
 
    private static string Join(string[] array, char sep=' ') {
        string result = "";
        for (int i = 0; i < array.Length; i++) {
            result += array[i];
            if (i != array.Length - 1) result += sep;
        }
 
        return result;
    }
}
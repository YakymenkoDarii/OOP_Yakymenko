using System;
using System.Collections.Generic;
using System.IO;

namespace Lab8
{
    internal class Program
    {
        // Підрахунок слів через масив символів
        static int CountWordsArray(char[] symbols)
        {
            int count = 0;
            bool inWord = false;

            foreach (char c in symbols)
            {
                if (!char.IsWhiteSpace(c))
                {
                    if (!inWord)
                    {
                        count++;
                        inWord = true;
                    }
                }
                else
                {
                    inWord = false;
                }
            }

            return count;
        }

        // Підрахунок слів через колекцію List<char>
        static int CountWordsList(List<char> symbols)
        {
            int count = 0;
            bool inWord = false;

            foreach (char c in symbols)
            {
                if (!char.IsWhiteSpace(c))
                {
                    if (!inWord)
                    {
                        count++;
                        inWord = true;
                    }
                }
                else
                {
                    inWord = false;
                }
            }

            return count;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            PrintHeader();

            // Зчитування файлу
            string filePath = Path.Combine(Environment.CurrentDirectory, "some.txt");

            if (!File.Exists(filePath))
            {
                PrintError($"Файл не знайдено: {filePath}");
                Pause();
                return;
            }

            string text = File.ReadAllText(filePath, System.Text.Encoding.UTF8);

            // --- Спосіб 1: масив символів ---
            char[] symbols = text.ToCharArray();

            PrintSection("Спосіб 1: масив символів (char[])");
            Console.WriteLine($"  Довжина масиву  : {symbols.Length} символів");
            int wordCountArray = CountWordsArray(symbols);
            Console.WriteLine($"  Кількість слів  : {wordCountArray}");

            Console.WriteLine();

            // --- Спосіб 2: колекція List<char> ---
            List<char> charList = new List<char>(symbols);

            PrintSection("Спосіб 2: колекція List<char>");
            Console.WriteLine($"  Кількість елем. : {charList.Count}");
            int wordCountList = CountWordsList(charList);
            Console.WriteLine($"  Кількість слів  : {wordCountList}");

            Console.WriteLine();

            // --- Вміст файлу ---
            PrintSection("Вміст файлу");
            Console.WriteLine(text);

            Pause();
        }

        static void PrintHeader()
        {
            Console.WriteLine("  ╔══════════════════════════════════════════════╗");
            Console.WriteLine("  ║   Лаб. №8 — Підрахунок слів у файлі         ║");
            Console.WriteLine("  ║   Варіант 4 | масив та List<T>               ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        static void PrintSection(string title)
        {
            Console.WriteLine($"  ── {title} ──");
        }

        static void PrintError(string msg)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  [!] {msg}");
            Console.ForegroundColor = prev;
        }

        static void Pause()
        {
            Console.WriteLine("\n  Натисніть Enter, щоб завершити...");
            Console.ReadLine();
        }
    }
}

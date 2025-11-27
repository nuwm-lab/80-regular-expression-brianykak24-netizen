
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegexLab
{
    // Клас, що інкапсулює логіку пошуку та валідації IP-адрес
    

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Рекомендація: Робота з вхідними даними (не хардкодити)
            string inputText = GetInputText(args);

            if (string.IsNullOrWhiteSpace(inputText))
            {
                Console.WriteLine("Текст для аналізу відсутній.");
                return;
            }

            // Список наших пошуковців
            var searchers = new List<IPatternSearcher>
            {
                new IpSearcher(),
                new DateSearcher()
            };

            Console.WriteLine("\n--- Результати аналізу ---\n");

            foreach (var searcher in searchers)
            {
                ProcessSearcher(searcher, inputText);
            }

            // Console.ReadKey() прибираємо для CI/CD, але для лаби можна залишити очікування
            Console.WriteLine("\nРоботу завершено.");
        }

        static void ProcessSearcher(IPatternSearcher searcher, string text)
        {
            Console.WriteLine($">>> Пошук категорії: {searcher.Name}");

            // Матеріалізуємо результати в список для підрахунку
            var results = searcher.Search(text).ToList();

            if (results.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"   Знайдено збігів: {results.Count}");
                foreach (var item in results)
                {
                    Console.WriteLine($"   - {item}");
                }
            }
            else
            {
                // Рекомендація: Зрозуміле повідомлення замість пустоти
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("   Збігів не знайдено.");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        static string GetInputText(string[] args)
        {
            // Спроба 1: Читання з файлу, якщо шлях передано в аргументах
            if (args.Length > 0 && File.Exists(args[0]))
            {
                try
                {
                    return File.ReadAllText(args[0]);
                }
                catch (Exception ex) // Рекомендація: Обробка виключних ситуацій
                {
                    Console.WriteLine($"Помилка читання файлу: {ex.Message}");
                }
            }

            // Спроба 2: Дефолтний тестовий текст (для демонстрації)
            Console.WriteLine("Використовується вбудований тестовий текст (файл не вказано).");
            return "Server log: Update at 12/05/2023. " +
                   "Host 192.168.0.1 connected. " +
                   "Error at 30/02/2023 (invalid date). " +
                   "Bad IP: 256.0.0.99. " +
                   "Backup server: 10.0.0.5 finished at 13/05/2023.";
        }
    }
}
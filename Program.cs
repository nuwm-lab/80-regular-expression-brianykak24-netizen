using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexLab
{
    // Клас, що інкапсулює логіку пошуку та валідації IP-адрес
    public class IpSearcher
    {
        private const string IpPattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";

        public List<string> FindIpAddresses(string inputText)
        {
            var foundIps = new List<string>();

            if (string.IsNullOrWhiteSpace(inputText))
            {
                return foundIps;
            }

            MatchCollection matches = Regex.Matches(inputText, IpPattern);

            foreach (Match match in matches)
            {
                string candidate = match.Value;
                if (IsValidIp(candidate))
                {
                    foundIps.Add(candidate);
                }
            }

            return foundIps;
        }

        private bool IsValidIp(string ipAddress)
        {
            return System.Net.IPAddress.TryParse(ipAddress, out _);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Лабораторна робота: Регулярні вирази (Варіант 5)";

            // Тестовий текст, що містить як правильні IP, так і неправильні
            string text = "Ось список серверів: 192.168.0.1, ще один 10.0.0.55. " +
                          "Неправильні IP: 256.0.0.1 (завелике число), 1.2.3 (мало октетів), " +
                          "123.456.78.90 (друге число завелике). " +
                          "Локальний хост: 127.0.0.1.";

            Console.WriteLine("--- Вхідний текст ---");
            Console.WriteLine(text);
            Console.WriteLine(new string('-', 30));

            // Створюємо екземпляр нашого класу (використання інкапсуляції)
            IpSearcher searcher = new IpSearcher();
            List<string> results = searcher.FindIpAddresses(text);

            Console.WriteLine($"\nЗнайдено IP-адрес: {results.Count}");

            int counter = 1;
            foreach (var ip in results)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{counter}. {ip}");
                Console.ResetColor();
                counter++;
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
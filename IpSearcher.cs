using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


public class IpSearcher : BaseSearcher
{
    public override string Name => "IP-адреси (IPv4)";

    // Простий патерн + сувора валідація кодом (найбільш надійний варіант)
    protected override string Pattern => @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";

    protected override bool IsValid(string match)
    {
        // Використовуємо вбудований парсер для 100% точності діапазону 0-255
        return IPAddress.TryParse(match, out _);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DateSearcher : BaseSearcher
{
    public override string Name => "Дати (DD/MM/YYYY)";

    // Шукаємо формат 01/01/2000
    protected override string Pattern => @"\b\d{2}/\d{2}/\d{4}\b";

    protected override bool IsValid(string match)
    {
        return DateTime.TryParseExact(match, "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out _);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public abstract class BaseSearcher : IPatternSearcher
{
    public abstract string Name { get; }
    protected abstract string Pattern { get; }

    // Опціональний валідатор (Func<string, bool>)
    protected virtual bool IsValid(string match) => true;

    public IEnumerable<string> Search(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) yield break;

        // Рекомендація: RegexOptions.Compiled для продуктивності
        var options = RegexOptions.Compiled | RegexOptions.ExplicitCapture;
        var regex = new Regex(Pattern, options);

        foreach (Match match in regex.Matches(text))
        {
            if (IsValid(match.Value))
            {
                yield return match.Value; // Рекомендація: yield return замість List
            }
        }
    }
}

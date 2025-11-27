using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IPatternSearcher
{
    string Name { get; }
    IEnumerable<string> Search(string text);
}


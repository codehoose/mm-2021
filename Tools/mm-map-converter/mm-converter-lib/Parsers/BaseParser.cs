using System;
using System.Collections.Generic;
using System.Linq;

namespace ManicMiner.Converter.Lib.Parsers
{
    abstract class BaseParser
    {
        public abstract void Parse(string[] lines);

        protected int[] GetNumbers(string[] lines, string sectionName, int numLines = 1)
        {
            List<int> numbers = new List<int>();
            int index = Array.IndexOf(lines, sectionName);
            if (index > 0)
            {
                for (var i = 0; i < numLines; i++)
                {
                    var line = lines[index + 1 + i];
                    numbers.AddRange(ExtractNumbers(line));
                }
            }

            return numbers.ToArray();
        }

        protected int[] ExtractNumbers(string line)
        {
            return line.Replace("Data\t", "")
                       .Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                       .Select((i) => int.Parse(i))
                       .ToArray();
        }
    }
}

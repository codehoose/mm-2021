using ManicMiner.Converter.Lib.Models;
using System.Collections.Generic;

namespace ManicMiner.Converter.Lib.Parsers
{
    class ExitParser : BaseParser
    {
        public List<MMPoint> Exits { get; } = new List<MMPoint>();

        public override void Parse(string[] lines)
        {
            var xpos = GetNumbers(lines, ".EXITxpos");
            var ypos = GetNumbers(lines, ".EXITypos");

            for (int i = 0; i < 20; i++)
            {
                Exits.Add(new MMPoint
                {
                    x = xpos[i],
                    y = ypos[i]
                });
            }
        }
    }
}

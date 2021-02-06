using ManicMiner.Converter.Lib.Models;
using System.Collections.Generic;

namespace ManicMiner.Converter.Lib.Parsers
{
    class TravelatorParser : BaseParser
    {
        public List<MMTravelator> Travelators { get; } = new List<MMTravelator>();

        public override void Parse(string[] lines)
        {
            var xpos = GetNumbers(lines, ".CONVxpos");
            var ypos = GetNumbers(lines, ".CONVypos");
            var dir = GetNumbers(lines, ".CONVdir");
            var len = GetNumbers(lines, ".CONVlen");

            for (int i = 0; i < 20; i++)
            {
                Travelators.Add(new MMTravelator
                {
                    dir = dir[i],
                    len = len[i],
                    pos = new MMPoint
                    {
                        x = xpos[i],
                        y = ypos[i]
                    }
                });
            }
        }
    }
}

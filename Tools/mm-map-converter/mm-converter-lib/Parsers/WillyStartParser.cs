using ManicMiner.Converter.Lib.Models;
using System.Collections.Generic;

namespace ManicMiner.Converter.Lib.Parsers
{
    class WillyStartParser : BaseParser
    {
        public List<MMWillyStart> WillyStart { get; } = new List<MMWillyStart>();

        public override void Parse(string[] lines)
        {
            var startX = GetNumbers(lines, ".WILLYstartx", 1);
            var startY = GetNumbers(lines, ".WILLYstarty", 1);
            var direction = GetNumbers(lines, ".WILLYstartd", 1);

            for(int i = 0; i < 20; i++)
            {
                WillyStart.Add(new MMWillyStart
                {
                    pos = new MMPoint
                    {
                        x = startX[0],
                        y = startY[0]
                    },
                    dir = direction[i]
                });
            }
        }
    }
}

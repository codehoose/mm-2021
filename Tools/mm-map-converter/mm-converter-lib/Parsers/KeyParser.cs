using ManicMiner.Converter.Lib.Models;
using System.Collections.Generic;

namespace ManicMiner.Converter.Lib.Parsers
{
    class KeyParser : BaseParser
    {
        public List<MMPoint[]> Keys { get; } = new List<MMPoint[]>();

        public override void Parse(string[] lines)
        {
            var xpos = GetNumbers(lines, ".KEYxpos", 20);
            var ypos = GetNumbers(lines, ".KEYypos", 20);
            var stat = GetNumbers(lines, ".KEYstat", 20);

            for (int room = 0; room < 20; room++)
            {
                List<MMPoint> keys = new List<MMPoint>();

                for (int key = 0; key < 5; key++)
                {
                    if (stat[room * 5 + key] == 0)
                        continue;

                    var x = xpos[room * 5 + key];
                    var y = ypos[room * 5 + key];

                    keys.Add(new MMPoint
                    {
                        x = x,
                        y = y
                    });
                }

                Keys.Add(keys.ToArray());
            }
        }
    }
}

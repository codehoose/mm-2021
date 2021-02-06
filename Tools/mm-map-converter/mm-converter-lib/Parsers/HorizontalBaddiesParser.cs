using ManicMiner.Converter.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicMiner.Converter.Lib.Parsers
{
    class HorizontalBaddiesParser : BaseParser
    {
        public List<MMMob[]> Baddies { get; } = new List<MMMob[]>();

        public override void Parse(string[] lines)
        {
            var xpos = GetNumbers(lines, ".HROBOxpos", 20);
            var ypos = GetNumbers(lines, ".HROBOypos", 20);
            var minPos = GetNumbers(lines, ".HROBOminpos", 20);
            var maxPos = GetNumbers(lines, ".HROBOmaxpos", 20);
            var dir = GetNumbers(lines, ".HROBOdir", 20);
            var speed = GetNumbers(lines, ".HROBOspeed", 20);
            var gra = GetNumbers(lines, ".HROBOgra", 20);
            var fli = GetNumbers(lines, ".HROBOfli", 20);
            var ani = GetNumbers(lines, ".HROBOani", 20);
            
            for (int i = 0; i < 20; i++)
            {
                List<MMMob> mobs = new List<MMMob>();
                var rowIndex = i * 4; // There are four possible baddies per row
                for (int col = 0; col < 4; col++)
                {
                    var offset = rowIndex + col;

                    if (xpos[offset] == -1)
                        continue;

                    mobs.Add(new MMMob
                    {
                        ani = ani[offset],
                        dir = dir[offset],
                        fli = fli[offset],
                        graphic=gra[offset],
                        maxPos= maxPos[offset],
                        minPos = minPos[offset],
                        pos = new MMPoint {  x = xpos[offset], y = ypos[offset]},
                        speed = speed[offset]
                    });
                }

                Baddies.Add(mobs.ToArray());
                mobs.Clear();
            }
        }
    }
}

using System;

namespace ManicMiner.Converter.Lib.Models
{
    [Serializable]
    public class MMWillyStart
    {
        public MMPoint pos;
        public int dir;

        public MMWillyStart Copy()
        {
            return new MMWillyStart
            {
                dir = dir,
                pos = new MMPoint { x = pos.x, y = pos.y }
            };
        }
    }
}

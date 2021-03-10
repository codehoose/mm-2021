using System;

namespace ManicMiner.Converter.Lib.Models
{
    [Serializable]
    public class MMTravelator
    {
        public MMPoint pos;
        public int dir;
        public int len;

        public MMTravelator Copy()
        {
            return new MMTravelator
            {
                dir = dir,
                len = len,
                pos = new MMPoint { x = pos.x, y = pos.y }
            };
        }
    }
}

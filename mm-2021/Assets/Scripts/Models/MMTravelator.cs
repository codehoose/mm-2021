using System;

namespace ManicMiner.Converter.Lib.Models
{
    [Serializable]
    public struct MMTravelator
    {
        public MMPoint pos;
        public int dir;
        public int len;
    }
}

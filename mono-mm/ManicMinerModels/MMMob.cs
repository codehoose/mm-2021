using System;

namespace ManicMiner.Converter.Lib.Models
{
    [Serializable]
    public struct MMMob
    {
        public MMPoint pos;
        public int minPos;
        public int maxPos;
        public int dir;
        public int speed;
        public int graphic;
        public int fli;
        public int ani;
    }
}

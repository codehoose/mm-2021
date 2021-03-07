using System;

namespace ManicMiner.Converter.Lib.Models
{
    [Serializable]
    public class MMRoom
    {
        public int[] blocks;
        public string name;
        public int border;
        public MMWillyStart willyStart;
        public MMTravelator travelator;
        public MMPoint[] keys;
        public MMObject[] switches;
        public MMPoint exitPosition;
        public int airCount;
        public MMMob[] horizEnemies;
        public MMMob[] vertEnemies;
    }
}

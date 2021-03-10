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

        public MMRoom Copy()
        {
            var room = new MMRoom();
            if (blocks.Length > 0)
            {
                room.blocks = new int[blocks.Length];
                Array.Copy(blocks, room.blocks, blocks.Length);
            }
            room.name = name;
            room.border = border;
            room.willyStart = willyStart.Copy();
            room.travelator = travelator?.Copy();
            if (keys.Length > 0)
            {
                room.keys = new MMPoint[keys.Length];
                Array.Copy(keys, room.keys, keys.Length);
            }
            if (switches!= null && switches.Length > 0)
            {
                room.switches = new MMObject[switches.Length];
                Array.Copy(switches, room.switches, switches.Length);
            }

            room.exitPosition = new MMPoint { x = exitPosition.x, y = exitPosition.y };
            room.airCount = airCount;
            if (horizEnemies != null && horizEnemies.Length > 0)
            {
                room.horizEnemies = new MMMob[horizEnemies.Length];
                Array.Copy(horizEnemies, room.horizEnemies, horizEnemies.Length);
            }

            if (vertEnemies != null && vertEnemies.Length > 0)
            {
                room.vertEnemies = new MMMob[vertEnemies.Length];
                Array.Copy(vertEnemies, room.vertEnemies, vertEnemies.Length);
            }

            return room;
        }
    }
}

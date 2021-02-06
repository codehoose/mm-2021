using System;
using System.Collections.Generic;

namespace ManicMiner.Converter.Lib.Parsers
{
    /// <summary>
    /// Parse out the room data from the original BlitzBASIC file.
    /// </summary>
    class RoomParser : BaseParser
    {
        public List<int[]> RoomData { get; } = new List<int[]>();
        public List<string> RoomNames { get; } = new List<string>();

        public override void Parse(string[] lines)
        {
            var index = Array.IndexOf(lines, ".LEVELS");
            if (index < 0)
            {
                return;
            }

            // There are 20 rooms
            for (var room = 0; room < 20; room++)
            {
                List<int> blocks = new List<int>();

                // Each room has 16 rows of blocks
                for (var row = 0; row < 16; row++)
                {
                    var line = lines[index + 1 + row + room * 16];
                    blocks.AddRange(ExtractNumbers(line));
                }

                RoomData.Add(blocks.ToArray());
            }

            var nameIndex = Array.IndexOf(lines, ".LEVnames");
            if (nameIndex < 0)
                return;

            for (var i = 0; i < 20; i++)
            {
                var line = lines[nameIndex + 1 + i];
                line = line.Replace("Data	", "").Replace("\"", "");
                RoomNames.Add(line);
            }
        }
    }
}

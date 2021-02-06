using ManicMiner.Converter.Lib.Models;
using ManicMiner.Converter.Lib.Parsers;
using Newtonsoft.Json;

namespace ManicMiner.Converter.Lib
{
    class JsonUtil
    {
        public static string MakeFrom(RoomParser roomParser, WillyStartParser willyStartParser, TravelatorParser travelatorParser, ExitParser exitParser, KeyParser keyParser)
        {
            var file = new MMMapFile
            {
                rooms = new MMRoom[20]
            };

            for (int i = 0; i < 20; i++)
            {
                file.rooms[i] = new MMRoom
                {
                    blocks = roomParser.RoomData[i],
                    name = roomParser.RoomNames[i],
                    exitPosition = exitParser.Exits[i],
                    keys = keyParser.Keys[i],
                    travelator = travelatorParser.Travelators[i],
                    willyStart = willyStartParser.WillyStart[i]
                };
            }

            return JsonConvert.SerializeObject(file);
        }
    }
}
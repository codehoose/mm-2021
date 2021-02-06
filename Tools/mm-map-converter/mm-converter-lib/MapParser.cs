using ManicMiner.Converter.Lib.Parsers;
using System.IO;
using System.Linq;

namespace ManicMiner.Converter.Lib
{
    public class MapParser
    {
        private readonly string _map;

        public MapParser(string map)
        {
            _map = map;
        }

        public string Parse()
        {
            // Read the contents of the file, but ignore lines that start
            // with a semi-colon (;) because those are comments in BlitzBASIC
            var lines = File.ReadLines(_map)
                            .Where((line) => !line.StartsWith(";"))
                            .ToArray();

            // Create the laundry list of parsers
            var roomParser = new RoomParser();
            var willyStartParser = new WillyStartParser();
            var exitParser = new ExitParser();
            var travelatorParser = new TravelatorParser();
            var keyParser = new KeyParser();

            roomParser.Parse(lines);
            willyStartParser.Parse(lines);
            travelatorParser.Parse(lines);
            exitParser.Parse(lines);
            keyParser.Parse(lines);

            return JsonUtil.MakeFrom(roomParser, willyStartParser, travelatorParser, exitParser, keyParser);
            /*
             * Each line:
             *  if it matches a marker; process the number of lines required for that section
             */
        }
    }
}

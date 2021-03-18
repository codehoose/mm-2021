using System.Collections.Generic;

namespace ManicMiner.Converter.Lib.Parsers
{
    class AirParser : BaseParser
    {
        public List<int> AirCount { get; } = new List<int>();
    
        public override void Parse(string[] lines)
        {
            var airCount = GetNumbers(lines, ".AIRcount");
            AirCount.AddRange(airCount);
        }
    }
}

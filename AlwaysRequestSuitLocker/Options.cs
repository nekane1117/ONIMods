using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace AlwaysRequestSuitLocker
{
    [JsonObject]
    [ModInfo("https://github.com/nekane1117/ONIMods/tree/master/AlwaysRequestSuitLocker")]
    public class Options
    {
        [Option("Auto request OxygenMask")]
        public bool OxygenMask { get; set; } = true;

        [Option("Auto request AtmoSuit")]
        public bool AtmoSuit { get; set; } = true;

        [Option("Auto request JetSuit")]
        public bool JetSuit { get; set; } = false;
    }
}
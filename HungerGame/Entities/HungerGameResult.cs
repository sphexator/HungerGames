using System.Collections.Generic;
using System.IO;

namespace HungerGame.Entities
{
    public class HungerGameResult
    {
        public string Content { get; set; }
        public Stream Image { get; set; }
        public IEnumerable<HungerGameProfile> Participants { get; set; }
    }
}
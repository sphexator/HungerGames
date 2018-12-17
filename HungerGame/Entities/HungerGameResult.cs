using System.Collections.Generic;
using System.IO;
using HungerGame.Entities.User;

namespace HungerGame.Entities
{
    public class HungerGameResult
    {
        public string Content { get; internal set; }
        public Stream Image { get; internal set; }
        public IEnumerable<HungerGameProfile> Participants { get; internal set; }
    }
}
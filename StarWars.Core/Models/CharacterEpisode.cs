using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Core.Models
{
    public class CharacterEpisode
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
    }
}

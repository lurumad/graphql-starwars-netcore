using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarWars.Core.Models
{
    public class Episode
    {
        public static Episode NewHope = new Episode(4, "NEWHOPE");
        public static Episode Empire = new Episode(5, "EMPIRE");
        public static Episode Jedi = new Episode(6, "JEDI");

        private Episode()
        {

        }

        private Episode(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Character Hero { get; set; }
        public int HeroId { get; set; }
        public ICollection<CharacterEpisode> CharacterEpisodes { get; set; }

        public static IReadOnlyCollection<Episode> GetEpisodes()
        {
            return new Episode[] { NewHope, Empire, Jedi };
        }

        public static Episode From(int id)
        {
            return GetEpisodes().SingleOrDefault(x => x.Id == id);
        }
    }
}

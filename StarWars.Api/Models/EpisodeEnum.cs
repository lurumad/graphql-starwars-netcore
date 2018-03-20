using GraphQL.Types;
using StarWars.Core.Models;

namespace StarWars.Api.Models
{
    public class EpisodeEnum : EnumerationGraphType
    {
        public EpisodeEnum()
        {
            Name = "Episode";
            Description = "One of the films in the Star Wars Trilogy";

            AddValue(Episode.NewHope.Title, "Released in 1977", Episode.NewHope.Id);
            AddValue(Episode.Empire.Title, "Released in 1980", Episode.Empire.Id);
            AddValue(Episode.Jedi.Title, "Released in 1983", Episode.Jedi.Id);
        }
    }
}

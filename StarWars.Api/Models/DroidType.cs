using AutoMapper;
using GraphQL.Types;
using StarWars.Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.Api.Models
{
    public class DroidType : ObjectGraphType<Droid>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IMapper mapper;

        public DroidType(ICharacterRepository characterRepository, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.mapper = mapper;

            Name = "Droid";
            Description = "A mechanical creature in the Star Wars universe";

            Field(x => x.Id).Description("The id of the Droid.");
            Field(x => x.Name, nullable: true).Description("The name of the Droid");
            FieldAsync<ListGraphType<CharacterInterface>>(
                "friends",
                resolve: async context =>
                {
                    var friends = await characterRepository.GetFriends(context.Source.Id);
                    return mapper.Map<List<Character>>(friends);
                });

            FieldAsync<ListGraphType<EpisodeEnum>>(
                "appearsIn",
                resolve: async context =>
                {
                    var episodes = await characterRepository.GetEpisodes(context.Source.Id);
                    var episodeEnums = episodes.Select(x => (Episodes)x.Id).ToArray();
                    return episodeEnums;
                });
            Field(x => x.PrimaryFunction, nullable: true).Description("The primary function of the Droid");

            Interface<CharacterInterface>();
        }
    }
}

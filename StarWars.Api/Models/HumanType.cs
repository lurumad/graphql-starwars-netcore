using AutoMapper;
using GraphQL.Types;
using StarWars.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Api.Models
{
    public class HumanType : ObjectGraphType<Human>
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IMapper mapper;

        public HumanType(ICharacterRepository characterRepository, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.mapper = mapper;

            Name = "Human";

            Field(x => x.Id).Description("The id of the human");
            Field(x => x.Name, nullable: true).Description("The name of the human");

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

            Field(x => x.HomePlanet, nullable: true).Description("The home planet of the human");

            Interface<CharacterInterface>();
        }
    }
}

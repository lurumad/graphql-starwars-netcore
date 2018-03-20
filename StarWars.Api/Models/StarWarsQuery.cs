using AutoMapper;
using GraphQL.Types;
using StarWars.Core.Data;
using StarWars.Core.Models;

namespace StarWars.Api.Models
{
    public class StarWarsQuery : ObjectGraphType
    {
        private const int R2D2 = 2001;
        private readonly ICharacterRepository characterRepository;
        private readonly IMapper mapper;

        public StarWarsQuery(ICharacterRepository characterRepository, IMapper mapper)
        {
            this.characterRepository = characterRepository;
            this.mapper = mapper;
            FieldAsync<CharacterInterface>(
                "hero",
                arguments: new QueryArguments(
                    new QueryArgument<EpisodeEnum> { Name = "episode", Description = "If omitted, returns the hero of the whole saga. If provided, returns the hero of that particular episode." }
                ),
                resolve: async context =>
                {
                    var episode = context.GetArgument<Episodes?>("episode");
                    if (episode.HasValue)
                    {
                        var hero = await characterRepository.GetHero(Episode.From((int)episode.Value));
                        return mapper.Map<Character>(hero);
                    }

                    var droid = await characterRepository.GetDroid(R2D2);
                    return mapper.Map<Character>(droid);
                }
            );

            FieldAsync<DroidType>(
                "droid",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }
                ),
                resolve: async context => 
                {
                    var id = context.GetArgument<int>("id");
                    var droid = await characterRepository.GetDroid(id);
                    return mapper.Map<Droid>(droid);
                }

            );

            FieldAsync<HumanType>(
                "human",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the human" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var human = await characterRepository.GetHuman(id);
                    return mapper.Map<Human>(human);
                }
            );
        }
    }
}

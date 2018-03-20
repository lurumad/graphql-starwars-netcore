using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Api.Models
{
    public class CharacterInterface : InterfaceGraphType<Character>
    {
        public CharacterInterface()
        {
            Name = "Character";

            Field(x => x.Id).Description("The id of the character");
            Field(x => x.Name, nullable:true).Description("The name of the character");

            Field<ListGraphType<CharacterInterface>>("friends");
            Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie the appear in.");
        }
    }
}

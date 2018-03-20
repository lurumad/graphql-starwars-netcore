using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Api.Models
{
    public class StarWarsSchema : Schema
    {
        public StarWarsSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            Query = (StarWarsQuery)resolveType(typeof(StarWarsQuery));
        }
    }
}

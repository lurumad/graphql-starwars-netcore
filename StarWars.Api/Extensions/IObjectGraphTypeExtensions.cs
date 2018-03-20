using GraphQL.Resolvers;
using System;
using System.Threading.Tasks;

namespace GraphQL.Types
{
    public static class IObjectGraphTypeExtensions
    {
        public static FieldType FieldAsync<TGraphype, TSourceType>(
            this IObjectGraphType obj,
            string name,
            string description,
            QueryArguments arguments = null,
            Func<ResolveFieldContext<TSourceType>, Task<object>> resolve = null,
            string deprecationReason = null) where TGraphype : IGraphType
        {
            return obj.AddField(
                new FieldType
                {
                    Name = name,
                    Description = description,
                    DeprecationReason = deprecationReason,
                    Type = typeof(TGraphype),
                    Arguments = arguments,
                    Resolver = resolve != null ? new FuncFieldResolver<TSourceType, Task<object>>(resolve) : null
                });
        }
    }
}

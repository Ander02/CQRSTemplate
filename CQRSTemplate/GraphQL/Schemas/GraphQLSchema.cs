using CQRSTemplate.GraphQL.Query;
using GraphQL.Types;
using System;

namespace CQRSTemplate.GraphQL.Schemas
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(Func<Type, GraphType> resolve) : base(resolve)
        {
            Query = (GraphQLQuery)resolve(typeof(GraphQLQuery));
        }
    }
}

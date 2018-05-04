using CQRSTemplate.GraphQL.Root;
using GraphQL.Types;
using System;

namespace CQRSTemplate.GraphQL
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(Func<Type, GraphType> resolve) : base(resolve)
        {
            //Roots
            Query = (RootQuery)resolve(typeof(RootQuery));
            Mutation = (RootMutation)resolve(typeof(RootMutation));
        }
    }
}

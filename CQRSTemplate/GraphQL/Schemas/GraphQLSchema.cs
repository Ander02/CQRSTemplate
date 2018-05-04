using CQRSTemplate.GraphQL.InputType;
using CQRSTemplate.GraphQL.Root;
using CQRSTemplate.GraphQL.Types;
using GraphQL.Types;
using System;

namespace CQRSTemplate.GraphQL.Schemas
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

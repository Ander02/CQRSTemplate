using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSTemplate.Features.GraphQL.Query;

namespace CQRSTemplate.Features.GraphQL.Schemas
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(Func<Type, GraphType> resolve) : base(resolve)
        {
            Query = (GraphQLQuery)resolve(typeof(GraphQLQuery));
        }
    }
}

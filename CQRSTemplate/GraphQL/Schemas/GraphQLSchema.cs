using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSTemplate.GraphQL.Query;

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

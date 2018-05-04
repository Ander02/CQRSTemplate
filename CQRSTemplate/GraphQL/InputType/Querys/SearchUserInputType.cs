using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.InputType.Querys
{
    public class SearchUserInputType : InputObjectGraphType
    {
        public SearchUserInputType()
        {
            this.Name = "SearchUserInputType";
            Field<IntGraphType>("Limit");
            Field<IntGraphType>("Page");
            Field<IntGraphType>("Age");
            Field<StringGraphType>("Name");
        }
    }
}

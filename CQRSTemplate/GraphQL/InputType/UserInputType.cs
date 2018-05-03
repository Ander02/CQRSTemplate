using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.InputType
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            this.Name = "UserInputType";
            Field<NonNullGraphType<StringGraphType>>("Name");
            Field<NonNullGraphType<IntGraphType>>("Age");
            Field<NonNullGraphType<StringGraphType>>("Email");
        }
    }
}

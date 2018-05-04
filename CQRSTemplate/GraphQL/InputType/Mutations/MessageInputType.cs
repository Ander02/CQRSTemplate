using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.InputType.Mutations
{
    public class MessageInputType : InputObjectGraphType
    {
        public MessageInputType()
        {
            this.Name = "MessageInputType";
            Field<NonNullGraphType<IdGraphType>>("UserId");
            Field<NonNullGraphType<StringGraphType>>("Title");
            Field<NonNullGraphType<StringGraphType>>("Content");
        }
    }
}

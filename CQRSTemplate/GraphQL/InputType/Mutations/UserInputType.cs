using GraphQL.Types;

namespace CQRSTemplate.GraphQL.InputType.Mutations
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

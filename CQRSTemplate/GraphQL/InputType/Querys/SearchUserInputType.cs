using GraphQL.Types;

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

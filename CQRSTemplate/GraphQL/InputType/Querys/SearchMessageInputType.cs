using GraphQL.Types;

namespace CQRSTemplate.GraphQL.InputType.Querys
{
    public class SearchMessageInputType : InputObjectGraphType
    {
        public SearchMessageInputType()
        {
            this.Name = "SearchMessageInputType";
            Field<IntGraphType>("Limit");
            Field<IntGraphType>("Page");
            Field<IdGraphType>("UserId");
            Field<StringGraphType>("Title");
            Field<StringGraphType>("Content");
        }
    }
}

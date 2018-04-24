using CQRSTemplate.Features.User;
using CQRSTemplate.GraphQL.Types;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Query
{
    public class GraphQLQuery : ObjectGraphType
    {
        public GraphQLQuery() { }

        public GraphQLQuery(IMediator mediator)
        {
            Name = "Query";

            #region Users
            Field<ListGraphType<UserType>>(

                name: "Users",
                description: "The system's users",
                resolve: (context) =>
                {
                    return mediator.Send(new SearchMany.Query() { }).Result;
                });

            Field<UserType>(
                name: "User",
                description: "A system user",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "id of the human"
                    }
                ),
                resolve: (context) =>
                {
                    try
                    {
                        var userId = context.GetArgument<Guid>("id");
                        return mediator.Send(new SearchOne.Query()
                        {
                            Id = userId
                        }).Result;
                    }
                    catch
                    {
                        return null;
                    }
                });
            #endregion
        }
    }
}
using CQRSTemplate.GraphQL.InputType.Querys;
using CQRSTemplate.GraphQL.Types;
using GraphQL.Types;
using MediatR;
using System;

namespace CQRSTemplate.GraphQL.Root
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery() { }

        public RootQuery(IMediator mediator)
        {
            this.Name = "Query";

            #region Users Querys
            //Search Many Users
            FieldAsync<ListGraphType<UserType>>(

                name: "Users",
                description: "The system's users",
                arguments: new QueryArguments()
                {
                    new QueryArgument<SearchUserInputType>()
                    {
                        Name = "Params",
                        Description = "Parameters of search"
                    }
                },
                resolve: async (context) =>
                {
                    var query = context.GetArgument<Features.User.SearchMany.Query>("Params");

                    if (query != null) return await mediator.Send(query);

                    else return await mediator.Send(new Features.User.SearchMany.Query());
                });

            //Search One User
            FieldAsync<UserType>(
                name: "User",
                description: "A system user",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType>
                    {
                        Name = "Id",
                        Description = "The user id"
                    }
                ),
                resolve: async (context) =>
                {
                    var userId = context.GetArgument<Guid>("Id");
                    return await mediator.Send(new Features.User.SearchOne.Query()
                    {
                        Id = userId
                    });

                });
            #endregion

            #region Messages Querys
            //Search Many Messages
            FieldAsync<ListGraphType<MessageType>>(

                name: "Messages",
                description: "The system's messages",
                arguments: new QueryArguments()
                {
                    new QueryArgument<SearchUserInputType>
                    {
                        Name = "Params",
                        Description = "The search message params"
                    }
                },
                resolve: async (context) =>
                {
                    /*
                    var userId = context.GetArgument<Guid?>("userId");
                    var title = context.GetArgument<string>("title");
                    var content = context.GetArgument<string>("content");
                    var limit = context.GetArgument<int?>("limit");
                    var page = context.GetArgument<int?>("page");
                    */

                    /*
                    query.UserId = userId ?? query.UserId;
                    query.Title = title ?? query.Title;
                    query.Content = content ?? query.Content;
                    query.Limit = limit ?? query.Limit;
                    query.Page = page ?? query.Page;
                    */

                    var query = context.GetArgument<Features.Message.SearchMany.Query>("Params");

                    if (query != null) return await mediator.Send(query);

                    else return await mediator.Send(new Features.Message.SearchMany.Query());
                });
            #endregion
        }
    }
}
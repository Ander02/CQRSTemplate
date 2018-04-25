using CQRSTemplate.Features;
using CQRSTemplate.GraphQL.Types;
using GraphQL.Types;
using MediatR;
using System;

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
                arguments: new QueryArguments()
                {
                    new QueryArgument<IntGraphType>
                    {
                        Name = "age",
                        Description = "users age"
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "name",
                        Description = "users name"
                    },
                    new QueryArgument<IntGraphType>
                    {
                        Name = "limit",
                        Description = "limit of users"
                    },
                    new QueryArgument<IntGraphType>
                    {
                        Name = "page",
                        Description = "data page"
                    },
                },
                resolve: (context) =>
                {
                    try
                    {
                        var age = context.GetArgument<int?>("age");
                        var name = context.GetArgument<String>("name");
                        var limit = context.GetArgument<int?>("limit");
                        var page = context.GetArgument<int?>("page");

                        var query = new Features.User.SearchMany.Query();
                        query.Age = age ?? query.Age;
                        query.Name = name ?? query.Name;
                        query.Limit = limit ?? query.Limit;
                        query.Page = page ?? query.Page;

                        return mediator.Send(query).Result;
                    }
                    catch { return null; }
                });

            Field<UserType>(
                name: "User",
                description: "A system user",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType>
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
                        return mediator.Send(new Features.User.SearchOne.Query()
                        {
                            Id = userId
                        }).Result;
                    }
                    catch { return null; }
                });
            #endregion

            #region Message
            Field<ListGraphType<MessageType>>(

                name: "Messages",
                description: "The system's messages",
                arguments: new QueryArguments()
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = "userId",
                        Description = "the user id"
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "title",
                        Description = "the message title"
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "content",
                        Description = "The message content"
                    },
                    new QueryArgument<IntGraphType>
                    {
                        Name = "limit",
                        Description = "limit of messages"
                    },
                    new QueryArgument<IntGraphType>
                    {
                        Name = "page",
                        Description = "data page"
                    }
                },
                resolve: (context) =>
                {
                    try
                    {
                        var userId = context.GetArgument<Guid?>("userId");
                        var title = context.GetArgument<string>("title");
                        var content = context.GetArgument<string>("content");
                        var limit = context.GetArgument<int?>("limit");
                        var page = context.GetArgument<int?>("page");

                        var query = new Features.Message.SearchMany.Query();
                        query.UserId = userId ?? query.UserId;
                        query.Title = title ?? query.Title;
                        query.Content = content ?? query.Content;
                        query.Limit = limit ?? query.Limit;
                        query.Page = page ?? query.Page;

                        return mediator.Send(query).Result;
                    }
                    catch { return null; }
                });
            #endregion
        }
    }
}
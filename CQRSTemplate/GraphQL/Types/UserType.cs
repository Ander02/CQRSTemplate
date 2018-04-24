using CQRSTemplate.Features.User;
using GraphQL.Types;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Types
{
    public class UserType : ObjectGraphType<UserViews.FullResult>
    {
        public UserType(IMediator mediator)
        {
            Name = "User";
            Description = "User Fields";

            Field(u => u.Id, type: typeof(IdGraphType)).Description("The user id");
            Field(u => u.Name).Description("The user name");
            Field(u => u.Age).Description("The user age");
            Field(u => u.Email).Description("The user email");

            Field<ListGraphType<MessageType>>(
                name: "Messages",
                description: "The user messages",
                resolve: (context) =>
                {
                    var userId = context.Source.Id;
                    return mediator.Send(new Features.Message.SearchMany.Query()
                    {
                        UserId = userId
                    }).Result;
                });
        }
    }
}

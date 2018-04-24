using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Domain;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(IMessageRepository messageRepository)
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
                    var q = messageRepository.GetEntityQuery();
                    return messageRepository.QueryFindByUserId(q, userId).ToListAsync().Result;
                });
        }
    }
}

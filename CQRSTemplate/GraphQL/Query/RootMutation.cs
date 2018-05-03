using CQRSTemplate.Domain;
using CQRSTemplate.Features.User;
using CQRSTemplate.GraphQL.InputType;
using CQRSTemplate.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Query
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation(IMediator mediator)
        {
            this.Name = "Mutation";
            this.Description = "The system root mutation";

            #region User
            FieldAsync<UserType>(
                name: "addUser",
                description: "This mutation add a user",
                arguments: new QueryArguments
                {
                    new QueryArgument<UserInputType>()
                    {
                        Name = "Input",
                        Description = "A system user"
                    }
                },
                resolve: async (context) =>
               {
                   try
                   {
                       var input = context.GetArgument<UserViews.SimpleResult>("Input");

                       Register.Command command = new Register.Command()
                       {
                           Age = input.Age,
                           Email = input.Email,
                           Name = input.Name
                       };

                       return await mediator.Send(command);
                   }
                   catch (Exception ex)
                   {
                       context.Errors.Add(new ExecutionError(ex.Message, ex));
                       return null;
                   }
               });
            #endregion
        }
    }
}

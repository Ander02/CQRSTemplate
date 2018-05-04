using CQRSTemplate.Features.User;
using CQRSTemplate.GraphQL.InputType;
using CQRSTemplate.GraphQL.InputType.Mutations;
using CQRSTemplate.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Root
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation(IMediator mediator)
        {
            this.Name = "Mutation";
            this.Description = "The system root mutation";

            #region Users Mutations
            //Add User
            FieldAsync<UserType>(
                name: "addUser",
                description: "This mutation add a user",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<UserInputType>>()
                    {
                        Name = "Input",
                        Description = "A system user"
                    }
                },
                resolve: async (context) =>
                {
                    var input = context.GetArgument<Register.Command>("Input");

                    /*
                    Register.Command command = new Register.Command()
                    {
                        Age = input.Age,
                        Email = input.Email,
                        Name = input.Name
                    };
                    */

                    var result = await mediator.Send(input);

                    return result;
                });

            FieldAsync<BooleanGraphType>(
                name: "removeUser",
                description: "This mutation add a user",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "Id",
                        Description = "A user id"
                    }
                },
                resolve: async (context) =>
                {
                    var id = context.GetArgument<Guid>("Id");

                    var command = new Delete.Command()
                    {
                        Id = id
                    };

                    await mediator.Send(command);

                    return true;
                });
            #endregion
        }
    }
}

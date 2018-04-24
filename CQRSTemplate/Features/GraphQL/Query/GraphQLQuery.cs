using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Features.GraphQL.Types;
using CQRSTemplate.Infraestructure.Exceptions;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Features.GraphQL.Query
{
    public class GraphQLQuery : ObjectGraphType
    {
        public GraphQLQuery() { }

        public GraphQLQuery(IUserRepository userRepository)
        {
            Name = "Query";

            #region Users
            Field<ListGraphType<UserType>>(

                name: "Users",
                description: "The system's users",
                resolve: (context) =>
                {
                    return userRepository.FindAllAsync().Result;
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
                        return userRepository.FindByIdAsync(userId).Result;
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
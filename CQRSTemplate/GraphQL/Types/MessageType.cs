using CQRSTemplate.Features.Message;
using GraphQL.Types;
using MediatR;

namespace CQRSTemplate.GraphQL.Types
{
    public class MessageType : ObjectGraphType<MessageViews.FullResult>
    {
        public MessageType(IMediator mediator)
        {
            Field(m => m.Id, type: typeof(IdGraphType)).Description("The message id");
            Field(m => m.Title).Description("The message title");
            Field(m => m.Content).Description("The message content");

            FieldAsync<UserType>(
                name: "User",
                description: "The user with send the message",
                resolve: async (context) =>
                {
                    try
                    {
                        return await mediator.Send(new Features.User.SearchOne.Query()
                        {
                            Id = context.Source.User.Id
                        });
                    }
                    catch { return null; }
                });
        }
    }
}

﻿using CQRSTemplate.Domain;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.GraphQL.Types
{
    public class MessageType : ObjectGraphType<Message>
    {
        public MessageType()
        {
            Field(m => m.Id, type: typeof(IdGraphType)).Description("The message id");
            Field(m => m.Title).Description("The message title");
            Field(m => m.Content).Description("The message content");
        }
    }
}
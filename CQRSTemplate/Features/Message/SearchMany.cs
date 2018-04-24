﻿using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CQRSTemplate.Infraestructure;
using CQRSTemplate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSTemplate.Database.Repository.Interface;

namespace CQRSTemplate.Features.Message
{
    public class SearchMany
    {
        public class Query : IRequest<List<MessageViews.FullResult>>
        {
            public string Title { get; set; } = "";
            public string Content { get; set; } = "";
            public Guid UserId { get; set; }
            public int Limit { get; set; } = 100;
            public int Page { get; set; } = 0;
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(u => u.Limit).GreaterThanOrEqualTo(1);
                RuleFor(u => u.Page).GreaterThanOrEqualTo(0);
            }
        }

        public class Handler : AsyncRequestHandler<Query, List<MessageViews.FullResult>>
        {
            private readonly IMessageRepository messageRepository;

            public Handler(IMessageRepository messageRepository)
            {
                this.messageRepository = messageRepository;
            }

            protected override async Task<List<MessageViews.FullResult>> HandleCore(Query query)
            {
                var q = messageRepository.GetEntityQuery();

                if (query.Title != null && query.Title.NotEquals("")) q = messageRepository.QueryFindByTitle(q, query.Title);

                if (query.Title != null && query.Content.NotEquals("")) q = messageRepository.QueryFindByContent(q, query.Content);

                if (query.UserId != null) q = messageRepository.QueryFindByUserId(q, query.UserId);

                q = q.OrderBy(m => m.Title);
                q = q.Skip(query.Limit * query.Page).Take(query.Limit);

                return (await q.ToListAsync()).Select(m => new MessageViews.FullResult(m)).ToList();
            }
        }
    }
}
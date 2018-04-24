﻿using CQRSTemplate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Database.Repository.Interface
{
    public interface IMessageRepository : IBaseRepository<Message, Guid>
    {
        IQueryable<Message> QueryFindByTitle(IQueryable<Message> query, string title);

        IQueryable<Message> QueryFindByContent(IQueryable<Message> query, string content);

        IQueryable<Message> QueryFindByUserId(IQueryable<Message> query, Guid userId);


    }
}

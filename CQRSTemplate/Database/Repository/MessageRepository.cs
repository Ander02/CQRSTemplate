using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Database.Repository
{
    public class MessageRepository : BaseRepository<Message, Guid>, IMessageRepository
    {
        public MessageRepository(Db db) : base(db) { }
    }
}

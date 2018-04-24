using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Database.Repository
{
    public class MessageRepository : BaseRepository<Message, Guid>, IMessageRepository
    {
        public MessageRepository(Db db) : base(db) { }

        public override Message FindById(Guid id) => this.GetEntityQuery().Where(m => m.Id.Equals(id)).FirstOrDefault();

        public override async Task<Message> FindByIdAsync(Guid id) => await this.GetEntityQuery().Where(m => m.Id.Equals(id)).FirstOrDefaultAsync();

        public override List<Message> FindAll() => this.GetEntityQuery().ToList();

        public override async Task<List<Message>> FindAllAsync() => await this.GetEntityQuery().ToListAsync();

        public override IQueryable<Message> GetEntityQuery() => db.Messages.Include(message => message.User).AsQueryable();

        public IQueryable<Message> QueryFindByTitle(IQueryable<Message> query, string title)
        {
            return query.Where(message => message.Title.Contains(title)).AsQueryable();
        }

        public IQueryable<Message> QueryFindByUserId(IQueryable<Message> query, Guid userId)
        {
            return query.Where(message => message.UserId.Equals(userId)).AsQueryable();
        }

        public IQueryable<Message> QueryFindByContent(IQueryable<Message> query, string content)
        {
            return query.Where(message => message.Content.Contains(content)).AsQueryable();
        }
    }
}

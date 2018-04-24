using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Database.Repository
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        public UserRepository(Db db) : base(db) { }

        public override User FindById(Guid id) => this.GetEntityQuery().Where(u => u.Id.Equals(id)).FirstOrDefault();

        public override async Task<User> FindByIdAsync(Guid id) => await this.GetEntityQuery().Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();
        
        public override List<User> FindAll() => this.GetEntityQuery().ToList();

        public override async Task<List<User>> FindAllAsync() => await this.GetEntityQuery().ToListAsync();

        public override IQueryable<User> GetEntityQuery() => db.Users.Include(user => user.Messages).AsQueryable();

        public List<User> FindByName(string name)
        {
            return this.QueryFindByName(this.GetEntityQuery(), name).ToList();
        }

        public async Task<List<User>> FindByNameAsync(string name)
        {
            return await this.QueryFindByName(this.GetEntityQuery(), name).ToListAsync();
        }

        public List<User> FindByAge(int age)
        {
            return this.QueryFindByAge(this.GetEntityQuery(), age).ToList();
        }

        public async Task<List<User>> FindByAgeAsync(int age)
        {
            return await this.QueryFindByAge(this.GetEntityQuery(), age).ToListAsync();
        }

        public IQueryable<User> QueryFindByName(IQueryable<User> query, string name)
        {
            return query.Where(u => u.Name.Equals(name)).AsQueryable();
        }

        public IQueryable<User> QueryFindByAge(IQueryable<User> query, int age)
        {
            return query.Where(u => u.Age == age).AsQueryable();
        }
    }
}

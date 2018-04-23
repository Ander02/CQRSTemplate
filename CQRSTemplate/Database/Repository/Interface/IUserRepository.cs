using CQRSTemplate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Database.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        List<User> FindByName(string name);
        Task<List<User>> FindByNameAsync(string name);

        List<User> FindByAge(int age);
        Task<List<User>> FindByAgeAsync(int age);

        IQueryable<User> QueryFindByName(IQueryable<User> query, string name);
        IQueryable<User> QueryFindByAge(IQueryable<User> query, int age);
    }
}

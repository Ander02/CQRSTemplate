using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Database.Repository.Interface
{
    public interface IBaseRepository<TEntity, in TKey>
    {
        List<TEntity> FindAll();

        Task<List<TEntity>> FindAllAsync();

        TEntity FindById(TKey id);
        Task<TEntity> FindByIdAsync(TKey id);

        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);

        void Remove(TKey id);
        void Remove(TEntity entity);

        bool SaveChanges();
        Task<bool> SaveChangesAsync();

        void Update(TEntity entity);

        IQueryable<TEntity> GetEntityQuery();
    }
}

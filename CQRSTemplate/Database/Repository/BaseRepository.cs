using CQRSTemplate.Database.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSTemplate.Database.Repository
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly Db db;

        protected BaseRepository(Db db)
        {
            this.db = db;
        }

        public virtual TEntity Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await db.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual void Remove(TKey id)
        {
            var entity = this.FindById(id);

            if (entity == null) throw new ArgumentNullException("Null entity");

            db.Set<TEntity>().Remove(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        public virtual List<TEntity> FindAll() => db.Set<TEntity>().ToList();

        public virtual Task<List<TEntity>> FindAllAsync()
        {
            return db.Set<TEntity>().ToListAsync();
        }

        public virtual TEntity FindById(TKey id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public virtual Task<TEntity> FindByIdAsync(TKey id)
        {
            return db.Set<TEntity>().FindAsync(id);
        }

        public virtual bool SaveChanges()
        {
            return db.SaveChanges() > 0;
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return (await db.SaveChangesAsync()) > 0;
        }

        public virtual void Update(TEntity entity)
        {
            db.Set<TEntity>().Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<TEntity> GetEntityQuery()
        {
            return db.Set<TEntity>().AsQueryable();
        }
    }
}

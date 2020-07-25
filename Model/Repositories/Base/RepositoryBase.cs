using Microsoft.EntityFrameworkCore;
using Model.DatabaseContext;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Model.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ClientDbContext _clientDbContext { get; set; }

        public RepositoryBase(ClientDbContext clientDbContext)
        {
            this._clientDbContext = clientDbContext;
        }

        public IQueryable<T> FindAll()
        {
            return this._clientDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._clientDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this._clientDbContext.Set<T>().Add(entity);
            this._clientDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            this._clientDbContext.Set<T>().Update(entity);
            this._clientDbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            this._clientDbContext.Set<T>().Remove(entity);
            this._clientDbContext.SaveChanges();
        }
    }
}

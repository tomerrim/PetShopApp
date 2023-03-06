using Microsoft.EntityFrameworkCore;
using PetShop.DataAccess.Data.Repository.IRepository;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PetDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(PetDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        void IRepository<T>.Update(T entity)
        {
            dbSet.Update(entity);
        }

        void IRepository<T>.Add(T entity)
        { 
            dbSet.Add(entity);  
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        T IRepository<T>.GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        void IRepository<T>.Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}

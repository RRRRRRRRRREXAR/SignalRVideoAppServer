﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

using AppContext = VideoAppDAL.DB.AppContext;

namespace VideoAppDAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppContext db;
        readonly DbSet<T> dbSet;
        public Repository(AppContext context)
        {
            db = context;
            dbSet = context.Set<T>();
        }

        public async Task Create(T item)
        {
            await dbSet.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            T item = await dbSet.FindAsync(id);
            if (item != null)
            {
                dbSet.Remove(item);
            }
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.SingleOrDefaultAsync(predicate);
        }
        public async Task<T> FindAsNoTracking(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.AsNoTracking().SingleOrDefaultAsync(predicate);
        }
        public async Task<List<T>> FindMany(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.Where(predicate).ToListAsync();
        }
        public async Task<T> Get(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }


        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            IEnumerable<T> entities = await query.ToListAsync();
            return entities;
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}

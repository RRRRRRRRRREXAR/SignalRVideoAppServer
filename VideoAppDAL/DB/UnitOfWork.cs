using System;
using System.Collections.Generic;
using System.Text;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;
using VideoAppDAL.Repositories;

namespace VideoAppDAL.DB
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppContext db;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        private bool disposed = false;
        public UnitOfWork(AppContext context)
        {
            this.db = context;
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories.ContainsKey(typeof(T)) == true)
            {
                return repositories[typeof(T)] as Repository<T>;
            }

            IRepository<T> repo = new Repository<T>(db);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposed)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

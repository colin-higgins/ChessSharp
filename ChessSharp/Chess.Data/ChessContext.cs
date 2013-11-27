using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Chess.Data
{
    public class ChessContext : DbContext, IUnitOfWork
    {
        public ChessContext()
            : base("name=ChessContext")
        {

        }

        public T Find<T>(params object[] keyValues) where T : class, IEntity
        {
            return Set<T>().Find(keyValues);
        }

        public T Find<T>(Func<T, bool> predicate) where T : class, IEntity
        {
            return Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> All<T>() where T : class, IEntity
        {
            return Set<T>();
        }

        public IEnumerable<T> All<T>(Func<T, bool> predicate) where T : class, IEntity
        {
            return Set<T>().Where(predicate);
        }

        public void Remove<T>(T entity) where T : class, IModifiable
        {
            Set<T>().Remove(entity);
        }

        public void Remove<T>(IEnumerable<T> entities) where T : class, IModifiable
        {
            foreach (var entity in entities)
                Set<T>().Remove(entity);
        }

        public void Add<T>(T entity) where T : class, IModifiable
        {
            Set<T>().Add(entity);
        }

        public void Attach<T>(T entity) where T : class, IModifiable
        {
            Set<T>().Attach(entity);
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}

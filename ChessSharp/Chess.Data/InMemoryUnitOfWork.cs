using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Chess.Data
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly SetCollection _sets;

        public InMemoryUnitOfWork()
        {
            _sets = new SetCollection();
        }

        public int CommitCount { get; private set; }

        public T Find<T>(params object[] keyValues) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public T Find<T>(Func<T, bool> predicate) where T : class, IEntity
        {
            return _sets.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> All<T>() where T : class, IEntity
        {
            return _sets.Set<T>();
        }

        public IEnumerable<T> All<T>(Func<T, bool> predicate) where T : class, IEntity
        {
            return _sets.Set<T>().Where(predicate);
        }

        public void Remove<T>(T entity) where T : class, IModifiable
        {
            _sets.Set<T>().Remove(entity);
        }

        public void Remove<T>(IEnumerable<T> entities) where T : class, IModifiable
        {
            foreach (var entity in entities)
                _sets.Set<T>().Remove(entity);
        }

        public void Add<T>(T entity) where T : class, IModifiable
        {
            _sets.Set<T>().Add(entity);
        }

        public void Attach<T>(T entity) where T : class, IModifiable
        {
            _sets.Set<T>().Add(entity);
        }

        public void Commit()
        {
            CommitCount++;
        }
    }

    internal class InMemoryDbSet<T> : IDbSet<T> where T : class
    {
        private readonly HashSet<T> set;
        private readonly IQueryable<T> queryableSet;

        public InMemoryDbSet()
            : this(Enumerable.Empty<T>())
        {

        }

        public InMemoryDbSet(IEnumerable<T> entities)
        {
            set = new HashSet<T>();
            foreach (var entity in entities)
            {
                set.Add(entity);
            }
            queryableSet = set.AsQueryable();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Add(T entity)
        {
            set.Add(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            set.Remove(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            set.Add(entity);
            return entity;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local { get { throw new NotImplementedException(); } }

        public void DeleteObject(T entity)
        {
            set.Remove(entity);
        }

        public void Detach(T entity)
        {
            set.Remove(entity);
        }

        public Type ElementType
        {
            get { return queryableSet.ElementType; }
        }

        public Expression Expression
        {
            get { return queryableSet.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return queryableSet.Provider; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
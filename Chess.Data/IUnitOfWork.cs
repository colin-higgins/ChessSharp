using System;
using System.Collections.Generic;

namespace Chess.Data
{
    public interface IUnitOfWork
    {
        T Find<T>(params object[] keyValues) where T : class, IEntity;

        T Find<T>(Func<T, bool> predicate) where T : class, IEntity;

        IEnumerable<T> All<T>() where T : class, IEntity;

        IEnumerable<T> All<T>(Func<T, bool> predicate) where T : class, IEntity;

        void Remove<T>(T entity) where T : class, IModifiable;

        void Remove<T>(IEnumerable<T> entities) where T : class, IModifiable;

        void Add<T>(T entity) where T : class, IModifiable;

        void Attach<T>(T entity) where T : class, IModifiable;

        bool Exists<T>(T entity) where T : class, IModifiable;

        void Commit();
    }

    // Marker Interfaces
    public interface IEntity { }

    public interface IModifiable : IEntity { }
}
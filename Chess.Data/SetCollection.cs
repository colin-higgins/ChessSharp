using System;
using System.Collections.Generic;

namespace Chess.Data
{
    internal class SetCollection
    {
        private readonly Dictionary<Type, object> _sets;

        public SetCollection()
        {
            _sets = new Dictionary<Type, object>();
        }

        public InMemoryDbSet<T> Set<T>() where T : class
        {
            if (!_sets.ContainsKey(typeof(T)))
                _sets.Add(typeof(T), new InMemoryDbSet<T>());

            return (InMemoryDbSet<T>)_sets[typeof(T)];
        }
    }
}
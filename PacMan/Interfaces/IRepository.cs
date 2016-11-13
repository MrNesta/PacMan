using System;
using System.Collections.Generic;

namespace PacMan.Interfaces
{
    public interface IRepository<T>: IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Delete(int id);
    }
}

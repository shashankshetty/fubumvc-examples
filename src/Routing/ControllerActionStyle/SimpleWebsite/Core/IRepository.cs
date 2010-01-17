using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleWebsite.Core
{
    public interface IRepository
    {
        T Find<T>(int id) where T : IEntity;
        void Save(object item);
        IQueryable<T> Query<T>();
        void Delete(object item);
    }

    public class FakeRepository : IRepository
    {
        private static readonly HashSet<object> _items = new HashSet<object>();
        static FakeRepository()
        {
            _items.Add(new Movie {Title = "Raiders of the Lost Ark"});
            _items.Add(new Movie {Title = "The Empire Strikes Back"});
        }

        public T Find<T>(int id) where T:IEntity
        {
            return _items.OfType<T>().FirstOrDefault(item => item.Id == id);
        }

        public void Save(object item)
        {
            _items.Add(item);
        }

        public IQueryable<T> Query<T>()
        {
            return _items.OfType<T>().AsQueryable();
        }

        public void Delete(object item)
        {
            _items.Remove(item);
        }
    }
}
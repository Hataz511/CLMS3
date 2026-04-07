using CLMS.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CLMS.Data.Repositories
{
    public class DatabaseRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _data = new();

        public IEnumerable<T> GetAll() => _data;

        public T GetById(int id)
        {
            var prop = typeof(T).GetProperty("Id");
            return _data.FirstOrDefault(x => (int)prop.GetValue(x) == id);
        }

        public void Add(T entity)
        {
            _data.Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            var prop = typeof(T).GetProperty("Id");
            var id = (int)prop.GetValue(entity);

            var existing = GetById(id);
            if (existing != null)
            {
                _data.Remove(existing);
                _data.Add(entity);
                Save();
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _data.Remove(entity);
                Save();
            }
        }

        public void Save()
        {
            // Simulim database
        }
    }
}
using CLMS.Data.Interfaces;
using CLMS.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CLMS.Data.Repositories
{
    public class RequestRepository : IRepository<Request>
    {
        private readonly string _filePath = "requests.csv";
        private List<Request> _requests;

        public RequestRepository()
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }

            _requests = File.ReadAllLines(_filePath)
                            .Where(l => !string.IsNullOrWhiteSpace(l))
                            .Select(line => Deserialize(line))
                            .ToList();
        }

        private Request Deserialize(string line)
        {
            var parts = line.Split(',');
            return new Request
            {
                Id = int.Parse(parts[0]),
                Description = parts[1],
                IsApproved = bool.Parse(parts[2])
            };
        }

        private string Serialize(Request r)
        {
            return $"{r.Id},{r.Description},{r.IsApproved}";
        }

        public IEnumerable<Request> GetAll() => _requests;

        public Request GetById(int id) => _requests.FirstOrDefault(r => r.Id == id);

        public void Add(Request entity)
        {
            _requests.Add(entity);
            Save();
        }

        public void Update(Request entity)
        {
            var r = GetById(entity.Id);
            if (r != null)
            {
                r.Description = entity.Description;
                r.IsApproved = entity.IsApproved;
                Save();
            }
        }

        public void Delete(int id)
        {
            var r = GetById(id);
            if (r != null)
            {
                _requests.Remove(r);
                Save();
            }
        }

        public void Save()
        {
            File.WriteAllLines(_filePath, _requests.Select(Serialize));
        }
    }
}
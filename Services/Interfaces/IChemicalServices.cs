using CLMS.Models;
using System.Collections.Generic;

namespace CLMS.Services.Interfaces
{
    public interface IChemicalService
    {
        List<Chemical> GetAll(string filter = "");
        Chemical GetById(int id);
        void AddChemical(Chemical chemical);
        void Update(Chemical chemical);
        void Delete(int id);
        List<Chemical> Search(string name);
    }
}
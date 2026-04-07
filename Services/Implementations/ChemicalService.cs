using System;
using System.Collections.Generic;
using System.Linq;

public class ChemicalService
{
    private readonly IRepository<Chemical> _repository;

    public ChemicalService(IRepository<Chemical> repository)
    {
        _repository = repository;
    }

    public Chemical Search(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var chemicals = _repository.GetAll();

            return chemicals.FirstOrDefault(c =>
                c.Name != null &&
                c.Name.ToLower().Contains(name.ToLower()));
        }
        catch (Exception)
        {
            Console.WriteLine("Gabim gjatë kërkimit.");
            return null;
        }
    }

    public bool Add(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var list = _repository.GetAll();

            list.Add(new Chemical
            {
                Id = list.Count + 1,
                Name = name
            });

            _repository.SaveAll(list);
            return true;
        }
        catch (Exception)
        {
            Console.WriteLine("Gabim gjatë shtimit.");
            return false;
        }
    }
}
using System.Collections.Generic;

public interface IRepository<T>
{
    List<T> GetAll();
    void SaveAll(List<T> items);
}
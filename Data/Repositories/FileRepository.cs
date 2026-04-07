using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class FileRepository<T> : IRepository<T>
{
    private readonly string _filePath;

    public FileRepository(string filePath)
    {
        _filePath = filePath;
    }

    public List<T> GetAll()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("File nuk u gjet, po krijoj file të ri...");
                File.Create(_filePath).Close();
                return new List<T>();
            }

            var json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<T>();

            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
        catch (Exception)
        {
            Console.WriteLine("Gabim gjatë leximit të file-it.");
            return new List<T>();
        }
    }

    public void SaveAll(List<T> items)
    {
        try
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }
        catch (Exception)
        {
            Console.WriteLine("Gabim gjatë ruajtjes së file-it.");
        }
    }
}
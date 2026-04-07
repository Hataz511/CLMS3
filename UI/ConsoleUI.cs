using System;

public class ConsoleUI
{
    private readonly ChemicalService _service;

    public ConsoleUI(ChemicalService service)
    {
        _service = service;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Kerko Chemical");
            Console.WriteLine("2. Shto Chemical");
            Console.WriteLine("0. Dil");
            Console.Write("Zgjedh: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SearchChemical();
                    break;
                case "2":
                    AddChemical();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Zgjedhje e pavlefshme!");
                    break;
            }
        }
    }

    private void SearchChemical()
    {
        try
        {
            Console.Write("Shkruaj emrin: ");
            string input = Console.ReadLine();

            var result = _service.Search(input);

            if (result == null)
                Console.WriteLine("Chemical nuk u gjet.");
            else
                Console.WriteLine($"U gjet: {result.Name} (ID: {result.Id})");
        }
        catch (Exception)
        {
            Console.WriteLine("Gabim gjatë kërkimit.");
        }
    }

    private void AddChemical()
    {
        try
        {
            Console.Write("Shkruaj emrin: ");
            string name = Console.ReadLine();

            bool success = _service.Add(name);

            if (!success)
                Console.WriteLine("Emri nuk mund të jetë bosh!");
            else
                Console.WriteLine("Chemical u shtua me sukses!");
        }
        catch (Exception)
        {
            Console.WriteLine("Gabim gjatë shtimit.");
        }
    }
}
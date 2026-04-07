class Program
{
    static void Main(string[] args)
    {
        var repository = new FileRepository<Chemical>("data.json");
        var service = new ChemicalService(repository);
        var ui = new ConsoleUI(service);

        ui.Run();
    }
}
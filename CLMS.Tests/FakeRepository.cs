using System.Collections.Generic;

public class FakeRepository : IRepository<Chemical>
{
	private List<Chemical> _data;

	public FakeRepository()
	{
		_data = new List<Chemical>
		{
			new Chemical { Id = 1, Name = "Acid" },
			new Chemical { Id = 2, Name = "Base" },
			new Chemical { Id = 3, Name = "Salt" }
		};
	}

	public List<Chemical> GetAll()
	{
		return _data;
	}

	public void SaveAll(List<Chemical> items)
	{
		_data = items;
	}
}
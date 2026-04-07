using Xunit;

public class ChemicalServiceTests
{
	[Fact]
	public void Search_ExistingItem_ReturnsItem()
	{
		var service = new ChemicalService(new FakeRepository());

		var result = service.Search("Acid");

		Assert.NotNull(result);
		Assert.Equal("Acid", result.Name);
	}

	[Fact]
	public void Search_NonExisting_ReturnsNull()
	{
		var service = new ChemicalService(new FakeRepository());

		var result = service.Search("XYZ");

		Assert.Null(result);
	}

	[Fact]
	public void Add_EmptyName_ReturnsFalse()
	{
		var service = new ChemicalService(new FakeRepository());

		var result = service.Add("");

		Assert.False(result);
	}
}
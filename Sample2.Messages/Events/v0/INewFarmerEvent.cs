namespace Sample2.Messages.Events.v0
{
	public interface INewFarmerEvent
	{
		int Id { get; set; }
		string Name { get; set; }
	}
}
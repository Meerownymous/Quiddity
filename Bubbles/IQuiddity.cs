namespace Existence
{
	/// <summary>
	/// A quiddity which is the sum of different aspects.
	/// </summary>
	public interface IQuiddity
	{
		string Name();
		ICollection<string> Aspects();
		IAspect Aspect(string name);
		void Mutate(string aspect);
	}
}
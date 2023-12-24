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

		ICollection<string> Mutations();
        void Mutate(IMutation mutation);
	}
}
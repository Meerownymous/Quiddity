namespace Quiddity
{
	/// <summary>
	/// A quiddity which is the sum of different aspects.
	/// </summary>
	public interface IQuiddity
	{
		/// <summary>
		/// Name of the quiddity, usually a unique identifier.
		/// </summary>
		string Name();

		/// <summary>
		/// List the different aspects that can be requested.
		/// </summary>
		ICollection<string> Aspects();

		/// <summary>
		/// Access a certain aspect.
		/// </summary>
		IAspect Aspect(string name);

		/// <summary>
		/// List the different possible mutations that can be applied.
		/// </summary>
		ICollection<string> Mutations();

		/// <summary>
		/// Apply a certain mutation.
		/// </summary>
        void Mutate(IMutation mutation);
	}
}
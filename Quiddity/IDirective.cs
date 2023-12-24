namespace Quiddity
{
	/// <summary>
	/// Directive which will digest mutations and apply them to the target quiddity or cluster.
	/// </summary>
	public interface IDirective
	{
		/// <summary>
		/// Name of this directive, unique in scope of the targhet quiddity/cluster.
		/// </summary>
		string Name();

		/// <summary>
		/// Apply the given mutation.
		/// </summary>
		void Apply(IMutation mutation);
	}
}
namespace Existence
{
	/// <summary>
	/// Directive which can apply mutations.
	/// </summary>
	public interface IDirective
	{
		string Name();
		void Apply(IMutation mutation);
	}
}
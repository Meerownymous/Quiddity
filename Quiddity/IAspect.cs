using ZiZZi;

namespace Quiddity
{
	/// <summary>
	/// Aspect of a quiddity. The aspect can be materialized as object or any other format
	/// using ZiZZi <see cref="IMatter{TContent}" implementations. />
	/// </summary>
	public interface IAspect
	{
		/// <summary>
		/// Name of this aspect, unique to the quiddity.
		/// </summary>
		/// <returns></returns>
		string Name();

		/// <summary>
		/// Information vault for this aspect. This aggregates the delcared data
		/// once calling <see cref="IBlox.Form{T}(IMatter{T})"/>.
		/// </summary>
		IBlox Vault();

        /// <summary>
        /// Information vault for this aspect, materialized as document in the
		/// format of the presented <see cref="IMatter{TContent}"/>.
		/// Common are <see cref="ZiZZi.Matter.JsonMatter"/> or <see cref="ZiZZi.Matter.XML.XmlMatter"/>
        /// </summary>
        TFormat As<TFormat>(IMatter<TFormat> matter) where TFormat : class;

        /// <summary>
        /// Information vault for this aspect, materialized into the given blueprint of
		/// an anonymous object. Only the fields of the anonymous object will be computed from the source.
        /// </summary>
        TBlueprint Into<TBlueprint>(TBlueprint blueprint) where TBlueprint : class;
	}
}


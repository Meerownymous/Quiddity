using System;

namespace Quiddity.Cluster
{
	/// <summary>
	/// A cluster which consists of multiple quiddities.
	/// </summary>
	public interface ICluster
	{
		/// <summary>
		/// Mutate this cluster.
		/// </summary>
		void Mutate(IMutation mutation);

		/// <summary>
		/// Filter quiddities of this cluster.
		/// </summary>
        ICollection<IQuiddity> Filtered(IFilter filter);

		/// <summary>
		/// Amount of quiddities in this cluster.
		/// </summary>
		long Count();
	}
}


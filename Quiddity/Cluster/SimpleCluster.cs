using System;
using Tonga;
using Tonga.Collection;
using Tonga.Enumerable;
using Tonga.Map;

namespace Quiddity.Cluster
{
    /// <summary>
    /// A simple cluster in memory, containing multiple quiddities.
    /// </summary>
    public sealed class SimpleCluster : ICluster
    {
        private readonly ICollection<IQuiddity> quiddities;
        private readonly IMap<string, IDirective> directives;

        /// <summary>
        /// A simple cluster in memory, containing multiple quiddities.
        /// </summary>
        public SimpleCluster(params IQuiddity[] quiddities) : this(
            AsCollection._(quiddities),
            None._<IDirective>()
        )
        { }

        /// <summary>
        /// A simple cluster in memory, containing multiple quiddities.
        /// </summary>
        public SimpleCluster(IEnumerable<IQuiddity> quiddities, IEnumerable<IDirective> directives)
        {
            this.quiddities = new LinkedList<IQuiddity>(quiddities);
            this.directives =
                AsMap._(
                    Tonga.Enumerable.Mapped._(
                        directive => AsPair._(directive.Name(), directive),
                        directives
                    )
                );
        }

        public long Count() => this.quiddities.Count;

        public ICollection<IQuiddity> Filtered(IFilter filter)
        {
            return this.quiddities;
        }

        public void Mutate(IMutation mutation) =>
            this.directives[mutation.Name()]
                .Apply(mutation);
    }
}


using Quiddity;
using Tonga;
using Tonga.Enumerable;
using Tonga.Map;

namespace Quiddity
{
    /// <summary>
    /// A simple quiddity which can deliver aspects and accepts mutations.
    /// </summary>
    public sealed class SimpleQuiddity : IQuiddity
	{
        private readonly string name;
        private readonly IMap<string, IAspect> aspects;
        private readonly IMap<string, IDirective> directives;

        /// <summary>
        /// A simple quiddity which can deliver aspects and accepts mutations.
        /// </summary>
        public SimpleQuiddity(string name, params IAspect[] aspects) : this(
            name, AsEnumerable._(aspects)
        )
        { }

        /// <summary>
        /// A simple quiddity which can deliver aspects and accepts mutations.
        /// </summary>
        public SimpleQuiddity(string name, IEnumerable<IAspect> aspects) : this(
            name, AsEnumerable._(aspects), None._<IDirective>()
        )
        { }

        /// <summary>
        /// A simple quiddity which can deliver aspects and accepts mutations.
        /// </summary>
        public SimpleQuiddity(string name, IEnumerable<IAspect> aspects, IEnumerable<IDirective> directives)
		{
            this.name = name;
            this.aspects =
                AsMap._(
                    Mapped._(
                        aspect => AsPair._(aspect.Name(), aspect),
                        aspects
                    )
                );
            this.directives =
                AsMap._(
                    Mapped._(
                        directive => AsPair._(directive.Name(), directive),
                        directives
                    )
                );
        }

        public string Name() => this.name;

        public ICollection<string> Aspects() => this.aspects.Keys();

        public IAspect Aspect(string focus) => this.aspects[focus];

        public ICollection<string> Mutations() => this.directives.Keys();

        public void Mutate(IMutation mutation) => this.directives[mutation.Name()].Apply(mutation);
    }
}


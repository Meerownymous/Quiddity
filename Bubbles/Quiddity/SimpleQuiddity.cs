using Tonga;
using Tonga.Enumerable;
using Tonga.Map;
using ZiZZi;

namespace Existence.Quiddity
{
    /// <summary>
    /// A simple quiddity which can deliver aspects.
    /// </summary>
    public sealed class SimpleQuiddity : IQuiddity
	{
        private readonly IMap<string, IAspect> aspects;
        private readonly string name;

        /// <summary>
        /// A simple quiddity which can deliver aspects.
        /// </summary>
        public SimpleQuiddity(string name, params IAspect[] aspects) : this(
            name, AsEnumerable._(aspects)
        )
        { }

        /// <summary>
        /// A simple quiddity which can deliver aspects.
        /// </summary>
        public SimpleQuiddity(string name, IEnumerable<IAspect> aspects)
		{
            this.aspects =
                AsMap._(
                    Mapped._(
                        aspect => AsPair._(aspect.Name(), aspect),
                        aspects
                    )
                );
            this.name = name;
        }

        public string Name() => this.name;

        public IAspect Aspect(string focus) => this.aspects[focus];

        public ICollection<string> Aspects() => this.aspects.Keys();

        public void Mutate(string aspect)
        {
            throw new NotImplementedException();
        }
    }
}


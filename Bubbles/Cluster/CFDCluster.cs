using Existence.Directive;
using Tonga;
using Tonga.Enumerable;
using Tonga.Map;
using ZiZZi;
using ZiZZi.Matter.Object;

namespace Existence.Cluster
{
    /// <summary>
    /// Cluster which can create, filter and mutate the collection.
    /// </summary>
    public sealed class CFDCluster : ICluster
    {
        private readonly IList<IQuiddity> quiddities;
        private readonly IMap<string, IDirective> directives;

        /// <summary>
        /// Cluster which can create, filter and mutate the collection.
        /// </summary>
        public CFDCluster(Func<string, IQuiddity> create) : this(
            new List<IQuiddity>(), create
        )
        { }

        /// <summary>
        /// Cluster which can create, filter and mutate the collection.
        /// </summary>
        public CFDCluster(List<IQuiddity> quiddities, Func<string, IQuiddity> create) : this(
            quiddities,
            new SimpleDirective("Create",
                mutation =>
                {
                    var newName = mutation.Information().Form(ObjectMatter.Fill(new { Name = "" })).Name;
                    foreach (var q in quiddities)
                        if (q.Name() == newName) throw new InvalidOperationException($"'{newName}' already exists.");
                    quiddities.Add(create(newName));
                }
            ),
            new SimpleDirective("Delete",
                mutation =>
                {
                    var newName = mutation.Information().Form(ObjectMatter.Fill(new { Name = "" })).Name;
                    foreach (var q in quiddities)
                    {
                        if (q.Name() == newName) quiddities.Remove(q);
                        break;
                    }
                }
            )
        )
        { }

        public CFDCluster(List<IQuiddity> quiddities, params IDirective[] directives) : this(
            quiddities,
            AsEnumerable._(directives)
        )
        { }

        private CFDCluster(
            IList<IQuiddity> quiddities,
            IEnumerable<IDirective> directives
        )
        {
            this.quiddities = quiddities;
            this.directives =
                AsMap._(
                    Mapped._(
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
            this.directives[mutation.Name()].Apply(mutation);


        public sealed class Create : IMutation
        {
            private readonly string name;

            public Create(string name)
            {
                this.name = name;
            }

            public IBlox Information() => new ZiBlock(new ZiProp("Name", name));

            public string Name() => "Create";

            public bool Valid() => true;
        }
    }
}
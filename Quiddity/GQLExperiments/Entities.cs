using HotChocolate.Types;
using Tonga.List;
using ZiZZi;
using ZiZZi.Matter;
using ZiZZi.Matter.Object;

namespace HotChocolatePlay
{
    /// <summary>
    /// An entity which can be queried by graphql.
    /// The shape of the entity is defined by a blueprint object. This object's
    /// properties define the queryable fields.
    /// The second part is the source from which the blueprint will be built.
    /// If lazyFill is false (default), the source will always be fully constructed
    /// before rendering the object, even if fields are not needed.
    /// If you have costly operations in the source, you can set lazyFill to true
    /// and the source will only compute the necessary fields - for the cost of some
    /// performance.
    /// </summary>
    public sealed class Entities<TObject> : ObjectType
        where TObject : class
    {
        private readonly string name;
        private readonly TObject blueprint;
        private readonly IList<IBlox> source;
        private readonly bool lazyFill;

        /// <summary>
        /// An entity which can be queried by graphql.
        /// The shape of the entity is defined by a blueprint object. This object's
        /// properties define the queryable fields.
        /// The second part is the source from which the blueprint will be built.
        /// If lazyFill is false (default), the source will always be fully constructed
        /// before rendering the object, even if fields are not needed.
        /// If you have costly operations in the source, you can set lazyFill to true
        /// and the source will only compute the necessary fields - for the cost of some
        /// performance.
        /// </summary>
        public Entities(string name, TObject blueprint, IList<IBlox> source, bool lazyFill = false)
        {
            this.name = name;
            this.blueprint = blueprint;
            this.source = source;
            this.lazyFill = lazyFill;
        }

        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor
                .Field(this.name)
                .Type(new ListType(new DynamicType(this.name, this.blueprint, false)))
                .Resolve(ctx =>
                    Mapped._(
                        entry => entry.Form(
                            new Ternary<TObject>(
                                () => this.lazyFill,
                                () =>
                                MaskedMatter._(
                                    ObjectMatter.Fill(this.blueprint),
                                    ctx
                                ),
                                () => ObjectMatter.Fill(this.blueprint)
                            )
                        ),
                        source
                    )
                );
        }
    }

    public static class Entities
    {
        public static Entities<TObject> _<TObject>(
            string name, TObject blueprint, IList<IBlox> source, bool lazyFill = false
        ) where TObject : class
            => new Entities<TObject>(name, blueprint, source, lazyFill);
    }
}


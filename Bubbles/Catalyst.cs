using HotChocolate.Types;
using ZiZZi;
using ZiZZi.Matter;
using ZiZZi.Matter.Object;

namespace HotChocolatePlay
{
    /// <summary>
    /// A catalyst which can be queried by graphql to form concrete objects.
    /// The shape of the concrete is defined by a blueprint object. This concrete's
    /// properties define the queryable fields.
    /// The second part is the source from which the blueprint will be built.
    /// If lazyFill is false (default), the source will always be fully constructed
    /// before rendering the object, even if fields are not needed.
    /// If you have costly operations in the source, you can set lazyFill to true
    /// and the source will only compute the necessary fields - for the cost of some
    /// performance.
    /// </summary>
    public sealed class Catalyst<TObject> : ObjectType
        where TObject : class
    {
        private readonly string name;
        private readonly TObject blueprint;
        private readonly IBlox source;
        private readonly bool lazyFill;

        /// <summary>
        /// A catalyst which can be queried by graphql to form concrete objects.
        /// The shape of the concrete is defined by a blueprint object. This concrete's
        /// properties define the queryable fields.
        /// The second part is the source from which the blueprint will be built.
        /// If lazyFill is false (default), the source will always be fully constructed
        /// before rendering the object, even if fields are not needed.
        /// If you have costly operations in the source, you can set lazyFill to true
        /// and the source will only compute the necessary fields - for the cost of some
        /// performance.
        /// </summary>
        public Catalyst(string name, TObject blueprint, IBlox source, bool lazyFill = false)
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
                .Type(new DynamicType(this.name, this.blueprint, false))
                .Resolve(ctx =>
                    this.source.Form(
                        new Ternary<TObject>(
                            () => this.lazyFill,
                            () =>
                            MaskedMatter._(
                                ObjectMatter.Fill(this.blueprint),
                                ctx
                            ),
                            () => ObjectMatter.Fill(this.blueprint)
                        )
                    )
                );
        }
    }

    public static class Catalyst
    {
        public static Catalyst<TObject> _<TObject>(
            string name, TObject blueprint, IBlox source, bool lazyFill = false
        ) where TObject : class
            => new Catalyst<TObject>(name, blueprint, source, lazyFill);
    }
}


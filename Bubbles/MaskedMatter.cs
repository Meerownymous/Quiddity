using HotChocolate.Language;
using HotChocolate.Resolvers;
using Tonga.List;
using Tonga.Scalar;
using ZiZZi;
using ZiZZi.Matter.Object;

namespace HotChocolatePlay
{
    /// <summary>
    /// Matter which only takes fields that are included in the given fieldset.
    /// This prevents rendering/aggregation of unrequested fields.
    /// </summary>
    public sealed class MaskedMatter<TResult> : IMatter<TResult>
        where TResult : class
	{
        private readonly IMatter<TResult> origin;
        private readonly IList<FieldNode> mask;
        private readonly bool isRoot;
        private readonly IList<string> propertyNames;

        /// <summary>
        /// Matter which only takes fields that are included in the given fieldset.
        /// This prevents rendering/aggregation of unrequested fields.
        /// </summary>
        public MaskedMatter(IMatter<TResult> origin, IResolverContext mask, bool isRoot = true) : this(
            origin,
            AsList._(() =>
                mask.Selection.SyntaxNode.SelectionSet!.Selections.OfType<FieldNode>()
            ),
            isRoot
        )
        { }

        /// <summary>
        /// Matter which only takes fields that are included in the given fieldset.
        /// This prevents rendering/aggregation of unrequested fields.
        /// </summary>
        public MaskedMatter(IMatter<TResult> origin, FieldNode mask, bool isRoot = false) : this(
            origin,
            AsList._(() =>
                mask.SelectionSet!.Selections.OfType<FieldNode>()
            ),
            isRoot
        )
        { }

        /// <summary>
        /// Matter which only takes fields that are included in the given fieldset.
        /// This prevents rendering/aggregation of unrequested fields.
        /// </summary>
        private MaskedMatter(IMatter<TResult> matter, IList<FieldNode> nodes, bool isRoot = false)
		{
            this.origin = matter;
            this.mask = nodes;
            this.isRoot = isRoot;
            this.propertyNames =
                Tonga.List.Sticky._(
                    Mapped._(
                        node => node.Name.Value,
                        nodes
                    )
                );
        }

        public TResult Content() => this.origin.Content();

        public IMatter<TResult> Open(string contentType, string name)
        {
            IMatter<TResult> result = new VoidMatter<TResult>();
            if (HasProperty(name, this.propertyNames))
            {
                result =
                    new MaskedMatter<TResult>(
                        this.origin.Open(contentType, name),
                        First._(
                            node => node.Name.Value.Equals(name, StringComparison.OrdinalIgnoreCase),
                            this.mask
                        )
                        .Value(),
                        false
                    );
            }
            else if(this.isRoot)
            {
                result =
                    new MaskedMatter<TResult>(
                        this.origin.Open(contentType, name),
                        this.mask,
                        false
                    );

            }
            return result;
        }

        public void Present(string name, Func<IContent<string>> content)
        {
            if (HasProperty(name, this.propertyNames))
                this.origin.Present(name, content);
        }

        public void Present(string name, string dataType, Func<IContent<byte[]>> content)
        {
            if (HasProperty(name, this.propertyNames))
                this.origin.Present(name, dataType, content);
        }

        public void Present(string name, string dataType, Func<IContent<Stream>> content)
        {
            if (HasProperty(name, this.propertyNames))
                this.origin.Present(name, dataType, content);
        }

        private static bool HasProperty(string name, IList<string> propertyNames)
        {
            return propertyNames.Contains(name, StringComparer.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Matter which only takes fields that are included in the given fieldset.
    /// This prevents rendering/aggregation of unrequested fields.
    /// </summary>
    public static class MaskedMatter
    {
        /// <summary>
        /// Matter which only takes fields that are included in the given fieldset.
        /// This prevents rendering/aggregation of unrequested fields.
        /// </summary>
        public static MaskedMatter<TResult> _<TResult>(IMatter<TResult> origin, IResolverContext mask)
            where TResult : class
            =>
            new MaskedMatter<TResult>(origin, mask);
    }
}


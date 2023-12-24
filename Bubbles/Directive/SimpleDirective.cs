namespace Existence.Directive
{
    /// <summary>
    /// A simple directive which applies given mutations of they match this directives name.
    /// </summary>
    public sealed class SimpleDirective : IDirective
    {
        private readonly string name;
        private readonly Action<IMutation> act;

        /// <summary>
        /// A simple directive which applies given mutations of they match this directives name.
        /// </summary>
        public SimpleDirective(string name, Action<IMutation> act)
        {
            this.name = name;
            this.act = act;
        }

        public string Name() => this.name;

        public void Apply(IMutation mutation)
        {
            if (!this.name.Equals(mutation.Name()))
                throw new ArgumentException(
                    $"Mutation '{mutation.Name()}' cannot be processed by this directive '{this.name}'"
                );
            this.act(mutation);
        }
    }
}


using ZiZZi;

namespace Existence.Mutation
{
	/// <summary>
	/// A simple named mutation.
	/// </summary>
	public sealed class SimpleMutation : IMutation
	{
        private readonly string name;
        private readonly IBlox information;
        private readonly Func<bool> valid;

        /// <summary>
        /// A simple named mutation.
        /// </summary>
        public SimpleMutation(string name, IBlox information, bool valid = true) : this(
            name, information, () => valid
        )
        { }

        /// <summary>
        /// A simple named mutation.
        /// </summary>
        public SimpleMutation(string name, IBlox information, Func<bool> valid)
		{
            this.name = name;
            this.information = information;
            this.valid = valid;
        }

        public IBlox Information() => this.information;

        public string Name() => this.name;

        public bool Valid() => this.valid();
    }
}


using ZiZZi;
using ZiZZi.Matter.Object;

namespace Quiddity.Aspect
{
	/// <summary>
	/// A simple aspect whose information is linked to a name.
	/// </summary>
    public sealed class SimpleAspect : IAspect
	{
        private readonly string name;
        private readonly IBlox information;

        /// <summary>
        /// A simple aspect whose information is linked to a name.
        /// </summary>
        public SimpleAspect(string name, IBlox information)
        {
            this.name = name;
            this.information = information;
        }

        public Blueprint Into<Blueprint>(Blueprint blueprint) where Blueprint : class
            => this.information.Form(ObjectMatter.Fill(blueprint));

        public TFormat As<TFormat>(IMatter<TFormat> matter) where TFormat : class
            => this.information.Form(matter);

        public string Name() => this.name;

        public IBlox Vault() => this.information;
    }
}


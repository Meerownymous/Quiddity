using Quiddity.Directive;
using Quiddity.Mutation;
using Xunit;
using ZiZZi;

namespace Existence.Directive
{
	public sealed class SimpleDirectiveTests
	{
		[Fact]
		public void RejectsWrongMutation()
		{
			Assert.Throws<ArgumentException>(() =>
				new SimpleDirective("Accept Only Right Things", (info) => { })
					.Apply(new SimpleMutation("Wrong Thing", new ZiBlock()))
			);
		}

        [Fact]
        public void AcceptsMutation()
        {
			var accepted = false;
            new SimpleDirective("Succeed", (info) => accepted = true)
                .Apply(new SimpleMutation("Succeed", new ZiBlock()));

			Assert.True(accepted);
        }
    }
}
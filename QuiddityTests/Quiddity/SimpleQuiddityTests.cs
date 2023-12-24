using Quiddity.Aspect;
using Quiddity.Directive;
using Quiddity.Mutation;
using Tonga.Enumerable;
using Xunit;
using ZiZZi;
using ZiZZi.Matter.Object;

namespace Quiddity.Tests
{
	public sealed class SimpleQuiddityTests
	{
		[Fact]
		public void DeliversAspects()
		{
			Assert.Equal(
                "Success-Avenue",
                new SimpleQuiddity("Testerius",
					new SimpleAspect("Location",
						new ZiBlock(
							new ZiProp("Street", "Success-Avenue")
						)
					)
				)
				.Aspect("Location")
				.Into(new { Street = String.Empty })
				.Street
			);
		}

        [Fact]
        public void DigestsMutations()
        {
			var memory = new Dictionary<string, string>();
			memory["Street"] = "Fail-Avenue";

			new SimpleQuiddity("Testerius",
				AsEnumerable._(
					new SimpleAspect("Location",
						new ZiBlock("Something",
							new ZiProp("Street", () => memory["Street"])
						)
					)
				),
				AsEnumerable._(
					new SimpleDirective("Relocate",
						mutation =>
							memory["Street"] =
								mutation
									.Information()
									.Form(
										ObjectMatter.Fill(new { Target = String.Empty })
									).Target
					)
				)
			).Mutate(
				new SimpleMutation("Relocate",
					new ZiBlock("Movement",
						new ZiProp("Target", "Success-Avenue")
					)
				)
			);

			Assert.Equal("Success-Avenue", memory["Street"]);
        }
    }
}

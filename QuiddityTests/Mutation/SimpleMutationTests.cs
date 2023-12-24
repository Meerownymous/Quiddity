using Quiddity.Mutation;
using Xunit;
using ZiZZi;
using ZiZZi.Matter.Object;

namespace Quiddity.Mutation.Tests
{
	public sealed class SimpleMutationTests
	{
		[Fact]
		public void DeliversName()
		{
			Assert.Equal(
				"Relocate",
				new SimpleMutation(
					"Relocate",
					new ZiBlock(
						new ZiProp("Country", "French Polynesia")
					)
				).Name()
			);
		}

        [Fact]
        public void DeliversInformation()
        {
            Assert.Equal(
                "French Polynesia",
                new SimpleMutation(
                    "Relocate",
                    new ZiBlock(
                        new ZiProp("Country", "French Polynesia")
                    )
                )
				.Information()
				.Form(ObjectMatter.Fill(new { Country = "" }))
				.Country
            );
        }

        [Fact]
        public void DeliversValidity()
        {
            Assert.True(
                new SimpleMutation(
                    "Relocate",
                    new ZiBlock(
                        new ZiProp("Country", "French Polynesia")
                    )
                )
                .Valid()
            );
        }
    }
}


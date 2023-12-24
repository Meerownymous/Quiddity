using Existence.Quiddity;
using Xunit;
using ZiZZi;

namespace Existence.Aspect.Tests
{
	public sealed class SimpleAspectTests
	{
		[Fact]
		public void DeliversName()
		{
			Assert.Equal(
				"Testing",
				new SimpleAspect(
					"Testing",
					new ZiProp("Result", "Success")
				).Name()
			);
		}

        [Fact]
        public void DeliversInformation()
        {
            Assert.Equal(
                "Success",
                new SimpleAspect("Testing",
					new ZiBlock("UnitTest",
						new ZiProp("Result", "Success")
					)
                )
				.Into(new { Result = "" })
				.Result
            );
        }
    }
}


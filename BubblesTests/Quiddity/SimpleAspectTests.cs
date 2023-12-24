using System;
using Existence.Quiddity;
using Xunit;
using ZiZZi;

namespace ExistenceTests.Quiddity
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
                new SimpleAspect(
					"Success",
					new ZiBlock("Unit",
						new ZiProp("Result", "Success")
					)
                )
				.Into(new { Result = "" })
				.Result
            );
        }
    }
}


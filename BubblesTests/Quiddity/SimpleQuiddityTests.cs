using Xunit;
using ZiZZi;

namespace Existence.Quiddity.Tests
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
						new ZiBlock("Something",
							new ZiProp("Street", "Success-Avenue")
						)
					)
				)
				.Aspect("Location")
				.Into(new { Street = String.Empty })
				.Street
			);
		}
	}
}

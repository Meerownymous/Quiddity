using System;
using Existence.Cluster;
using Existence.Quiddity;
using Xunit;
using ZiZZi;

namespace Existence
{
	public sealed class SimpleClusterTests
	{
		[Fact]
		public void Counts()
		{
			Assert.Equal(
				2,
				new SimpleCluster(
					new SimpleQuiddity("Jay",
						new SimpleAspect("Appearance", new ZiBlock(new ZiProp("Hair", "Long")))
					),
					new SimpleQuiddity("Silent Bob",
						new SimpleAspect("Appearance", new ZiBlock(new ZiProp("Hair", "Short")))
					)
				).Count()
			);
		}
	}
}


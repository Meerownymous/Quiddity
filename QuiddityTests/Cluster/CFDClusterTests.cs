﻿using System;
using Tonga.Scalar;
using Xunit;

namespace Quiddity.Cluster
{
	public sealed class CFDClusterTests
	{
		[Fact]
		public void Creates()
		{
			var cluster = new CFDCluster(name => new SimpleQuiddity(name));
			cluster.Mutate(new CFDCluster.Create("Newbert"));

			Assert.Equal(
				"Newbert",
				First._(cluster.Filtered(null)).Value().Name()
			);
        }
	}
}


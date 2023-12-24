using System;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolatePlay;
using Microsoft.Extensions.DependencyInjection;

namespace Quiddity.GQLExperiments.Tests
{
    //https://www.youtube.com/watch?v=Nf7nX2H_iiM
    public static class TestServices
	{
		static TestServices()
		{
            Services =
				new ServiceCollection()
					.AddGraphQLServer()
					.Services
					.AddSingleton(sp =>
						new RequestExecutorProxy(
							sp.GetRequiredService<IRequestExecutorResolver>(),
							Schema.DefaultName
						)
					)
					.BuildServiceProvider();

			Executor = Services.GetRequiredService<RequestExecutorProxy>();
		}

		public static IServiceProvider Services{ get; set; }

		public static RequestExecutorProxy Executor { get; set; }

		public static async Task<string> ExecuteRequestAsync(
			Action<IQueryRequestBuilder> configureRequest,
			CancellationToken cancellationToken
		)
		{
			await using var scope = Services.CreateAsyncScope();

			var requestBuilder = new QueryRequestBuilder();
			requestBuilder.SetServices(scope.ServiceProvider);
			configureRequest(requestBuilder);
			var request = requestBuilder.Create();

			await using var result =
				await Executor.ExecuteAsync(
					request,
					cancellationToken
				);

			result.ExpectQueryResult();

			return result.ToJson();
		}
	}
}


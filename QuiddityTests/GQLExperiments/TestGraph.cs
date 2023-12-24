using HotChocolate;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace Quiddity.GQLExperiments.Tests
{
    public sealed class Graph
    {
        public Graph()
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

        private static IServiceProvider Services { get; set; }

        private static RequestExecutorProxy Executor { get; set; }
    }
}


using HotChocolate;
using HotChocolate.Execution;
using HotChocolatePlay;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;
using Xunit;
using ZiZZi;

namespace Quiddity.GQLExperiments.Tests
{
	public sealed class EntityTests
	{
		[Fact]
		public async void EntityCanBeQueried()
		{
            var service =
                new ServiceCollection()
                    .AddGraphQLServer()
                    .AddType<QueryDummy>()
                    .AddQueryType(
                        Catalyst._(
                            "TVShow",
                            new
                            {
                                ID = String.Empty,
                                Name = String.Empty,
                                Title = String.Empty,
                                Author = new
                                {
                                    ID = 789,
                                    FirstName = String.Empty,
                                    LastName = String.Empty
                                }
                            },
                            new ZiBlock("root",
                                new ZiProp("ID", "1"),
                                new ZiProp("Name", "Zini"),
                                new ZiProp("Title", "Doctor"),
                                new ZiBlock("Author",
                                    new ZiProp("ID", 789),
                                    new ZiProp("FirstName", "Werner"),
                                    new ZiProp("LastName", "Schulze-Erdel")
                                )
                            )
                        )
                    )
                    .ConfigureSchema(sb => sb.ModifyOptions(opts => opts.StrictValidation = false));

            var result =
                (await service
                        .ExecuteRequestAsync("{ TVShow { name author { id firstname lastname } } }")
                );

            result.ToJson().MatchSnapshot();
        }
	}
}


using HotChocolate;
using HotChocolate.Execution;
using HotChocolatePlay;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;
using Tonga.List;
using Xunit;
using ZiZZi;

namespace Quiddity.GQLExperiments.Tests
{
	public sealed class EntitiesTests
	{
		[Fact]
		public async void EntitiesCanBeQueried()
		{
            var lst = new List<( string name, string city)>();


            var service =
                new ServiceCollection()
                    .AddGraphQLServer()
                    .AddType<QueryDummy>()
                    .AddQueryType(
                        Entities._(
                            "TVShows",
                            new
                            {
                                ID = String.Empty,
                                Name = String.Empty,
                                Description = String.Empty,
                                Author = new
                                {
                                    ID = 789,
                                    FirstName = String.Empty,
                                    LastName = String.Empty
                                }
                            },
                            new AsList<IBlox>(
                                new ZiBlock("root",
                                    new ZiProp("Name", "Killing Eve"),
                                    new ZiProp("Description", "The Villanelle Stories"),
                                    new ZiBlock("Author",
                                        new ZiProp("FirstName", "Luke"),
                                        new ZiProp("LastName", "Jennings")
                                    )
                                ),
                                new ZiBlock("root",
                                    new ZiProp("Name", "Zini"),
                                    new ZiProp("Description", "Doctor"),
                                    new ZiBlock("Author",
                                        new ZiProp("FirstName", "Werner"),
                                        new ZiProp("LastName", "Schulze-Erdel")
                                    )
                                )
                            )
                        )
                    )
                    .ConfigureSchema(sb => sb.ModifyOptions(opts => opts.StrictValidation = false));

            var result =
                (await service
                        .ExecuteRequestAsync("{ TVShows { name author { firstname lastname } } }")
                );

            result.ToJson().MatchSnapshot();
        }
	}
}


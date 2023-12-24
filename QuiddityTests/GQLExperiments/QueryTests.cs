using HotChocolate;
using HotChocolate.Execution;
using HotChocolatePlay;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Snapshooter.Xunit;
using Newtonsoft.Json.Linq;
using HotChocolate.Types;
using HotChocolatePlay.Experiments;
using System.Diagnostics;
using ZiZZi;
using ZiZZi.Matter.Object;
using Tonga.Enumerable;

namespace Quiddity.GQLExperiments.Tests
{
	public class QueryTests
	{
        [Fact]
        public void CreateGeneric()
        {
            //Assuming that Property.PropertyType is something like List<T>
            //Type elementType = typeof(List<>).GetGenericArguments()[0];
            Type listType = typeof(List<>).MakeGenericType(new { Name = "" }.GetType());
            dynamic repository = Activator.CreateInstance(listType);
            repository.Add(new { Name = "Test" });

            Type objectType = typeof(ObjectType<>).MakeGenericType(new { Name = "" }.GetType());
            dynamic result = Activator.CreateInstance(objectType);
        }

        [Fact]
        public async void FetchesDynamic()
        {
            var result =
                (await
                    new ServiceCollection()
                        .AddGraphQLServer()
                        .AddType<QueryDummy>()
                        .AddQueryType<DynamicQueryType>()
                        .ConfigureSchema(sb => sb.ModifyOptions(opts => opts.StrictValidation = false))
                        .ExecuteRequestAsync("{ book { name title author { id firstname lastname } } }")
                ).ToJson();

            result.MatchSnapshot();
        }

        [Fact]
        public async void FetchesZizzi()
        {
            //var authorBlueprint =
            //    new
            //    {
            //        ID = 789,
            //        FirstName = String.Empty,
            //        LastName = String.Empty
            //    };

            //var zizziBlueprint =
            //        //new[]
            //        //{
            //        new
            //        {
            //            ID = String.Empty,
            //            Name = String.Empty,
            //            Title = String.Empty,
            //            Author = authorBlueprint
            //        };
            //    //};

            //var filled =
            //    new ZiBlock("root",
            //        new ZiProp("ID", "1"),
            //        new ZiProp("Name", "Zini"),
            //        new ZiProp("Title", "Doctor"),
            //        new ZiBlock("Author",
            //            new ZiProp("ID", 789),
            //            new ZiProp("FirstName", "Werner"),
            //            new ZiProp("LastName", "Schulze-Erdel")
            //        )
            //    ).Form(ObjectMatter.Fill(zizziBlueprint));

            var service =
                new ServiceCollection()
                        .AddGraphQLServer()
                        .AddType<QueryDummy>()
                        .AddQueryType<DynamicQueryType>()
                        .ConfigureSchema(sb => sb.ModifyOptions(opts => opts.StrictValidation = false));

            //var sw = new Stopwatch();
            //sw.Start();
            //for (int i = 0; i < 1000; i++)
            //{
            var result =
                (await service
                        .ExecuteRequestAsync("{ zizzi { name author { id firstname lastname } } }")
                );
            //}
            //var time = sw.ElapsedMilliseconds;

            result.ToJson().MatchSnapshot();
        }

        [Fact]
		public async void SchemaIsCurrent()
		{
            var schema =
                await
                    TestServices
                        .Executor
                        .GetSchemaAsync(default);

            schema.ToString().MatchSnapshot();
        }

        [Fact]
        public async void FetchesAnyType()
        {
            var result =
                (await
                    new ServiceCollection()
                        .AddGraphQLServer()
                        .AddType<AuthorType>()
                        .AddType<BookType>()
                        //.AddQueryType<Query>()
                        .ExecuteRequestAsync("{ book { authors { name } attributes nonsense } }")
                )
                .ToJson();

            result.MatchSnapshot();
        }

        [Fact]
        public async void FetchesAuthorAndTitle()
        {
            var result =
                (await
                    new ServiceCollection()
                        .AddGraphQLServer()
                        //.AddQueryType<Query>()
                        .ExecuteRequestAsync("{ book { title authors { name } } }")
                )
                .ToJson();

            result.MatchSnapshot();
        }

        [Fact]
        public async void FillsObject()
        {
            //https://www.youtube.com/watch?v=wODiVDT8ECI
            var expansion = new Expansion();
            var result = expansion.Fill("book", new { Title = "", Authors = "" });

            Assert.Equal("C# in depth.", result.Title);

            //result.MatchSnapshot();
        }

        [Fact]
        public async void DefinesExplicitely()
        {
            var result =
                (await
                    new ServiceCollection()
                        .AddGraphQLServer()
                        .AddType<AuthorType>()
                        .AddType<BookType>()
                        .AddType<DynamicQueryType>()
                        //.AddQueryType<Query>()
                        .ExecuteRequestAsync("{ book { title authors { name } } }")
                )
                .ToJson();

            result.MatchSnapshot();
        }

        [Fact]
        public async void AddsBook()
        {
            var result =
                (await
                    new ServiceCollection()
                        .AddSingleton(new Books() )
                        .AddGraphQLServer()
                        .AddType<AuthorType>()
                        .AddType<BookType>()
                        .AddType<AddedPayloadType>()
                        //.AddQueryType<Query>()
                        //.AddMutationType<Mutation>()
                        .ExecuteRequestAsync("""mutation { addBook( title : "Elegance" ){ bla item } }""")
                )
                .ToJson();

            result.MatchSnapshot();
        }
    }
}


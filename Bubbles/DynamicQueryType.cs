using HotChocolate.Types;
using ZiZZi;
using ZiZZi.Matter.Object;

namespace HotChocolatePlay.Experiments
{
    public sealed class DynamicQueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            var authorBlueprint =
                new
                {
                    ID = 789,
                    FirstName = String.Empty,
                    LastName = String.Empty
                };

            var bookBlueprint =
                new
                {
                    ID = String.Empty,
                    Name = String.Empty,
                    Title = String.Empty,
                    Author = authorBlueprint
                };


            var zizziBlueprint =
                    //new[]
                    //{
                    new
                    {
                        ID = String.Empty,
                        Name = String.Empty,
                        Title = String.Empty,
                        Author = authorBlueprint
                    };

            descriptor
                .Field("zizzi")
                .Type(new DynamicType("zizzi", zizziBlueprint, false))
                .Resolve(ctx =>
                    new ZiBlock("root",
                        new ZiProp("ID", "1"),
                        new ZiProp("Name", () => "Zini"),
                        new ZiProp("Title", () => "Werner und Zini"),
                        new ZiBlock("Author",
                            new ZiProp("ID", 789),
                            new ZiProp("FirstName", () => "Werner"),
                            new ZiProp("LastName", "Schulze-Erdel")
                        )
                    ).Form(
                        MaskedMatter._(
                            ObjectMatter.Fill(zizziBlueprint),
                            ctx
                        )
                    )
                );

            descriptor
                .Field("book")
                .Type(new DynamicType("book", bookBlueprint, true))
                .Resolve(ctx =>
                {
                    return ValueTask.FromResult<object>(
                        new
                        {
                            ID = "Book-123",
                            Name = "El Booko",
                            Title = "Bookolicious",
                            Author = authorBlueprint
                        }
                    )!;
                });
        }
    }
}


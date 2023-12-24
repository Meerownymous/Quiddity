using HotChocolate.Types;

namespace HotChocolatePlay
{
    public class Book
    {
        public string ID { get; set; }
        public string Title { get; set; }

        public List<Author> Authors { get; set; }
        public IDictionary<string, object> Attributes{ get; set; }

        public dynamic Nonsense { get; set; }
    }

    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(f => f.Title);
            descriptor.Field(f => f.Authors);
            descriptor.Field(f => f.Attributes).Type<AnyType>();
            descriptor.Field(f => f.Nonsense).Type<AnyType>();
        }
    }

    public class AddedPayload
    {
        public string Bla { get; set; }
        public string[] Item { get; set; }
    }

    public class AddedPayloadType : ObjectType<AddedPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<AddedPayload> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(f => f.Bla);
            descriptor.Field(f => f.Item);
            //descriptor.Field(f => f.Author);
        }
    }

    public class Author
    {
        public string Address { get; set; }
        public string Name { get; set; }
    }

    public class AuthorType : ObjectType<Author>
    {
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(f => f.Name);
            descriptor.Field(f => f.Address);
        }
    }

}


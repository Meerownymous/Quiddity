using System;

namespace HotChocolatePlay
{
	public sealed class Books : IRepository<Book>
	{
        private readonly IList<Book> content;

        public Books()
		{
            this.content = new List<Book>();
		}

        public string Add(string name)
        {
            var id = Guid.NewGuid().ToString();
            this.content
                .Add(
                    new Book() { ID = id, Title = name }
                );
            return id;
        }

        public ICollection<Book> Find(string query)
        {
            return this.content;
        }
    }
}


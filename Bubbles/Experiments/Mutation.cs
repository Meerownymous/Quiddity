using System.Collections.ObjectModel;
using HotChocolate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HotChocolatePlay
{
    public sealed class Mutation
	{
        public async Task<AddedPayload> AddBook([Service] Books service, string title)
        {
            service.Add(title);
            return
                new AddedPayload()
                {
                    Bla = "Bla",
                    Item = new string[] { "A", "B" }
                };
            //var result = new AddedPayload()
            //{
            //    Item = new System.Dynamic.ExpandoObject()
            //};
            //(result.Item as IDictionary<string,object>)["id"] = First._(
            //                Filtered._(
            //                    book => book.Title == title,
            //                    service.Find(string.Empty)
            //                )
            //            ).Value().ID;
            //return result.Item;
        }
    }
}


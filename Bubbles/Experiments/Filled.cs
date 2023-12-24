using System;
using System.Dynamic;
using System.Security.Principal;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Descriptors.Definitions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HotChocolatePlay
{
	public class Expansion
	{
        private readonly IRequestExecutorBuilder expansion;

        public Expansion()
		{
            this.expansion =
                new ServiceCollection()
                    .AddGraphQLServer()
                    .AddType<AuthorType>()
                    .AddType<BookType>();
                    //.AddQueryType<Query>();
        }

        public T Fill<T>(string type, T target) where T : class
        {
            var query = String.Empty;
            query += "{ " + type + " { ";
            foreach(var property in target.GetType().GetProperties())
            {
                query += property.Name.ToLower() + " ";
            }
            query += " } }";

            var result = Task.Run(async () => (await this.expansion.ExecuteRequestAsync(query)).ToJson()).Result;
            var jsonResult = JsonConvert.DeserializeObject<JObject>(result);
            var test = JsonConvert.DeserializeAnonymousType(((jsonResult.GetValue("data") as JObject).GetValue(type) as JObject).ToString(), target);

            IDictionary<string,object> returnObject = new ExpandoObject();
            foreach(var sourceProperty in ((jsonResult.GetValue("data") as JObject).GetValue(type) as JObject).Properties())
            {
                foreach(var targetProperty in target.GetType().GetProperties())
                {
                    if (sourceProperty.Name.ToLower() == targetProperty.Name.ToLower())
                    {
                        (returnObject as IDictionary<string, object>)[targetProperty.Name] = sourceProperty.Value.ToString();
                    }
                }
            }
            var ctor = target.GetType().GetConstructors().First();
            IList<object> parameters = new List<object>();
            foreach (var parameter in ctor.GetParameters())
            {
                if (returnObject.ContainsKey(parameter.Name))
                {
                    parameters.Add(returnObject[parameter.Name]);
                }
                else
                {
                    var def = parameter.GetType().IsValueType ? Activator.CreateInstance(parameter.GetType()) : null;
                    parameters.Add(def);
                }
            }
            var obj = ctor.Invoke(parameters.ToArray());

            var casted = obj as T;
            return casted;
        }
    }
}


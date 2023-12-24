using System;
using System.Diagnostics;
using HotChocolate.Types;
using Tonga.Enumerable;
using Tonga.Map;

namespace HotChocolatePlay
{
    public class DynamicType : ObjectType
    {
        private readonly string typeName;
        private readonly object blueprint;
        private readonly bool returnBlueprint;
        private readonly AsMap<Type, Type> types;

        public DynamicType(string entityType, object blueprint, bool returnBlueprint)
        {
            this.typeName = entityType;
            this.blueprint = blueprint;
            this.returnBlueprint = returnBlueprint;
            this.types =
                AsMap._(
                    AsEnumerable._(
                        AsPair._(typeof(string), typeof(StringType)),
                        AsPair._(typeof(int), typeof(IntType)),
                        AsPair._(typeof(float), typeof(FloatType)),
                        AsPair._(typeof(long), typeof(LongType))
                    )
                );
        }

        // This class represents the dynamic result type
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(this.typeName);
            foreach (var prop in this.blueprint.GetType().GetProperties())
            {
                if (!prop.PropertyType.Name.Contains("AnonymousType"))
                {
                    descriptor
                        .Field(prop.Name.ToLower())
                        .Type(this.types[prop.PropertyType])
                        .Resolve(ctx =>
                        {
                            var result = ctx.Parent<object>();
                            var value =
                                this.blueprint
                                    .GetType()
                                    .GetProperty(prop.Name)!
                                    .GetValue(result);
                            return ValueTask.FromResult<object>(value);
                        });
                }
                else
                {
                    descriptor
                        .Field($"{prop.Name.ToLower()}")
                        .Type(new DynamicType($"{this.typeName}_{prop.Name.ToLower()}", prop.GetValue(this.blueprint), this.returnBlueprint))
                        .Resolve(ctx =>
                        {
                            var result = ctx.Parent<object>();
                            var value =
                                this.blueprint
                                    .GetType()
                                    .GetProperty(prop.Name)
                                    .GetValue(result);
                            return ValueTask.FromResult<object>(value);
                        });
                }
            }
        }


            //descriptor.Name(this.typeName);
            //descriptor
            //    .Field("name")
            //    .Type<StringType>()
            //    .Resolve(ctx =>
            //    {
            //        var result = ctx.Parent<dynamic>();
            //        var id = result.ID;
            //        ctx.ContextData["A"] = "handover A";
            //        return ValueTask.FromResult<dynamic>($"A ({id})");
            //    });

            //if (this.subType != null)
            //{
            //    descriptor
            //        .Field("author")
            //        .Type(this.subType)
            //        .Resolve(ctx =>
            //        {
            //            var result = ctx.Parent<dynamic>();
            //            var id = result.ID;
            //            return ValueTask.FromResult<dynamic>(
            //                new { FirstName = "Mr.", LastName = "Nestedman", ID = "Infinity" }
            //            );
            //        });
            //}

            //descriptor.Field("title")
            //    .Type<StringType>()
            //    .Resolve(ctx =>
            //    {
            //        var result = ctx.Parent<dynamic>();
            //        var id = result.ID;
            //        ctx.ContextData["B"] = "handover B";
            //        return ValueTask.FromResult<dynamic>($"B ({id})");
            //    });
        
    }
}


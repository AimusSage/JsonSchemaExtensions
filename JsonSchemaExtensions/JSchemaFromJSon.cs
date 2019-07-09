using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;

namespace JsonSchemaExtensions
{
    public class JsonSchemaFromJson
    {
        public JSchema ConvertJsonToSchema(string jsonString)
        {
            var json = JObject.Parse(jsonString);
            //object schemaObject = JsonConvert.DeserializeObject<object>(jsonString);
            //var generator = new JSchemaGenerator();
            //var jsonReader = json.CreateReader();

            JSchema schema = new JSchema();
            schema.Id = new Uri($"https://example.com/root.json");
            schema.Title = "The Root Schema";
            schema.Type = JSchemaType.Object;
            foreach (var item in json)
            {
                schema.Required.Add(item.Key);
                schema.Properties.Add(item.Key, new JSchema()
                {
                    Id = new Uri($"#/Properties/{item.Key}", UriKind.Relative),
                    Type = GetJSchemaPropertyType(item.Value.Type),
                    Title = $"The {item.Key} Schema"
                });
            }

            Console.WriteLine($"Schema: {schema.ToString()}");
            return schema;
        }

        private JSchemaType GetJSchemaPropertyType(JTokenType item)
        {
            JSchemaType schemaType;
            if (Enum.TryParse(item.ToString(), out schemaType))
            {
                if (Enum.IsDefined(typeof(JSchemaType), schemaType))
                {
                    return schemaType;
                }
                else { throw new ArgumentOutOfRangeException($"{item.ToString()} is not a value in JschemaType enum"); }
            }
            else
            {
                throw new ArgumentOutOfRangeException($"{item.ToString()} is not a member of JschemaType enum");
            }
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Schema;

namespace JsonSchemaExtensions.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConvertJsonToJSchemaShouldSucceed()
        {
            JSchema jSchema = new JSchema();
            var jsonString = @"{'types': 
                                    {
                                        'String':'Hello',
                                        'Number':123,
                                        'Boolean':false,
                                        'array':[
                                            'item1',
                                            'item2'
                                        ]
                                    }
                                }";
            var schemarelizer = new JsonSchemaFromJson();
            var result = schemarelizer.ConvertJsonToSchema(jsonString);
            Assert.AreEqual(jSchema, result);
        }
    }
}

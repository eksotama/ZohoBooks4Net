using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Xunit;
using ZohoBooks4Net.Clients;
using ZohoBooks4Net.Domain.Entities;
using ZohoBooks4Net.Responses.PaginatedResponses;

namespace ZohoBooks4Net.Tests
{
    public class TestBase
    {
        public ZohoBooksClient Client { get; set; }
        public readonly Configuration configuration;

        public TestBase()
        {
            configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("configuration.json"));
            Client = new ZohoBooksClient(configuration);
        }

        [Fact]
        public void TestDeserializer()
        {
            var testResponse = JsonConvert.DeserializeObject<PaginatedResponse<Contact>>(File.ReadAllText("test_response.json"));

            var contacts = testResponse.Resource as List<Contact>;

            Assert.True(contacts.Count > 0);
        }
    }
}

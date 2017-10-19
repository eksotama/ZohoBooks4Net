using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using ZohoBooks4Net.Domain.Entities;
using ZohoBooks4Net.Domain.Enumeration.Variants;
using ZohoBooks4Net.Filters;
using ZohoBooks4Net.Responses.PaginatedResponses;

namespace ZohoBooks4Net.Tests
{
    public class TestContacts : TestBase
    {
        [Fact]
        public async void TestGetPageOneContactsAsync()
        {
            var contacts = await Client.Contacts.GetPageAsync(1) as List<Contact>;

            Assert.True(contacts.Count > 0);
        }

        [Fact]
        public async void TestGetContactAsync()
        {
            var testResponse = JsonConvert.DeserializeObject<PaginatedResponse<Contact>>(File.ReadAllText("test_response.json"));

            var contacts = testResponse.Resource as List<Contact>;

            var contact = await Client.Contacts.GetAsync(contacts[0].ContactId.ToString());

            Assert.Equal(contacts[0].CompanyName, contact.CompanyName);
        }

        [Fact]
        public async void TestGetContactsWithFilterAsync()
        {
            var contacts = await Client.Contacts.GetPageAsync(1, 10, new ContactsFilter
            {
                SearchText = "229th"
            });

            Assert.Equal(2, contacts.Count);

            contacts = await Client.Contacts.GetPageAsync(1, 10, new ContactsFilter
            {
                CompanyName = new Tuple<SearchVariant, string>(SearchVariant.StartsWith, "160")
            });

            Assert.Equal(3, contacts.Count);
            
            contacts = await Client.Contacts.GetPageAsync(1, 10, new ContactsFilter
            {
                ContactName = new Tuple<SearchVariant, string>(SearchVariant.StartsWith, "160")
            });

            Assert.Equal(3, contacts.Count);
            
            contacts = await Client.Contacts.GetPageAsync(1, 10, new ContactsFilter
            {
                Email = new Tuple<SearchVariant, string>(SearchVariant.None, "northerngirl87@yahoo.com")
            });

            Assert.Equal(1, contacts.Count);

            contacts = await Client.Contacts.GetPageAsync(1, 10, new ContactsFilter
            {
                Email = new Tuple<SearchVariant, string>(SearchVariant.StartsWith, "northerngirl87")
            });

            Assert.Equal(1, contacts.Count);
        }
    }
}

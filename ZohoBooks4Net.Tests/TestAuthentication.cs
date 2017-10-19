using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZohoBooks4Net.Tests
{
    public class TestAuthentication : TestBase
    {
        [Fact]
        public async void TestAuthenticateAsync()
        {
            var authClient = Client.Authentication;

            var authenticated = await authClient.Authenticate("brandon.james@ninelineapparel.com", "C0nn3ct3d!");

            Assert.Equal(true, authenticated);
        }
    }
}

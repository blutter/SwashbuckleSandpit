using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SwashbuckleSandpit.IntegrationTests
{
    public class ExampleTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ExampleTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetReturns200AndValue1Value2()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Values").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseJsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseArray = JArray.Parse(responseJsonString);
            Assert.Equal(2, responseArray.Count);
            Assert.Equal("value1", responseArray[0].Value<string>());
            Assert.Equal("value2", responseArray[1].Value<string>());
        }
    }
}

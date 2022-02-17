using CustomerPortal.Tests.Integration.Utilities;
using DataTransfer;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CustomerPortal.Tests.Integration
{
    public class CustomerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly string AddCustomerUrl = "/customers/add";
        private readonly string GetCustomerUrl = "/customers";

        public CustomerControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnCreatedCustomer()
        {
            var client = _factory.CreateClient();

            var request = new CustomerDetails
            {
                FirstName = "John",
                LastName = "Smith",
                ReferenceNumber = "AA-000000",
                DOB = DateTime.Now.AddYears(-20),
                Email = "jsmith@email.com"
            };

            var content = JsonUtility.ConvertToJsonContent(request);

            var response = await client.PostAsync(AddCustomerUrl, content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnBadRequestForInvalidRequest()
        {
            var client = _factory.CreateClient();

            var request = new CustomerDetails
            {
                FirstName = "JohnABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ",
                LastName = "SmithABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ",
                ReferenceNumber = "aa-aaaaaa",
                DOB = DateTime.Now.AddYears(-17).Date,
                Email = "jsh@email.fr"
            };

            var content = JsonUtility.ConvertToJsonContent(request);

            var response = await client.PostAsync(AddCustomerUrl, content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnBadRequestForEmptyRequest()
        {
            var client = _factory.CreateClient();

            var request = new CustomerDetails
            {
                FirstName = "",
                LastName = "",
                ReferenceNumber = "",
                DOB = null,
                Email = ""
            };

            var content = JsonUtility.ConvertToJsonContent(request);

            var response = await client.PostAsync(AddCustomerUrl, content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetCustomer_ShouldReturnOkForCustomer()
        {
            var client = _factory.CreateClient();

            var request = new CustomerDetails
            {
                FirstName = "John",
                LastName = "Smith",
                ReferenceNumber = "AA-000000",
                DOB = DateTime.Now.AddYears(-20),
                Email = "jsmith@email.com"
            };

            var content = JsonUtility.ConvertToJsonContent(request);

            var createdCustomer = await client.PostAsync(AddCustomerUrl, content);

            var getCustomer = await client.GetAsync($"{GetCustomerUrl}/1");

            getCustomer.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnNotFoundForNoCustomer()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"{GetCustomerUrl}/404");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}

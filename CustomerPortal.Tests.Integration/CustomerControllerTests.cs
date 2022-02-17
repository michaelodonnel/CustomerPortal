using CustomerPortal.Tests.Integration.Utilities;
using DataTransfer;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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

            var responseContent = await response.Content.ReadAsStringAsync();
            var parsedJson = JsonUtility.ConvertFromJsonString<ValidationProblemDetails>(responseContent);

            parsedJson.Errors.Should().ContainKey("FirstName");
            parsedJson.Errors["FirstName"].Should().BeEquivalentTo("The length of 'First Name' must be 50 characters or fewer. You entered 56 characters.");

            parsedJson.Errors.Should().ContainKey("LastName");
            parsedJson.Errors["LastName"].Should().BeEquivalentTo("The length of 'Last Name' must be 50 characters or fewer. You entered 57 characters.");

            parsedJson.Errors.Should().ContainKey("ReferenceNumber");
            parsedJson.Errors["ReferenceNumber"].Should().BeEquivalentTo("The specified condition was not met for 'Reference Number'.");

            parsedJson.Errors.Should().ContainKey("Email");
            parsedJson.Errors["Email"].Should().BeEquivalentTo("The specified condition was not met for 'Email'.");

            parsedJson.Errors.Should().ContainKey("DOB");
            parsedJson.Errors["DOB"].Should().BeEquivalentTo($"'DOB' must be less than or equal to '{DateTime.Now.AddYears(-18)}'.");
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

            var responseContent = await response.Content.ReadAsStringAsync();
            var parsedJson = JsonUtility.ConvertFromJsonString<ValidationProblemDetails>(responseContent);

            parsedJson.Errors.Should().ContainKey("FirstName");
            parsedJson.Errors["FirstName"].Should().BeEquivalentTo("The length of 'First Name' must be at least 3 characters. You entered 0 characters.");

            parsedJson.Errors.Should().ContainKey("LastName");
            parsedJson.Errors["LastName"].Should().BeEquivalentTo("The length of 'Last Name' must be at least 3 characters. You entered 0 characters.");

            parsedJson.Errors.Should().ContainKey("ReferenceNumber");
            parsedJson.Errors["ReferenceNumber"][0].Should().BeEquivalentTo("'Reference Number' must not be empty.");
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

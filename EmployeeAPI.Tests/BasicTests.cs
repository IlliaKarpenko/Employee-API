using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace EmployeeAPI.Tests
{
	public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
	{
		private readonly WebApplicationFactory<Program> _factory;

		public BasicTests(WebApplicationFactory<Program> factory)
		{
			_factory = factory;
		}

		[Fact]
		public async Task GetAllEmployees_ReturnsOkResult()
		{
			HttpClient client = _factory.CreateClient();
			var response = await client.GetAsync("/employees");

			response.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task GetEmployeeById_ReturnsOkResult()
		{
			HttpClient client = _factory.CreateClient();
			var response = await client.GetAsync("/employees/1");

			response.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task CreateEmployee_ReturnsCreatedResult()
		{
			HttpClient client = _factory.CreateClient();
			var response = await client.PostAsJsonAsync("/employees",
				new Employee
				{
					FirstName = "John",
					LastName = "Doe"
				});

			response.EnsureSuccessStatusCode();
		}

		[Fact]
		public async Task CreateEmployee_ReturnsBadRequestResult()
		{
			HttpClient client = _factory.CreateClient();
			var response = await client.PostAsJsonAsync("/employees", new { });

			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}
	}
}
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Moq;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.API;
using System.Net;
using Microsoft.EntityFrameworkCore;
using SchoolCanteen.DATA.DatabaseConnector;
using NUnit.Framework.Internal;

namespace SchoolCanteen.Test
{
    [TestFixture]
    public class UserControllerIntegrationTests
    {
        private WebApplicationFactory<Startup> _factory;
        private JwtTokenGenerator tokenGenerator;
        private HttpClient client;
        private string companyId;
        private IConfiguration Configuration { get; set; }

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
            var configurationBuilder = new ConfigurationBuilder()
                .AddUserSecrets<Startup>();

            Configuration = configurationBuilder.Build();
            tokenGenerator = new JwtTokenGenerator(Configuration);
            client = SetupTestClientWithMockTokenUtil();
            companyId = "08dc18c7-54cd-4746-8647-a6c19d7c127c";
        }


        [Test]
        public async Task GetAllAsync_AdminRole_ReturnsOk()
        {

            var token = tokenGenerator.GenerateJwtTokenWithAdminRole();



            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


            var response = await client.GetAsync("/api/users");


            response.EnsureSuccessStatusCode();

            var users = await response.Content.ReadFromJsonAsync<IEnumerable<SimpleUserDTO>>();
            Assert.NotNull(users);
        }
        [Test]
        public async Task GetAllAsync_UserRole_ReturnsUnauthorize()
        {

            var token = tokenGenerator.GenerateJwtTokenWithUserRole();



            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


            var response = await client.GetAsync("/api/users");


            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
        [Test]
        public async Task GetByNameAsync_AdminRole_ReturnsUser()
        {
            var token = tokenGenerator.GenerateJwtTokenWithAdminRole();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var userName = "user2";
            var response = await client.GetAsync($"/api/users?UserName={userName}");

            response.EnsureSuccessStatusCode();

            var users = await response.Content.ReadFromJsonAsync<IEnumerable<SimpleUserDTO>>();
            Assert.NotNull(users);
        }
/*        [Test]
        public async Task CreateAsync_AdminRole_CreatesUser()
        {
            var token = tokenGenerator.GenerateJwtTokenWithAdminRole();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var newUser = new CreateUserDTO
            {
                CompanyId = Guid.Parse(companyId),
                UserName = "userNameTests",
                Email = "tests@example.com",
                Password = "password",
                RoleName = "Admin",
            };
          
            var response = await client.PostAsJsonAsync("/api/users", newUser);


            response.EnsureSuccessStatusCode();


            var createdUser = await response.Content.ReadFromJsonAsync<SimpleUserDTO>();
            Assert.NotNull(createdUser);


            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
                var userInDatabase = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
                Assert.NotNull(userInDatabase);
            }
        }*/
        // IT WORKS BUT CREATE NEW USER TO REAL DATABASE
        //AT THE MOMENT I DON'T KNOW HOW TO CREATE MEMORY DATABASE FOR THIS



        private HttpClient SetupTestClientWithMockTokenUtil()
        {
            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<ITokenUtil>(provider =>
                    {
                        services.RemoveAll<ITokenUtil>();
                        var mock = new Mock<ITokenUtil>();

                        mock.Setup(util => util.GetIdentityCompany()).Returns(Guid.Parse(companyId));

                        return mock.Object;
                    });

                });
            }).CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
        }
    }
}

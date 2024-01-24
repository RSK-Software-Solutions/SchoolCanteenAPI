using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using SchoolCanteen.API;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.User;
using Moq;

[TestFixture]
public class UserControllerIntegrationTests
{
    private WebApplicationFactory<Startup> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Startup>();

    }

    [Test]
    public async Task GetAllAsync_AdminRole_ReturnsOk()
    {
        // Arrange
        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<ITokenUtil>(provider =>
                {
                    services.RemoveAll<ITokenUtil>();
                    var mock = new Mock<ITokenUtil>();


                    mock.Setup(util => util.GetIdentityCompany()).Returns(Guid.Parse("08dc18c7-54cd-4746-8647-a6c19d7c127c"));

                    return mock.Object;
                });

            });
        }).CreateClient();

        var token = GenerateJwtTokenWithAdminRole();



        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


        var response = await client.GetAsync("/api/users");


        response.EnsureSuccessStatusCode(); // Status Code 200-299

        var users = await response.Content.ReadFromJsonAsync<IEnumerable<SimpleUserDTO>>();
        Assert.NotNull(users);
    }

    private string GenerateJwtTokenWithAdminRole()
    {
        string validIssuer = "apiWithAuthBackend";
        string validAudience = "apiWithAuthBackend";
        string claimNameSub = "TokenForTheApiWithAuth";
        string issuerSigningKey = "!SomethingSecret!";

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, claimNameSub),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")),
            new Claim(ClaimTypes.Role, "Admin"), // Add admin role claim
            // Add other claims if needed
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = validIssuer,
            Audience = validAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30), // Set expiration time
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
                SecurityAlgorithms.HmacSha256
            ),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

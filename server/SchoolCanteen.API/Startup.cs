using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Repositories;
using SchoolCanteen.DATA.Repositories.Interfaces;
using SchoolCanteen.Logic.DTOs.AutoMapperProfiles;
using SchoolCanteen.Logic.Services;
using SchoolCanteen.Logic.Services.Authentication;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.Interfaces;
using SchoolCanteen.Logic.Services.Roles;
using System.Text;

namespace SchoolCanteen.API;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors();
        services.AddEndpointsApiExplorer();
        AddSwagger(services);
        services.AddAutoMapper(typeof(AutoMapperProfile));

        var connectionString = Configuration["ConnectionString"];
        var serverVersion = ServerVersion.Parse(Configuration["ServerVersion"]);

        services.AddDbContext<DatabaseApiContext>(options => options.UseMySql(connectionString, serverVersion));
        services.AddDbContext<UsersContext>(options => options.UseMySql(connectionString, serverVersion));

        AddAuthentication(services);
        AddIdentity(services);

        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IRolesService, RolesService>();
        services.AddScoped<IUserDetailsService, UserDetailsService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(o => o
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials()
            //.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();


        app.UseEndpoints(endpoints => 
        { 
            endpoints.MapControllers(); 
        });

        AddRoles(app);
    }

    private void AddAuthentication(IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var validIssuer = config["Authentication:ValidIssuer"];
        var validAudience = config["Authentication:ValidAudience"];
        var claimNameSub = config["Authentication:ClaimNameSub"];
        var issuerSigningKey = Configuration["Authentication:IssuerSigningKey"];

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(issuerSigningKey) 
                    )
                };
            });
    }

    private void AddIdentity(IServiceCollection services)
    {
        services
            .AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UsersContext>();
    }

    private void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "School Canteen API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    }, new string[]{ }
                }
            });
        });
    }

    void AddRoles(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope(); 
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var tAdmin = CreateRole(roleManager, "Admin");
        tAdmin.Wait();

        var tManager = CreateRole(roleManager, "Manager");
        tManager.Wait();

        var tUser = CreateRole(roleManager, "User");
        tUser.Wait();

    }

    private async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
    {
        var adminRoleExists = await roleManager.RoleExistsAsync(roleName);
        if (!adminRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

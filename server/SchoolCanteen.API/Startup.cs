using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.CompanyRepo;
using SchoolCanteen.DATA.Repositories.FinishedProductRepo;
using SchoolCanteen.DATA.Repositories.ProductRepo;
using SchoolCanteen.DATA.Repositories.UnitRepo;
using SchoolCanteen.Logic.DTOs.AutoMapperProfiles;
using SchoolCanteen.Logic.Services.Authentication;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.CompanyServices;
using SchoolCanteen.Logic.Services.FinishedProductServices;
using SchoolCanteen.Logic.Services.ProductServices;
using SchoolCanteen.Logic.Services.Roles;
using SchoolCanteen.Logic.Services.UnitServices;
using SchoolCanteen.Logic.Services.User;
using SchoolCanteen.Logic.Services.UserServices;
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
        services.AddHttpContextAccessor();

        AddSwagger(services);
        services.AddAutoMapper(typeof(AutoMapperProfile));

        var connectionString = Configuration["ConnectionString"];
        var serverVersion = ServerVersion.Parse(Configuration["ServerVersion"]);

        services.AddDbContext<DatabaseApiContext>(options => options.UseMySql(connectionString, serverVersion));
        services.AddDbContext<UsersContext>(options => options.UseMySql(connectionString, serverVersion));

        AddAuthentication(services);
        AddIdentity(services);

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITokenUtil, TokenUtil>();

        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        
        services.AddScoped<IFinishedProductService, FinishedProductService>();
        services.AddScoped<IFinishedProductRepository, FinishedProductRepository>();
        
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUnitBaseService, UnitBaseService>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddScoped<IUnitRepository, UnitRepository>();

        services.AddScoped<IRolesService, RolesService>();
        
        services.AddScoped<IAuthService, AuthService>();

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
            //.AllowCredentials()
            .AllowAnyOrigin()
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
            .AddIdentityCore<ApplicationUser>(options =>
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
            option.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "School Canteen API", 
                Version = "v1" 
            });

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

        var tSuperAdmin = CreateRole(roleManager, "SuperAdmin");
        tSuperAdmin.Wait();

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

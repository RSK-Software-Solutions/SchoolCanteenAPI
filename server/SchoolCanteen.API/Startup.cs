using Microsoft.EntityFrameworkCore;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Repositories;
using SchoolCanteen.DATA.Repositories.Interfaces;
using SchoolCanteen.Logic.DTOs.AutoMapperProfiles;
using SchoolCanteen.Logic.Services;
using SchoolCanteen.Logic.Services.Interfaces;

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
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(AutoMapperProfile));

        var connectionString = Configuration["ConnectionString"];
        var serverVersion = ServerVersion.Parse("10.6.12-mariadb");

        services.AddDbContext<DatabaseApiContext>(options => options.UseMySql(connectionString, serverVersion));

        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IRoleRepository,  RoleRepository>();
        services.AddScoped<IUserDetailsService, UserDetailsService>();
        services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
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

        app.UseEndpoints(endpoints => 
        { 
            endpoints.MapControllers(); 
        });
    }
}

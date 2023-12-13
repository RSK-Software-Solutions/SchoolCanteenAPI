using Microsoft.EntityFrameworkCore;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];
var serverVersion = ServerVersion.Parse("10.6.12-mariadb");

builder.Services.AddDbContext<DatabaseApiContext>(options => options
.UseMySql(connectionString, serverVersion));

builder.Services.AddScoped<ICompanyService, CompanyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS before other middleware
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
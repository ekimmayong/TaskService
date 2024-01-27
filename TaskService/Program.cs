using System.Text.Json.Serialization;
using System.Text.Json;
using TaskService.Infrastructure.Configurations;
using TaskService.Infrastructure.Extensions;
using TaskService.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Configuration IOptions
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(JwtConfiguration.ConfigurationName));

builder.Services.AddOptions<JwtConfiguration>()
    .Bind(builder.Configuration.GetSection(JwtConfiguration.ConfigurationName))
    .ValidateDataAnnotations();

// configure dbcontext
builder.Services.AddDbContextConfiguration();

// Implement dependency injections
builder.Services.AddApplicationDependencyInjection();
builder.Services.AddAuthorizationExtension(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    }); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API Service");
    });
}

// Custom middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

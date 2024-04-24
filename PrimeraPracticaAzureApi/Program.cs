using Microsoft.EntityFrameworkCore;
using PrimeraPracticaAzureApi.Data;
using PrimeraPracticaAzureApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddDbContext<PersonajesSeriesContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddTransient<PersonajesSeriesRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api Primera Practica Azure";
    document.Description = "Api del primer examen de Azure";
});

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json",
        name: "Api Primera Practica Azure");
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();

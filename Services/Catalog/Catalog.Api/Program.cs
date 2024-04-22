using Catalog.Api;
using Catalog.Application;
using Catalog.Infrastructure;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPresentation(builder.Configuration);



var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPresentation(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();
    new OrderContextSeed().SeedAsync(context, logger).Wait();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

using ProductCatalog.Api;
using ProductCatalog.Api.Middleware;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<HealthCheckMiddleware>();
app.UseMiddleware<LogRequestScope>();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.Run();

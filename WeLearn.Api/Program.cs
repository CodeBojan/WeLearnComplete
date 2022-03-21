using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

// Add services to the container.

var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHttpLogging(logging =>
{
    logging.LoggingFields =
    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPath
    | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestQuery
    | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProtocol
    | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestMethod;
});

var app = builder.Build();
var configuration = app.Configuration;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO refactor to extension method
if (configuration.GetSection("Authentication:Enabled").Get<bool>())
{
    // TODO
    app.UseAuthentication();
}
if (configuration.GetSection("Authorization:Enabled").Get<bool>())
{
    // TODO
    app.UseAuthorization();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello");

app.Run();

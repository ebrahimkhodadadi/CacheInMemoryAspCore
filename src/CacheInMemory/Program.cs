
using CacheService.Repository;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

    //Serilog.Log.Information("Starting VMS API");

    builder.Services.AddMemoryCache();
    builder.Services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();


    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    //Serilog.Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    //Serilog.Log.Information("Server Shutting down...");
    //Log.CloseAndFlush();
}
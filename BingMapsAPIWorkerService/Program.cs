using BingMapsAPIWorkerService;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(@"d:\TrafficWorkerService\LogFile.txt", shared: true)
    .CreateLogger();

try
{
    Log.Information("Starting up the service");

    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .UseSerilog()
        .Build();

    await host.RunAsync();
    return;
}
catch (Exception ex)
{
    Log.Fatal(ex, "There was a problem starting the service");
    return;
}
finally
{
    Log.CloseAndFlush();
}
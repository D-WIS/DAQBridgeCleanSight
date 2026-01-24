using DWIS.DAQBridge.CleanSight.Server;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<HostOptions>(options =>
{
    options.ServicesStartConcurrently = true;
    options.ServicesStopConcurrently = true;
});
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

using DWIS.DAQBridge.CleanSight.OPCUASource;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

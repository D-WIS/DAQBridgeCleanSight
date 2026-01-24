using DWIS.DAQBridge.CleanSight.MQTTSource;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

using DWIS.API.DTO;
using DWIS.Client.ReferenceImplementation;
using DWIS.Client.ReferenceImplementation.OPCFoundation;
using DWIS.RigOS.Common.Worker;
using DWIS.RigOS.Common.Model;
using MQTTnet;
using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using OSDC.DotnetLibraries.General.Statistics;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace DWIS.DAQBridge.CleanSight.Server
{
    public class Worker : DWISWorker
    {
        private CleanSightInputData InputData { get; } = new CleanSightInputData();

        private CleanSightOutputData OutputData { get; } = new CleanSightOutputData();

        public Worker(ILogger<IDWISWorker> logger, ILogger<DWISClientOPCF>? loggerDWISClient):base(logger, loggerDWISClient)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttFactory = new MqttClientFactory();
            _mqttClient = mqttFactory.CreateMqttClient();
            ConnectToBlackboard();
            if (Configuration is not null && _DWISClient != null && _DWISClient.Connected)
            {
                await SubscribeTopicsAndConnect(OutputData, stoppingToken);
                await RegisterQueries(InputData);
                await RegisterToBlackboard(OutputData);
                await Loop(stoppingToken);
            }
        }
        protected override async Task Loop(CancellationToken stoppingToken)
        {
            Random rnd = new Random();
            if (_mqttClient is not null)
            {
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {

                        await ReadBlackboardAsync(InputData, stoppingToken);
                        await PublishMQTTAsync(InputData, stoppingToken);
                        await PublishBlackboardAsync(OutputData, stoppingToken);

                        lock (_lock)
                        {
                            if (OutputData.OverallCuttingsRecovery is not null &&
                                OutputData.OverallCuttingsRecovery.Value is not null)
                            {
                                if (Logger is not null && Logger.IsEnabled(LogLevel.Information))
                                {
                                    Logger?.LogInformation("Overall Cuttings Recovery: " + OutputData.OverallCuttingsRecovery.Value.Value.ToString("F3"));
                                }
                            }
                            if (InputData.BlockPosition is not null && InputData.BlockPosition.Value is not null)
                            {
                                if (Logger is not null && Logger.IsEnabled(LogLevel.Information))
                                {
                                    Logger?.LogInformation("Block position: " + InputData.BlockPosition.Value.Value.ToString("F3"));
                                }
                            }
                        }

                        ConfigurationUpdater.Instance.UpdateConfiguration(this);
                        await Task.Delay(LoopSpan, stoppingToken);
                    }
                }
                finally
                {
                    if (_mqttClient.IsConnected)
                    {
                        await _mqttClient.DisconnectAsync();
                    }
                }
            }
        }      
    }
}

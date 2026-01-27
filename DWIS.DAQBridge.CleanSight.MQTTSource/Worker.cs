using DWIS.Client.ReferenceImplementation.OPCFoundation;
using MQTTnet;
using DWIS.RigOS.Common.Worker;

namespace DWIS.DAQBridge.CleanSight.MQTTSource
{
    public class Worker : DWISWorkerWithMQTT<ConfigurationForMQTT>
    {
        private CleanSightOperationData OperationData { get; } = new CleanSightOperationData();

        private CleanSightResults CleanSightResults { get; } = new CleanSightResults();

        public Worker(ILogger<IDWISWorker<ConfigurationForMQTT>> logger, ILogger<DWISClientOPCF>? loggerDWISClient) : base(logger, loggerDWISClient)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttFactory = new MqttClientFactory();
            _mqttClient = mqttFactory.CreateMqttClient();
            if (Configuration is not null)
            {
                await SubscribeTopicsAndConnect(OperationData, stoppingToken);
                await Loop(stoppingToken);
            }
        }

        protected override async Task Loop(CancellationToken stoppingToken)
        {
            if (_mqttClient is not null)
            {
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        FillRandomData(CleanSightResults);
                        await PublishMQTTAsync(CleanSightResults, stoppingToken);
                        lock (_lock)
                        {
                            if (CleanSightResults.OverallCuttingsRecovery is not null &&
                                CleanSightResults.OverallCuttingsRecovery.Value is not null)
                            {
                                if (Logger is not null && Logger.IsEnabled(LogLevel.Information))
                                {
                                    Logger?.LogInformation("Overall Cuttings Recovery: " + CleanSightResults.OverallCuttingsRecovery.Value.Value.ToString("F3"));
                                }
                            }
                        }

                        lock (_lock)
                        {
                            if (OperationData.BlockPosition is not null && OperationData.BlockPosition.Value is not null)
                            {
                                if (Logger is not null && Logger.IsEnabled(LogLevel.Information))
                                {
                                    Logger?.LogInformation("Block position: " + OperationData.BlockPosition.Value.Value.ToString("F3"));
                                }
                            }
                        }
                        ConfigurationUpdater<ConfigurationForMQTT>.Instance.UpdateConfiguration(this);
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

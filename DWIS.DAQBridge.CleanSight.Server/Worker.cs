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
    public class Worker : DWISWorkerWithMQTT<ConfigurationForMQTT>
    {
        private CleanSightInputData InputData { get; } = new CleanSightInputData();

        private CleanSightOutputData OutputData { get; } = new CleanSightOutputData();

        public Worker(ILogger<IDWISWorker<ConfigurationForMQTT>> logger, ILogger<DWISClientOPCF>? loggerDWISClient):base(logger, loggerDWISClient)
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

        protected override async Task SubscribeTopicsAndConnect(DWISDataWithMQTT data, CancellationToken stoppingToken)
        {
            if (_mqttClient is null || Configuration is null)
                return;

            Dictionary<string, UnitConversion>? unitConversions = null;
            if (Configuration is not null)
            {
                unitConversions = Configuration.UnitConversions;
            }
            // Ensure we do not wire handlers multiple times if this method is called again.
            _mqttClient.ApplicationMessageReceivedAsync -= (e) => MQTTCallBack(e, data, unitConversions);
            _mqttClient.ApplicationMessageReceivedAsync += (e) => MQTTCallBack(e, data, unitConversions);

            _mqttClient.ConnectedAsync += async _ =>
            {
                Logger?.LogInformation("MQTT connected. Subscribing topics...");

                var subscribeOptionsBuilder = new MqttClientSubscribeOptionsBuilder();
                data.SubscribeMqttTopics(topic => subscribeOptionsBuilder.WithTopicFilter(topic));

                var subOptions = subscribeOptionsBuilder.Build();
                var result = await _mqttClient.SubscribeAsync(subOptions, stoppingToken);

                // Log SUBACK (very useful in containers)
                Logger?.LogInformation("MQTT subscribe result: {Result}", result?.Items?.Count);
            };

            _mqttClient.DisconnectedAsync += async e =>
            {
                Logger?.LogWarning("MQTT disconnected: {Reason} {Exception}",
                    e.Reason, e.Exception?.Message);

                if (stoppingToken.IsCancellationRequested)
                    return;

                // Simple retry loop/backoff
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);

                try
                {
                    await _mqttClient.ConnectAsync(BuildMqttOptions(), stoppingToken);
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "MQTT reconnect failed");
                }
            };

            await _mqttClient.ConnectAsync(BuildMqttOptions(), stoppingToken);
        }

        protected override Task MQTTCallBack(MqttApplicationMessageReceivedEventArgs e, DWISDataWithMQTT data, Dictionary<string, UnitConversion>? unitConversions)
        {
            var topic = e.ApplicationMessage.Topic;
            Logger?.LogInformation("Data received on topic: " + topic);
            if (string.IsNullOrWhiteSpace(topic))
            {
                return Task.CompletedTask;
            }

            var payload = e.ApplicationMessage.Payload;
            Logger?.LogInformation($"Payload received of length: " + payload.Length);
            if (payload.IsEmpty)
            {
                return Task.CompletedTask;
            }
            try
            {
                var payloadText = Encoding.UTF8.GetString(payload);
                Logger?.LogInformation("Payload: " + payloadText);
                if (data is CleanSightOutputData outputData)
                {
                    lock (_lock)
                    {
                        outputData.TryApplyMqttValue(topic, payloadText, unitConversions, true, Logger);
                    }
                }
                else
                {
                    lock (_lock)
                    {
                        data.TryApplyMqttValue(topic, payloadText, unitConversions, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex.ToString());
            }
            return Task.CompletedTask;
        }

        private MqttClientOptions BuildMqttOptions()
        {
            // Unique per container instance. If you want persistent sessions, make it stable-but-unique.
            var clientId = $"{Environment.MachineName}-{Guid.NewGuid():N}";

            return new MqttClientOptionsBuilder()
                .WithClientId(clientId)
                .WithTcpServer(Configuration!.MQTTServer, Configuration!.MQTTPort)
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(15))
                // Consider explicitly setting clean session semantics:
                // .WithCleanSession(true)
                .Build();
        }
    }
}

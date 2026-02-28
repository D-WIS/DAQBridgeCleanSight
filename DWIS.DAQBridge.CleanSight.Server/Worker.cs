using DWIS.Client.ReferenceImplementation.OPCFoundation;
using DWIS.RigOS.Common.Worker;
using MQTTnet;
using DWIS.DAQBridge.CleanSight.Model;

namespace DWIS.DAQBridge.CleanSight.Server
{

    public class Worker : DWISWorkerWithMQTT<ConfigurationForCleanSight, CompactData>
    {
        private static string Prefix { get; } = "CleanSightData";
        private CleanSightInputDataForQueries InputData { get; } = new CleanSightInputDataForQueries();

        private CleanSightRawData RawData { get; } = new CleanSightRawData();
        private CleanSightOutputData OutputData { get; } = new CleanSightOutputData();

        public Worker(ILogger<IDWISWorker<ConfigurationForCleanSight>> logger, ILogger<DWISClientOPCF>? loggerDWISClient):base(logger, loggerDWISClient)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttFactory = new MqttClientFactory();
            _mqttClient = mqttFactory.CreateMqttClient();
            ConnectToBlackboard();
            if (Configuration is not null && _DWISClient != null && _DWISClient.Connected)
            {
                await SubscribeTopicsAndConnect(RawData, stoppingToken);
                await RegisterQueries(InputData);
                await RegisterToBlackboard(OutputData, true);
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

                        try
                        {
                            await ReadBlackboardAsync(InputData, stoppingToken);
                            await PublishMQTTAsync(InputData, stoppingToken);
                            await TransferRawData(RawData, OutputData);
                            await PublishBlackboardAsync(OutputData, stoppingToken);
                        }
                        catch (Exception ex)
                        {
                            Logger?.LogError(ex.ToString());
                        }

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
                        await TryDumpProcessLogIfDueAsync(Prefix, stoppingToken);
                        ConfigurationUpdater<ConfigurationForCleanSight>.Instance.UpdateConfiguration(this);
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
                await ForceDumpProcessLogAsync(Prefix);
            }
        }

        protected override CompactData? CreateSample(DateTimeOffset timestampUtc, ILogger<IDWISWorker<ConfigurationForCleanSight>>? logger)
        {
            CompactData data = new CompactData();
            try
            {
                data.Transfer(RawData, timestampUtc);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex.ToString());
            }
            return data;
        }

        protected virtual async Task TransferRawData(CleanSightRawData rawData, CleanSightOutputData cleanSightOutputData)
        {
            try
            {
                if (rawData.ShakerLoadEstimates is not null)
                {
                    cleanSightOutputData.ShakerLoadEstimates ??= new ScalarProperty();
                    cleanSightOutputData.ShakerLoadEstimates.Value = rawData.ShakerLoadEstimates.Value;
                }
                if (rawData.AverageShakerLoadEstimate is not null)
                {
                    cleanSightOutputData.AverageShakerLoadEstimate ??= new ScalarProperty();
                    cleanSightOutputData.AverageShakerLoadEstimate.Value = rawData.AverageShakerLoadEstimate.Value;
                }
                if (rawData.CuttingsRecoveryRates is not null)
                {
                    cleanSightOutputData.CuttingsRecoveryRates ??= new ScalarProperty();
                    cleanSightOutputData.CuttingsRecoveryRates.Value = rawData.CuttingsRecoveryRates.Value;
                }
                if (rawData.AccumulatedCuttingsRecovery is not null)
                {
                    cleanSightOutputData.AccumulatedCuttingsRecovery ??= new ScalarProperty();
                    cleanSightOutputData.AccumulatedCuttingsRecovery.Value = rawData.AccumulatedCuttingsRecovery.Value;
                }
                if (rawData.OverallCuttingsRecovery is not null)
                {
                    cleanSightOutputData.OverallCuttingsRecovery ??= new ScalarProperty();
                    cleanSightOutputData.OverallCuttingsRecovery.Value = rawData.OverallCuttingsRecovery.Value;
                }
                if (rawData.TotalTheoreticalCuttingsVolume is not null)
                {
                    cleanSightOutputData.TotalTheoreticalCuttingsVolume ??= new ScalarProperty();
                    cleanSightOutputData.TotalTheoreticalCuttingsVolume.Value = rawData.TotalTheoreticalCuttingsVolume.Value;
                }
                if (rawData.MaxCavingSizes is not null)
                {
                    cleanSightOutputData.MaxCavingSizes ??= new ScalarProperty();
                    cleanSightOutputData.MaxCavingSizes.Value = rawData.MaxCavingSizes.Value;
                }
                if (rawData.OverallMaxCavingSize is not null)
                {
                    cleanSightOutputData.OverallMaxCavingSize ??= new ScalarProperty();
                    cleanSightOutputData.OverallMaxCavingSize.Value = rawData.OverallMaxCavingSize.Value;
                }
                if (rawData.OverallAverageCuttingSize is not null)
                {
                    cleanSightOutputData.OverallAverageCuttingSize ??= new ScalarProperty();
                    cleanSightOutputData.OverallAverageCuttingSize.Value = rawData.OverallAverageCuttingSize.Value;
                }
                if (rawData.ShakerLoadDistribution01A is not null &&
                    rawData.ShakerLoadDistribution02A is not null &&
                    rawData.ShakerLoadDistribution03A is not null &&
                    rawData.ShakerLoadDistribution04A is not null &&
                    rawData.ShakerLoadDistribution05A is not null &&
                    rawData.ShakerLoadDistribution06A is not null &&
                    rawData.ShakerLoadDistribution07A is not null &&
                    rawData.ShakerLoadDistribution08A is not null &&
                    rawData.ShakerLoadDistribution09A is not null &&
                    rawData.ShakerLoadDistribution10A is not null &&
                    rawData.ShakerLoadDistribution01B is not null &&
                    rawData.ShakerLoadDistribution02B is not null &&
                    rawData.ShakerLoadDistribution03B is not null &&
                    rawData.ShakerLoadDistribution04B is not null &&
                    rawData.ShakerLoadDistribution05B is not null &&
                    rawData.ShakerLoadDistribution06B is not null &&
                    rawData.ShakerLoadDistribution07B is not null &&
                    rawData.ShakerLoadDistribution08B is not null &&
                    rawData.ShakerLoadDistribution09B is not null &&
                    rawData.ShakerLoadDistribution10B is not null &&
                    rawData.ShakerLoadDistribution01A.Value is not null &&
                    rawData.ShakerLoadDistribution02A.Value is not null &&
                    rawData.ShakerLoadDistribution03A.Value is not null &&
                    rawData.ShakerLoadDistribution04A.Value is not null &&
                    rawData.ShakerLoadDistribution05A.Value is not null &&
                    rawData.ShakerLoadDistribution06A.Value is not null &&
                    rawData.ShakerLoadDistribution07A.Value is not null &&
                    rawData.ShakerLoadDistribution08A.Value is not null &&
                    rawData.ShakerLoadDistribution09A.Value is not null &&
                    rawData.ShakerLoadDistribution10A.Value is not null &&
                    rawData.ShakerLoadDistribution01B.Value is not null &&
                    rawData.ShakerLoadDistribution02B.Value is not null &&
                    rawData.ShakerLoadDistribution03B.Value is not null &&
                    rawData.ShakerLoadDistribution04B.Value is not null &&
                    rawData.ShakerLoadDistribution05B.Value is not null &&
                    rawData.ShakerLoadDistribution06B.Value is not null &&
                    rawData.ShakerLoadDistribution07B.Value is not null &&
                    rawData.ShakerLoadDistribution08B.Value is not null &&
                    rawData.ShakerLoadDistribution09B.Value is not null &&
                    rawData.ShakerLoadDistribution10B.Value is not null)
                {
                    cleanSightOutputData.ShakerLoadDistributions ??= new HistogramsProperty();
                    cleanSightOutputData.ShakerLoadDistributions.Values ??= new List<Histogram>();
                    cleanSightOutputData.ShakerLoadDistributions.Values.Clear();
                    Histogram histogramShakerA = new Histogram();
                    histogramShakerA.Bins ??= new double[10];
                    histogramShakerA.Bins[0] = rawData.ShakerLoadDistribution01A.Value.Value;
                    histogramShakerA.Bins[1] = rawData.ShakerLoadDistribution02A.Value.Value;
                    histogramShakerA.Bins[2] = rawData.ShakerLoadDistribution03A.Value.Value;
                    histogramShakerA.Bins[3] = rawData.ShakerLoadDistribution04A.Value.Value;
                    histogramShakerA.Bins[4] = rawData.ShakerLoadDistribution05A.Value.Value;
                    histogramShakerA.Bins[5] = rawData.ShakerLoadDistribution06A.Value.Value;
                    histogramShakerA.Bins[6] = rawData.ShakerLoadDistribution07A.Value.Value;
                    histogramShakerA.Bins[7] = rawData.ShakerLoadDistribution08A.Value.Value;
                    histogramShakerA.Bins[8] = rawData.ShakerLoadDistribution09A.Value.Value;
                    histogramShakerA.Bins[9] = rawData.ShakerLoadDistribution10A.Value.Value;
                    cleanSightOutputData.ShakerLoadDistributions.Values.Add(histogramShakerA);
                    Histogram histogramShakerB = new Histogram();
                    histogramShakerB.Bins ??= new double[10];
                    histogramShakerB.Bins[0] = rawData.ShakerLoadDistribution01B.Value.Value;
                    histogramShakerB.Bins[1] = rawData.ShakerLoadDistribution02B.Value.Value;
                    histogramShakerB.Bins[2] = rawData.ShakerLoadDistribution03B.Value.Value;
                    histogramShakerB.Bins[3] = rawData.ShakerLoadDistribution04B.Value.Value;
                    histogramShakerB.Bins[4] = rawData.ShakerLoadDistribution05B.Value.Value;
                    histogramShakerB.Bins[5] = rawData.ShakerLoadDistribution06B.Value.Value;
                    histogramShakerB.Bins[6] = rawData.ShakerLoadDistribution07B.Value.Value;
                    histogramShakerB.Bins[7] = rawData.ShakerLoadDistribution08B.Value.Value;
                    histogramShakerB.Bins[8] = rawData.ShakerLoadDistribution09B.Value.Value;
                    histogramShakerB.Bins[9] = rawData.ShakerLoadDistribution10B.Value.Value;
                    cleanSightOutputData.ShakerLoadDistributions.Values.Add(histogramShakerB);
                }
            } catch (Exception ex)
            {
                Logger?.LogError(ex.ToString());
            }
            await Task.Delay(1);
        }
    }
}

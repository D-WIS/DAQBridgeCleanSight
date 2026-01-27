using DWIS.Client.ReferenceImplementation.OPCFoundation;
using DWIS.RigOS.Common.Worker;

namespace DWIS.DAQBridge.CleanSight.OPCUASource
{
    public class Worker : DWISWorker<Configuration>
    {
        private CleanSightOperationData OperationData { get; set; } = new CleanSightOperationData();
        private CleanSightResults Results { get; set; } = new CleanSightResults();

        public Worker(ILogger<IDWISWorker<Configuration>> logger, ILogger<DWISClientOPCF>? loggerDWISClient) : base(logger, loggerDWISClient)
        {
        }
 
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectToBlackboard();
            if (_DWISClient != null && _DWISClient.Connected)
            {
                await RegisterQueries(Results);
                await RegisterToBlackboard(OperationData);
                await Loop(stoppingToken);
            }
        }

        protected override async Task Loop(CancellationToken cancellationToken)
        {
            PeriodicTimer timer = new PeriodicTimer(LoopSpan);

            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                // do some stuff
                FillRandomData(OperationData);
                await PublishBlackboardAsync(OperationData, cancellationToken);
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
                await ReadBlackboardAsync(Results, cancellationToken);
                if (Logger is not null && Logger.IsEnabled(LogLevel.Information) && Results.OverallCuttingsRecovery is not null)
                {
                    Logger.LogInformation("Overall cuttings recovery: " + Results.OverallCuttingsRecovery.Value);
                }
            }
        }

    }
}

using DWIS.RigOS.Common.Worker;

namespace DWIS.DAQBridge.CleanSight.Model
{
    public class ConfigurationForCleanSight : ConfigurationForMQTT
    {
        public TimeSpan TimeIntervalDataDumpShort { get; set; } = TimeSpan.FromMinutes(5);
        public TimeSpan TimeIntervalDataDumpLong { get; set; } = TimeSpan.FromHours(1);
        public bool UseDataDump { get; set; } = true;
    }
}

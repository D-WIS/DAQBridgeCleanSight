using DWIS.API.DTO;
using DWIS.RigOS.Common.Worker;
using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace DWIS.DAQBridge.CleanSight.Model
{
    public class CleanSightRawData : DWISDataWithMQTT<ConfigurationForCleanSight>
    {
        private static readonly Lazy<IReadOnlyDictionary<string, PropertyInfo>> LocalTopicPropertyMap = new(BuildTopicPropertyMap(typeof(CleanSightRawData)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> LocalSparQLQueries = new(BuildSparQLQueries(typeof(CleanSightRawData)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> LocalManifests = new(BuildManifests(typeof(CleanSightRawData), "CleanSightRawData", "DrillDocs", "DWISBridge"));
        public override Lazy<IReadOnlyDictionary<string, PropertyInfo>> TopicPropertyMap { get => LocalTopicPropertyMap; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> SparQLQueries { get => LocalSparQLQueries; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> Manifests { get => LocalManifests; }

        [MQTTTopic("DWIS/ComputedData/DimensionLessStandard/CuttingSeparatorLogical/ShakerLoadEstimates")]
        public ScalarProperty? ShakerLoadEstimates { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/DimensionLessStandard/CuttingSeparatorLogical/AverageShakerLoadEstimate")]
        public ScalarProperty? AverageShakerLoadEstimate { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/VolumetricFlowrateDrilling/CuttingSeparatorLogical/AccumulatedCuttingsRecoveryRates")]
        public ScalarProperty? CuttingsRecoveryRates { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/AccumulatedCuttingsRecovery")]
        public ScalarProperty? AccumulatedCuttingsRecovery { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/OverallCuttingsRecovery")]
        public ScalarProperty? OverallCuttingsRecovery { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/TotalTheoreticalCuttingsVolume")]
        public ScalarProperty? TotalTheoreticalCuttingsVolume { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/MaxCavingSizes")]
        public ScalarProperty? MaxCavingSizes { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/OverallMaxCavingSize")]
        public ScalarProperty? OverallMaxCavingSize { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/OverallAverageCuttingSize")]
        public ScalarProperty? OverallAverageCuttingSize { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z01_1")]
        public ScalarProperty? ShakerLoadDistribution01A { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z02_1")]
        public ScalarProperty? ShakerLoadDistribution02A { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z03_1")]
        public ScalarProperty? ShakerLoadDistribution03A { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z04_1")]
        public ScalarProperty? ShakerLoadDistribution04A { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z05_1")]
        public ScalarProperty? ShakerLoadDistribution05A { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z06_1")]
        public ScalarProperty? ShakerLoadDistribution06A { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z07_1")]
        public ScalarProperty? ShakerLoadDistribution07A { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z08_1")]
        public ScalarProperty? ShakerLoadDistribution08A { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z09_1")]
        public ScalarProperty? ShakerLoadDistribution09A { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z10_1")]
        public ScalarProperty? ShakerLoadDistribution10A { get; set; } = null;


        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z01_2")]
        public ScalarProperty? ShakerLoadDistribution01B { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z02_2")]
        public ScalarProperty? ShakerLoadDistribution02B { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z03_2")]
        public ScalarProperty? ShakerLoadDistribution03B { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z04_2")]
        public ScalarProperty? ShakerLoadDistribution04B { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z05_2")]
        public ScalarProperty? ShakerLoadDistribution05B { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z06_2")]
        public ScalarProperty? ShakerLoadDistribution06B { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z07_2")]
        public ScalarProperty? ShakerLoadDistribution07B { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z08_2")]
        public ScalarProperty? ShakerLoadDistribution08B { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z09_2")]
        public ScalarProperty? ShakerLoadDistribution09B { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions/sle_z10_2")]
        public ScalarProperty? ShakerLoadDistribution10B { get; set; } = null;

    }
}

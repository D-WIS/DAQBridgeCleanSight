using DWIS.API.DTO;
using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using System.Reflection;
using DWIS.RigOS.Common.Worker;

namespace DWIS.DAQBridge.CleanSight.MQTTSource
{
    internal class CleanSightResults : DWISData
    {
        private static readonly Lazy<IReadOnlyDictionary<string, PropertyInfo>> LocalTopicPropertyMap = new(BuildTopicPropertyMap(typeof(CleanSightResults)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> LocalSparQLQueries = new(BuildSparQLQueries(typeof(CleanSightResults)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> LocalManifests = new(BuildManifests(typeof(CleanSightResults), "CleanSightResultsManifest", "DrillDocs", "DWISBridge"));
        public override Lazy<IReadOnlyDictionary<string, PropertyInfo>> TopicPropertyMap { get => LocalTopicPropertyMap; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> SparQLQueries { get => LocalSparQLQueries; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> Manifests { get => LocalManifests; }

        [MQTTTopic("DWIS/ComputedData/DimensionLessStandard/CuttingSeparatorLogical/ShakerLoadEstimates")]
        public GaussianValuesProperty? ShakerLoadEstimates { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/DimensionLessStandard/CuttingSeparatorLogical/AverageShakerLoadEstimate")]
        public ScalarProperty? AverageShakerLoadEstimate { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/VolumetricFlowrateDrilling/CuttingSeparatorLogical/AccumulatedCuttingsRecoveryRates")]
        public GaussianValuesProperty? AccumulatedCuttingsRecoveryRates { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/AccumulatedCuttingsRecovery")]
        public GaussianValuesProperty? AccumulatedCuttingsRecovery { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/OverallCuttingsRecovery")]
        public ScalarProperty? OverallCuttingsRecovery { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/TotalTheoreticalCuttingsVolume")]
        public ScalarProperty? TotalTheoreticalCuttingsVolume { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions")]
        public HistogramsProperty? ShakerLoadDistributions { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/MaxCavingSizes")]
        public GaussianValuesProperty? MaxCavingSizes { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [MQTTTopic("DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/OverallMaxCavingSize")]
        public ScalarProperty? OverallMaxCavingSize { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/OverallAverageCuttingSize")]
        public ScalarProperty? OverallAverageCuttingSize { get; set; } = null;
    }
}

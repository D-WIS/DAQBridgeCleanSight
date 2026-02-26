using DWIS.API.DTO;
using DWIS.Vocabulary.Schemas;
using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using OSDC.UnitConversion.Conversion;
using OSDC.UnitConversion.Conversion.DrillingEngineering;
using System.Reflection;
using DWIS.RigOS.Common.Worker;
using System.Text.Json;

namespace DWIS.DAQBridge.CleanSight.Server
{
    public class CleanSightOutputData : DWISDataWithMQTT
    {
        private static readonly Lazy<IReadOnlyDictionary<string, PropertyInfo>> LocalTopicPropertyMap = new(BuildTopicPropertyMap(typeof(CleanSightOutputData)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> LocalSparQLQueries = new(BuildSparQLQueries(typeof(CleanSightOutputData)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> LocalManifests = new(BuildManifests(typeof(CleanSightOutputData), "CleanSightOutputDataManifest", "DrillDocs", "DWISBridge"));
        public override Lazy<IReadOnlyDictionary<string, PropertyInfo>> TopicPropertyMap { get => LocalTopicPropertyMap; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> SparQLQueries { get => LocalSparQLQueries; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> Manifests { get => LocalManifests; }

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticStringVariable("CleanSightShakerLoadEstimate")]
        [SemanticFact("CleanSightShakerLoadEstimate", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightShakerLoadEstimate#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightShakerLoadEstimate#01", Nouns.Enum.JSonDataType)]
        [SemanticFact("CleanSightShakerLoadEstimate#01", Verbs.Enum.HasDynamicValue, "CleanSightShakerLoadEstimate")]
        [SemanticFact("CleanSightShakerLoadEstimate#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.DimensionLessStandard)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightShakerLoadEstimate#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingAverageCleanSightShakerLoadEstimate", Nouns.Enum.MovingAverage)]
        [SemanticFact("CleanSightShakerLoadEstimate#01", Verbs.Enum.IsTransformationOutput, "movingAverageCleanSightShakerLoadEstimate")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightShakerLoadEstimate#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("ImageInterpreter#01", Nouns.Enum.Interpreter)]
        [SemanticFact("CleanSightShakerLoadEstimate#01", Verbs.Enum.IsComputedBy, "ImageInterpreter#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightShakerLoadEstimate#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/DimensionLessStandard/CuttingSeparatorLogical/ShakerLoadEstimates")]
        public GaussianValuesProperty? ShakerLoadEstimates { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("CleanSightAverageShakerLoadEstimate")]
        [SemanticFact("CleanSightAverageShakerLoadEstimate", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Verbs.Enum.HasDynamicValue, "CleanSightAverageShakerLoadEstimate")]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.DimensionLessStandard)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingAverageCleanSightAverageShakerLoadEstimate", Nouns.Enum.MovingAverage)]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Verbs.Enum.IsTransformationOutput, "movingAverageCleanSightAverageShakerLoadEstimate")]
        [SemanticFact("GaussianUncertaintyCleanSightAverageShakerLoadEstimate#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Verbs.Enum.HasUncertainty, "GaussianUncertaintyCleanSightAverageShakerLoadEstimate#01")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("ImageInterpreter#01", Nouns.Enum.Interpreter)]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Verbs.Enum.IsComputedBy, "ImageInterpreter#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightAverageShakerLoadEstimate#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/DimensionLessStandard/CuttingSeparatorLogical/AverageShakerLoadEstimate")]
        public ScalarProperty? AverageShakerLoadEstimate { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticStringVariable("CleanSightCuttingsRecoveryRate")]
        [SemanticFact("CleanSightCuttingsRecoveryRate", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Nouns.Enum.JSonDataType)]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Verbs.Enum.HasDynamicValue, "CleanSightCuttingsRecoveryRate")]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Verbs.Enum.IsOfMeasurableQuantity, DrillingPhysicalQuantity.QuantityEnum.VolumetricFlowrateDrilling)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingAverageCleanSightCuttingsRecoveryRate", Nouns.Enum.MovingAverage)]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Verbs.Enum.IsTransformationOutput, "movingAverageCleanSightCuttingsRecoveryRate")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("Cuttings#01", Nouns.Enum.CuttingsComponent)]
        [SemanticFact("Cuttings#01", Verbs.Enum.IsAComponentOf, "DrillingFluid#01")]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Verbs.Enum.ConcernsAFluidComponent, "Cuttings#01")]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("ImageInterpreter#01", Nouns.Enum.Interpreter)]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Verbs.Enum.IsComputedBy, "ImageInterpreter#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightCuttingsRecoveryRate#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/VolumetricFlowrateDrilling/CuttingSeparatorLogical/AccumulatedCuttingsRecoveryRates")]
        public GaussianValuesProperty? CuttingsRecoveryRates { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticStringVariable("CleanSightAccumulatedCuttingsRecovery")]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Nouns.Enum.JSonDataType)]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Verbs.Enum.HasDynamicValue, "CleanSightAccumulatedCuttingsRecovery")]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Verbs.Enum.IsOfMeasurableQuantity, DrillingPhysicalQuantity.QuantityEnum.VolumeDrilling)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingAverageCleanSightAccumulatedCuttingsRecovery", Nouns.Enum.MovingAverage)]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Verbs.Enum.IsTransformationOutput, "movingAverageCleanSightAccumulatedCuttingsRecovery")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("Cuttings#01", Nouns.Enum.CuttingsComponent)]
        [SemanticFact("Cuttings#01", Verbs.Enum.IsAComponentOf, "DrillingFluid#01")]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Verbs.Enum.ConcernsAFluidComponent, "Cuttings#01")]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("ImageInterpreter#01", Nouns.Enum.Interpreter)]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Verbs.Enum.IsComputedBy, "ImageInterpreter#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightAccumulatedCuttingsRecovery#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/AccumulatedCuttingsRecovery")]
        public GaussianValuesProperty? AccumulatedCuttingsRecovery { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("CleanSightTotalAccumulatedCuttingsRecovery")]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Verbs.Enum.HasDynamicValue, "CleanSightTotalAccumulatedCuttingsRecovery")]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsOfMeasurableQuantity, DrillingPhysicalQuantity.QuantityEnum.VolumeDrilling)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingAverageCleanSightTotalAccumulatedCuttingsRecovery", Nouns.Enum.MovingAverage)]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsTransformationOutput, "movingAverageCleanSightTotalAccumulatedCuttingsRecovery")]
        [SemanticFact("GaussianUncertaintyCleanSightTotalAccumulatedCuttingsRecovery#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Verbs.Enum.HasUncertainty, "GaussianUncertaintyCleanSightTotalAccumulatedCuttingsRecovery#01")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("Cuttings#01", Nouns.Enum.CuttingsComponent)]
        [SemanticFact("Cuttings#01", Verbs.Enum.IsAComponentOf, "DrillingFluid#01")]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Verbs.Enum.ConcernsAFluidComponent, "Cuttings#01")]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("ImageInterpreter#01", Nouns.Enum.Interpreter)]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsComputedBy, "ImageInterpreter#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightTotalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/OverallCuttingsRecovery")]
        public ScalarProperty? OverallCuttingsRecovery { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery")]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Verbs.Enum.HasDynamicValue, "CleanSightTotalTheoreticalAccumulatedCuttingsRecovery")]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsOfMeasurableQuantity, DrillingPhysicalQuantity.QuantityEnum.VolumeDrilling)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingAverageCleanSightTotalTheoreticalAccumulatedCuttingsRecovery", Nouns.Enum.MovingAverage)]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsTransformationOutput, "movingAverageCleanSightTotalTheoreticalAccumulatedCuttingsRecovery")]
        [SemanticFact("GaussianUncertaintyCleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Verbs.Enum.HasUncertainty, "GaussianUncertaintyCleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("Cuttings#01", Nouns.Enum.CuttingsComponent)]
        [SemanticFact("Cuttings#01", Verbs.Enum.IsAComponentOf, "DrillingFluid#01")]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Verbs.Enum.ConcernsAFluidComponent, "Cuttings#01")]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("drillingHydraulicModel#01", Nouns.Enum.DrillingHydraulicModel)]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsComputedBy, "drillingHydraulicModel#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightTotalTheoreticalAccumulatedCuttingsRecovery#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/TotalTheoreticalCuttingsVolume")]
        public ScalarProperty? TotalTheoreticalCuttingsVolume { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticStringVariable("CleanSightShakerLoadDistribution")]
        [SemanticFact("CleanSightShakerLoadDistribution", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightShakerLoadDistribution#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightShakerLoadDistribution#01", Nouns.Enum.JSonDataType)]
        [SemanticFact("CleanSightShakerLoadDistribution#01", Verbs.Enum.HasDynamicValue, "CleanSightShakerLoadDistribution")]
        [SemanticFact("CleanSightShakerLoadDistribution#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.ProportionStandard)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightShakerLoadDistribution#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingDistributionCleanSightShakerLoadDistribution", Nouns.Enum.MovingDistribution)]
        [SemanticFact("CleanSightShakerLoadDistribution#01", Verbs.Enum.IsTransformationOutput, "movingDistributionCleanSightShakerLoadDistribution")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightShakerLoadDistribution#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("ImageInterpreter#01", Nouns.Enum.Interpreter)]
        [SemanticFact("CleanSightShakerLoadDistribution#01", Verbs.Enum.IsComputedBy, "ImageInterpreter#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightShakerLoadDistribution#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions")]
        public HistogramsProperty? ShakerLoadDistributions { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticStringVariable("CleanSightMaxCavingSize")]
        [SemanticFact("CleanSightMaxCavingSize", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightMaxCavingSize#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightMaxCavingSize#01", Nouns.Enum.JSonDataType)]
        [SemanticFact("CleanSightMaxCavingSize#01", Verbs.Enum.HasDynamicValue, "CleanSightMaxCavingSize")]
        [SemanticFact("CleanSightMaxCavingSize#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.LengthSmall)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightMaxCavingSize#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingMaxCleanSightMaxCavingSize", Nouns.Enum.MovingMax)]
        [SemanticFact("CleanSightMaxCavingSize#01", Verbs.Enum.IsTransformationOutput, "movingMaxCleanSightMaxCavingSize")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("Cuttings#01", Nouns.Enum.CuttingsComponent)]
        [SemanticFact("Cuttings#01", Verbs.Enum.IsAComponentOf, "DrillingFluid#01")]
        [SemanticFact("CleanSightMaxCavingSize#01", Verbs.Enum.ConcernsAFluidComponent, "Cuttings#01")]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightMaxCavingSize#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("ImageInterpreter#01", Nouns.Enum.Interpreter)]
        [SemanticFact("CleanSightMaxCavingSize#01", Verbs.Enum.IsComputedBy, "ImageInterpreter#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightMaxCavingSize#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/MaxCavingSizes")]
        public GaussianValuesProperty? MaxCavingSizes { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("CleanSightOverallMaxCavingSize")]
        [SemanticFact("CleanSightOverallMaxCavingSize", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Verbs.Enum.HasDynamicValue, "CleanSightOverallMaxCavingSize")]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.LengthSmall)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingMaxCleanSightOverallMaxCavingSize", Nouns.Enum.MovingMax)]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Verbs.Enum.IsTransformationOutput, "movingMaxCleanSightOverallMaxCavingSize")]
        [SemanticFact("GaussianUncertaintyCleanSightOverallMaxCavingSize#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Verbs.Enum.HasUncertainty, "GaussianUncertaintyCleanSightOverallMaxCavingSize#01")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("Cuttings#01", Nouns.Enum.CuttingsComponent)]
        [SemanticFact("Cuttings#01", Verbs.Enum.IsAComponentOf, "DrillingFluid#01")]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Verbs.Enum.ConcernsAFluidComponent, "Cuttings#01")]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("ImageInterpreter#01", Nouns.Enum.Interpreter)]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Verbs.Enum.IsComputedBy, "ImageInterpreter#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightOverallMaxCavingSize#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/OverallMaxCavingSize")]
        public ScalarProperty? OverallMaxCavingSize { get; set; } = null;

        [AccessToVariable(CommonProperty.VariableAccessType.Assignable)]
        [Mandatory(CommonProperty.MandatoryType.General)]
        [SemanticDiracVariable("CleanSightOverallAverageCuttingSize")]
        [SemanticFact("CleanSightOverallAverageCuttingSize", Nouns.Enum.DynamicDrillingSignal)]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Nouns.Enum.ComputedData)]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Nouns.Enum.ContinuousDataType)]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Verbs.Enum.HasDynamicValue, "CleanSightOverallAverageCuttingSize")]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Verbs.Enum.IsOfMeasurableQuantity, BasePhysicalQuantity.QuantityEnum.LengthSmall)]
        [SemanticFact("topSideTelemetry", Nouns.Enum.TopSideTelemetry)]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Verbs.Enum.IsTransmittedBy, "topSideTelemetry")]
        [SemanticFact("movingAverageCleanSightOverallAverageCuttingSize", Nouns.Enum.MovingAverage)]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Verbs.Enum.IsTransformationOutput, "movingAverageCleanSightOverallAverageCuttingSize")]
        [SemanticFact("GaussianUncertaintyCleanSightOverallAverageCuttingSize#01", Nouns.Enum.GaussianUncertainty)]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Verbs.Enum.HasUncertainty, "GaussianUncertaintyCleanSightOverallAverageCuttingSize#01")]
        [SemanticFact("ShaleShakerElement#01", Nouns.Enum.CuttingSeparatorLogical)]
        [SemanticFact("DrillingFluid#01", Nouns.Enum.DrillingLiquidType)]
        [SemanticFact("Cuttings#01", Nouns.Enum.CuttingsComponent)]
        [SemanticFact("Cuttings#01", Verbs.Enum.IsAComponentOf, "DrillingFluid#01")]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Verbs.Enum.ConcernsAFluidComponent, "Cuttings#01")]
        [SemanticFact("DrillingFluid#01", Verbs.Enum.IsFluidTypeLocatedAt, "ShaleShakerElement#01")]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Verbs.Enum.IsHydraulicEstimationAt, "ShaleShakerElement#01")]
        [SemanticFact("ImageInterpreter#01", Nouns.Enum.Interpreter)]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Verbs.Enum.IsComputedBy, "ImageInterpreter#01")]
        [SemanticFact("DrillDocs#01", Nouns.Enum.InstrumentationCompany)]
        [SemanticFact("CleanSightOverallAverageCuttingSize#01", Verbs.Enum.IsProvidedBy, "DrillDocs#01")]
        [MQTTTopic("DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/OverallAverageCuttingSize")]
        public ScalarProperty? OverallAverageCuttingSize { get; set; } = null;

        public bool TryApplyMqttValue(string topic, string? svalue, Dictionary<string, UnitConversion>? unitConversions, bool createIfNull, ILogger<IDWISWorker<ConfigurationForMQTT>>? logger)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Topic cannot be null or whitespace.", nameof(topic));
            }

            if (!TopicPropertyMap.Value.TryGetValue(topic, out var property))
            {
                return false;
            }
            UnitConversion unitConversion = new UnitConversion();
            if (unitConversions is not null && unitConversions.ContainsKey(topic))
            {
                unitConversion = unitConversions[topic];
            }
            try
            {
                if (property.PropertyType == typeof(ScalarProperty))
                {
                    var propertyValue = (ScalarProperty?)property.GetValue(this);
                    if (propertyValue == null)
                    {
                        if (!createIfNull)
                        {
                            return false;
                        }
                        CreateProperty(property);
                    }
                    propertyValue = (ScalarProperty?)property.GetValue(this);
                    if (propertyValue is not null)
                    {
                        if (string.IsNullOrEmpty(svalue))
                        {
                            propertyValue.Value = null;
                            return true;
                        }
                        var val = JsonSerializer.Deserialize<ScalarProperty>(svalue);
                        if (val is not null)
                        {
                            if (val.Value is not null)
                            {
                                val.Value = unitConversion.ToSI(val.Value.Value);
                            }
                            propertyValue.Value = val.Value;
                            propertyValue.Timestamp = (val.Timestamp is not null) ? val.Timestamp : DateTime.UtcNow;
                            return true;
                        }
                    }
                    return false;
                }
                else if (property.PropertyType == typeof(GaussianProperty))
                {
                    var propertyValue = (GaussianProperty?)property.GetValue(this);
                    if (propertyValue == null)
                    {
                        if (!createIfNull)
                        {
                            return false;
                        }
                        CreateProperty(property);
                    }
                    propertyValue = (GaussianProperty?)property.GetValue(this);
                    if (propertyValue is not null)
                    {
                        if (string.IsNullOrEmpty(svalue))
                        {
                            propertyValue.Mean = null;
                            propertyValue.StandardDeviation = null;
                            return true;
                        }
                        var val = JsonSerializer.Deserialize<GaussianValue>(svalue);
                        if (val is not null)
                        {
                            GaussianValue convertedValue = val.ToSI(unitConversion);
                            propertyValue.Mean = convertedValue.Mean;
                            propertyValue.StandardDeviation = convertedValue.StandardDeviation;
                            return true;
                        }
                    }
                    return false;
                }
                else if (property.PropertyType == typeof(GaussianValuesProperty))
                {
                    var propertyValue = (GaussianValuesProperty?)property.GetValue(this);
                    if (propertyValue == null)
                    {
                        if (!createIfNull)
                        {
                            return false;
                        }
                        CreateProperty(property);
                    }
                    propertyValue = (GaussianValuesProperty?)property.GetValue(this);
                    if (propertyValue is not null)
                    {
                        if (string.IsNullOrEmpty(svalue))
                        {
                            propertyValue.Values = null;
                            return true;
                        }
                        var values = JsonSerializer.Deserialize<IList<GaussianValue>>(svalue);
                        if (values is not null)
                        {
                            List<GaussianValue> convertedValues = new List<GaussianValue>();
                            foreach (var value in values)
                            {
                                if (value is not null)
                                {
                                    convertedValues.Add(value.ToSI(unitConversion));
                                }
                            }
                            propertyValue.Values = convertedValues;
                            return true;
                        }
                    }
                    return false;
                }
                else if (property.PropertyType == typeof(HistogramProperty))
                {
                    var propertyValue = (HistogramProperty?)property.GetValue(this);
                    if (propertyValue == null)
                    {
                        if (!createIfNull)
                        {
                            return false;
                        }
                        CreateProperty(property);
                    }
                    propertyValue = (HistogramProperty?)property.GetValue(this);
                    if (propertyValue is not null)
                    {
                        if (string.IsNullOrEmpty(svalue))
                        {
                            propertyValue.Value = null;
                            return true;
                        }
                        var val = JsonSerializer.Deserialize<Histogram>(svalue);
                        if (val is not null)
                        {
                            Histogram convertedValue = val.ToSI(unitConversion);
                            propertyValue.Value = convertedValue;
                            return true;
                        }
                    }
                    return false;
                }
                else if (property.PropertyType == typeof(HistogramsProperty))
                {
                    var propertyValue = (HistogramsProperty?)property.GetValue(this);
                    if (propertyValue == null)
                    {
                        if (!createIfNull)
                        {
                            return false;
                        }
                        CreateProperty(property);
                    }
                    propertyValue = (HistogramsProperty?)property.GetValue(this);
                    if (propertyValue is not null)
                    {
                        if (string.IsNullOrEmpty(svalue))
                        {
                            propertyValue.Values = null;
                            return true;
                        }
                        var values = JsonSerializer.Deserialize<IList<Histogram>>(svalue);
                        if (values is not null)
                        {
                            List<Histogram> convertedValues = new List<Histogram>();
                            foreach (var value in values)
                            {
                                if (value is not null)
                                {
                                    convertedValues.Add(value.ToSI(unitConversion));
                                }
                            }
                            propertyValue.Values = convertedValues;
                            return true;
                        }
                    }
                    return false;
                }
                else if (property.PropertyType == typeof(TimeOffsetSeriesProperty))
                {
                    var propertyValue = (TimeOffsetSeriesProperty?)property.GetValue(this);
                    if (propertyValue == null)
                    {
                        if (!createIfNull)
                        {
                            return false;
                        }
                        CreateProperty(property);
                    }
                    propertyValue = (TimeOffsetSeriesProperty?)property.GetValue(this);
                    if (propertyValue is not null)
                    {
                        if (string.IsNullOrEmpty(svalue))
                        {
                            propertyValue.Values = null;
                            return true;
                        }
                        var values = JsonSerializer.Deserialize<IList<TimeOffsetValue>>(svalue);
                        if (values is not null)
                        {
                            List<TimeOffsetValue> convertedValues = new List<TimeOffsetValue>();
                            foreach (var value in values)
                            {
                                if (value is not null)
                                {
                                    convertedValues.Add(value.ToSI(unitConversion));
                                }
                            }
                            propertyValue.Values = convertedValues;
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
            }
            return false;
        }
    }
}

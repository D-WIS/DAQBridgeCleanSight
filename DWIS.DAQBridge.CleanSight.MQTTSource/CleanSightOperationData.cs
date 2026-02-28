using DWIS.API.DTO;
using DWIS.RigOS.Common.Worker;
using DWIS.Vocabulary.Schemas;
using OSDC.DotnetLibraries.Drilling.DrillingProperties;
using OSDC.UnitConversion.Conversion.DrillingEngineering;
using System.Reflection;
using DWIS.DAQBridge.CleanSight.Model;

namespace DWIS.DAQBridge.CleanSight.MQTTSource
{
    internal class CleanSightOperationData : DWISDataWithMQTT<ConfigurationForCleanSight>
    {
        private static readonly Lazy<IReadOnlyDictionary<string, PropertyInfo>> LocalTopicPropertyMap = new(BuildTopicPropertyMap(typeof(CleanSightOperationData)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> LocalSparQLQueries = new(BuildSparQLQueries(typeof(CleanSightOperationData)));
        private static readonly Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> LocalManifests = new(BuildManifests(typeof(CleanSightOperationData), "CleanSightOperationDataManifest", "DrillDocs", "DWISBridge"));
        public override Lazy<IReadOnlyDictionary<string, PropertyInfo>> TopicPropertyMap { get => LocalTopicPropertyMap; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, Dictionary<string, QuerySpecification>>> SparQLQueries { get => LocalSparQLQueries; }
        public override Lazy<IReadOnlyDictionary<PropertyInfo, ManifestFile>> Manifests { get => LocalManifests; }

        [MQTTTopic("DWIS/Measurement/DepthDrilling/BottomOfStringReferenceLocation/BottomOfStringDepth")]
        public ScalarProperty? BottomOfStringDepth { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/DepthDrilling/HoleBottomLocation/BottomHoleDepth")]
        public ScalarProperty? BottomHoleDepth { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/VolumetricFlowrateDrilling/TOSInletHydraulicBranch/FlowrateIn")]
        public ScalarProperty? FlowrateIn { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/VolumetricFlowrateDrilling/AnnulusOutletHydraulicBranch/FlowrateOut")]
        public ScalarProperty? FlowrateOut { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/ProportionStandard/AnnulusOutletHydraulicBranch/FlowrateOutProportion")]
        public ScalarProperty? FlowrateOutProportion { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/VolumeDrilling/activePitLogical/ActiveVolume")]
        public ScalarProperty? ActiveVolume { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/HeightDrilling/DrillPipeElevator/BlockPosition")]
        public ScalarProperty? BlockPosition { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/MassDensityDrilling/TOSInletHydraulicBranch/DrillingFluidDensityIn")]
        public ScalarProperty? DrillingFluidDensityIn { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/MassDensityDrilling/AnnulusOutletHydraulicBranch/DrillingFluidDensityOut")]
        public ScalarProperty? DrillingFluidDensityOut { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/MassDensityDrilling/BHAAnnulusBranch/DownholeEquivalentCirculationDensity")]
        public ScalarProperty? DownholeEquivalentCirculationDensity { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/MassDensityDrilling/BHAAnnulusBranch/DownholeEquivalentStaticDensity")]
        public ScalarProperty? DownholeEquivalentStaticDensity { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/PressureDrilling/TOSInletHydraulicBranch/StandPipePressure")]
        public ScalarProperty? StandPipePressure { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/GammaRayIndexDrilling/BHAAnnulusBranch/DownholeGammaRay")]
        public ScalarProperty? DownholeGammaRay { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/RateOfPenetrationDrilling/HoleBottomLocation/SurfaceRateOfPenetration")]
        public ScalarProperty? SurfaceRateOfPenetration { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/HookLoadDrilling/Hook/HookLoad")]
        public ScalarProperty? HookLoad { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/TorqueDrilling/RotatingDriveSystemLocation/RotatingDriveSystemTorque")]
        public ScalarProperty? RotatingDriveSystemTorque { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/AngularVelocityDrilling/RotatingDriveSystemLocation/RotatingDriveSystemRotationalSpeed")]
        public ScalarProperty? RotatingDriveSystemRotationalSpeed { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/DepthDrilling/ReturnFlowLine/LagDepth")]
        public ScalarProperty? LagDepth { get; set; } = null;

        [MQTTTopic("DWIS/ComputedData/DurationDrilling/ReturnFlowLine/LagTime")]
        public ScalarProperty? LagTime { get; set; } = null;

        [MQTTTopic("DWIS/Measurement/WeightOnBitDrilling/HoleBottomLocation/SurfaceWeightOnBit")]
        public ScalarProperty? SurfaceWeightOnBit { get; set; } = null;

        // Gas
        [MQTTTopic("DWIS/Measurement/ProportionStandard/ReturnFlowLine/GasProportionFlowOut")]
        public ScalarProperty? GasProportionFlowOut { get; set; } = null;

        // inclination
        [MQTTTopic("DWIS/Measurement/PlaneAngleDrilling/BottomOfStringReferenceLocation/BottomOfStringInclination")]
        public ScalarProperty? BottomOfStringInclination { get; set; } = null;

        // TVD
        [MQTTTopic("DWIS/Measurement/DepthDrilling/BottomOfStringReferenceLocation/BottomOfStringVerticalDepth")]
        public ScalarProperty? BottomOfStringVerticalDepth { get; set; } = null;

        // PV
        [MQTTTopic("DWIS/ComputedData/DynamicViscosityDrilling/TOSInletHydraulicBranch/PlasticViscosityAtInlet")]
        public ScalarProperty? PlasticViscosityAtInlet { get; set; } = null;

        // YP
        [MQTTTopic("DWIS/ComputedData/FluidShearStress/TOSInletHydraulicBranch/YieldPointAtInlet")]
        public ScalarProperty? YieldPointAtInlet { get; set; } = null;

        // bit diameter
        [MQTTTopic("DWIS/ProcessData/DiameterPipeDrilling/BottomOfStringReferenceLocation/BitDiameter")]
        public ScalarProperty? BitDiameter { get; set; } = null;
    }
}

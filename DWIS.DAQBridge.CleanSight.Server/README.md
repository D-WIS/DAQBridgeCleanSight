## CleanSight Bridge Server

This project is the main bridge service between DrillDocs CleanSight and the DWIS Blackboard. It
connects to an MQTT broker to receive CleanSight analytics and to publish operational data, while
also connecting to the DWIS Blackboard (OPC UA client) to write CleanSight results and read
operational signals.

### What it does
- Subscribes to CleanSight result topics over MQTT and publishes them to the DWIS Blackboard.
- Queries operational drilling data from the DWIS Blackboard and publishes the values to MQTT so
  CleanSight can consume them.
- Registers SPARQL queries and DWIS manifests for input and output datasets to align with DWIS
  vocabulary semantics.

### Data flowing from DWIS to MQTT (inputs)
Defined in `CleanSightInputData.cs`, the service reads operational data such as:
- depth signals (bit depth, hole depth, TVD)
- flow rates, flow proportions, active pit volume
- mud properties (density in/out, PV, YP), pressures, ECD/ESD
- ROP, WOB, hook load, torque, RPM, block position
- lag time/depth, gas proportion, gamma ray, and bit diameter

### Data flowing from MQTT to DWIS (outputs)
Defined in `CleanSightOutputData.cs`, the service publishes CleanSight results such as:
- shaker load estimates and distributions
- cuttings recovery rates and accumulated cuttings recovery
- overall cuttings recovery and total theoretical cuttings volume
- max caving sizes and overall average cutting size

### Runtime behavior
The worker connects to MQTT and the Blackboard on startup, subscribes to the relevant topics, and
loops on a configurable interval to read and publish data. It logs a sample of key values (overall
cuttings recovery and block position) for sanity checking.

### Configuration note
The OPC UA client configuration is stored in `config/Quickstarts.ReferenceClient.Config.xml` and
controls certificate and endpoint behavior for the DWIS Blackboard connection.

### Installation Procedure
For the installation, you will need:
To have docker desktop installed on your PC (configured to run Linux docker images).
Create a folder called something like c:\Volumes. This folder will be used to share files between the different docker containers.
To install and run a docker container with an MQTT broker. For my tests, I have used mosquitto. Here is the installation command:
```sh
docker run -d  --name mosquitto -p 1883:1883  -v c:\Volumes\mosquitto\config:/mosquitto/config  -v c:\Volumes\mosquitto\data:/mosquitto/data  -v c:\Volumes\mosquitto\log:/mosquitto/log  eclipse-mosquitto:latest
```
To install and run a replicated DWIS Blackboard. Here is the installation command:
```sh
docker run  -dit --name blackboard -P -p 48030:48030/tcp --hostname localhost  digiwells/ddhubserver:latest --useHub --hubURL https://dwis.digiwells.no/blackboard/applications
```
To install and run the CleanSight DWIS bridge. Here is the installation command:
```sh
docker run -dit --name CleanSightBridge -v c:\Volumes\DWISDAQBridgeCleanSightServer:/home digiwells/dwisdaqbridgecleansightserver:stable
```

For the operational data, the following MQTT topics are used:
- DWIS/Measurement/DepthDrilling/BottomOfStringReferenceLocation/BottomOfStringDepth
- DWIS/Measurement/DepthDrilling/HoleBottomLocation/BottomHoleDepth
- DWIS/Measurement/VolumetricFlowrateDrilling/TOSInletHydraulicBranch/FlowrateIn
- DWIS/Measurement/VolumetricFlowrateDrilling/AnnulusOutletHydraulicBranch/FlowrateOut
- DWIS/Measurement/ProportionStandard/AnnulusOutletHydraulicBranch/FlowrateOutProportion
- DWIS/Measurement/VolumeDrilling/activePitLogical/ActiveVolume
- DWIS/Measurement/HeightDrilling/DrillPipeElevator/BlockPosition
- DWIS/Measurement/MassDensityDrilling/TOSInletHydraulicBranch/DrillingFluidDensityIn
- DWIS/Measurement/MassDensityDrilling/AnnulusOutletHydraulicBranch/DrillingFluidDensityOut
- DWIS/Measurement/MassDensityDrilling/BHAAnnulusBranch/DownholeEquivalentCirculationDensity
- DWIS/Measurement/MassDensityDrilling/BHAAnnulusBranch/DownholeEquivalentStaticDensity
- DWIS/Measurement/VolumeDrilling/TOSInletHydraulicBranch/ActivePitVolume
- DWIS/Measurement/PressureDrilling/TOSInletHydraulicBranch/StandPipePressure
- DWIS/Measurement/GammaRayIndexDrilling/BHAAnnulusBranch/DownholeGammaRay
- DWIS/Measurement/RateOfPenetrationDrilling/HoleBottomLocation/SurfaceRateOfPenetration
- DWIS/Measurement/HookLoadDrilling/Hook/HookLoad
- DWIS/Measurement/TorqueDrilling/RotatingDriveSystemLocation/RotatingDriveSystemTorque
- DWIS/Measurement/AngularVelocityDrilling/RotatingDriveSystemLocation/RotatingDriveSystemRotationalSpeed
- DWIS/ComputedData/DepthDrilling/ReturnFlowLine/LagDepth
- DWIS/ComputedData/DurationDrilling/ReturnFlowLine/LagTime
- DWIS/Measurement/WeightOnBitDrilling/HoleBottomLocation/SurfaceWeightOnBit
- DWIS/Measurement/ProportionStandard/ReturnFlowLine/GasProportionFlowOut
- DWIS/Measurement/PlaneAngleDrilling/BottomOfStringReferenceLocation/BottomOfStringInclination
- DWIS/Measurement/DepthDrilling/BottomOfStringReferenceLocation/BottomOfStringVerticalDepth
- DWIS/ComputedData/DynamicViscosityDrilling/TOSInletHydraulicBranch/PlasticViscosityAtInlet
- DWIS/ComputedData/FluidShearStress/TOSInletHydraulicBranch/YieldPointAtInlet
- DWIS/ProcessData/DiameterPipeDrilling/BottomOfStringReferenceLocation/BitDiameter
 For these topics, the value is a floating point formatted using an "invariant culture" floating point format, i.e., . For decimal point and no commas or spaces for thousands.

The data that you will provide are expected as follows:
Floating point values in an "invariant culture" format for the following topics:
- DWIS/ComputedData/DimensionLessStandard/CuttingSeparatorLogical/AverageShakerLoadEstimate
- DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/OverallCuttingsRecovery
- DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/TotalTheoreticalCuttingsVolume
- DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/OverallMaxCavingSize
- DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/OverallAverageCuttingSize

 A list of Gaussian Property serialized in json (each element is for one shale shaker) for the following topics:
- DWIS/ComputedData/DimensionLessStandard/CuttingSeparatorLogical/ShakerLoadEstimates
- DWIS/ComputedData/VolumetricFlowrateDrilling/CuttingSeparatorLogical/AccumulatedCuttingsRecoveryRates
- DWIS/ComputedData/VolumeDrilling/CuttingSeparatorLogical/AccumulatedCuttingsRecovery
- DWIS/ComputedData/LengthSmall/CuttingSeparatorLogical/MaxCavingSizes

Example format:
```json
{
  "values": [
    {
      "mean": 10.5,
      "standardDeviation": 2.1
    },
    {
      "mean": 7.0,
      "standardDeviation": 1.3
    }
  ]
}
```

A list of histograms serialized in json (each element is for one shale shaker, the bins in an histogram correspond to the position on shake shaker) for the following topic:
- DWIS/ComputedData/ProportionStandard/CuttingSeparatorLogical/ShakerLoadDistributions

Example json format:
```json
{
  "values": [
    {
      "bins": [0.0, 1.2, 2.5, 3.7]
    },
    {
      "bins": [5.0, 6.0, 7.5]
    }
  ]
}
```

When running inside docker, the configuration file may use `host.docker.internal` instead of `localhost`.
It is possible to define unit conversions for each of the topic as the data comming from MQTT may not be in SI unit while DWIS expects that all internal data are in SI.

The json format for the unit conversions stored in the configuration is as follows:
```json
{
  "unitConversions": {
    "topic1": {
      "conversionFactor": 3.28084,
      "conversionOffset": 0.0
    },
    "topic2": {
      "conversionFactor": 1.8,
      "conversionOffset": 32.0
    }
  }
}
```

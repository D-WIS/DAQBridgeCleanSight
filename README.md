## DWIS CleanSight Bridge Solution

This solution integrates DrillDocs CleanSight analytics with the DWIS Blackboard. It provides a
production bridge service plus two companion simulators that let you validate end-to-end data flow
on both the MQTT and OPC UA (Blackboard) sides.

### Solution overview
The solution consists of three projects:
- `DWIS.DAQBridge.CleanSight.Server`: the production bridge that connects MQTT to the DWIS
  Blackboard, pushing CleanSight results into DWIS and publishing operational data back to MQTT.
- `DWIS.DAQBridge.CleanSight.MQTTSource`: a test service that publishes synthetic CleanSight
  results over MQTT and subscribes to operational data to verify the MQTT flow.
- `DWIS.DAQBridge.CleanSight.OPCUASource`: a test service that publishes synthetic operational
  data to the DWIS Blackboard and reads back CleanSight results to verify the Blackboard flow.

### Data flow in the bridge
The bridge is bi-directional:
- MQTT -> DWIS Blackboard: CleanSight results such as shaker load estimates, cuttings recovery
  metrics, shaker load distributions, and cutting size statistics.
- DWIS Blackboard -> MQTT: operational drilling signals such as depth, flow rates, mud properties,
  pressures, ROP, WOB, torque, RPM, block position, lag time/depth, and related values.

### How the solution is used
1) Run the bridge service against a configured MQTT broker and DWIS Blackboard endpoint.
2) Use the MQTTSource simulator to validate that CleanSight results published over MQTT are
   translated into DWIS data on the Blackboard.
3) Use the OPCUASource simulator to validate that operational data written to the Blackboard is
   published back to MQTT for CleanSight to consume.

### Configuration highlights
- The bridge and OPC UA simulator use an OPC UA client configuration file at
  `DWIS.DAQBridge.CleanSight.Server/config/Quickstarts.ReferenceClient.Config.xml` and
  `DWIS.DAQBridge.CleanSight.OPCUASource/config/Quickstarts.ReferenceClient.Config.xml`.
- Logging is configured via standard `appsettings.json` files in each project.

### Container image (bridge only)
To run the bridge from the published image:
```docker
docker run -dit --name CleanSightBridge -v c:/Volumes/DWISDAQBridgeCleanSightServer:/home digiwells/dwisdaqbridgecleansightserver:stable
```

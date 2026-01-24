This is the repository dedicated to the CleanSight (from DrillDocs) bridge to DWIS. It contains
the bridge in DWIS.DAQBridge.CleanSight.Server and two test services to check the communication.
One is defined in DWIS.DAQBridge.CleanSight.MQTTSource that simulates the generation of CleanSight
results and the reading of operational data using MQTT. The other one is called
DWIS.DAQBridge.CleanSight.OPCUASource. The later is used to verify that the CleanSight results
are received on the DWIS Blackboard and to generate on the DWIS Blackboard operational data.

To install the CleanSight bridge, one shall run the following docker command:
```docker
docker run  -dit --name CleanSightBridge -v c:/Volumes/DWISDAQBridgeCleanSightServer:/home digiwells/dwisdaqbridgecleansightserver:stable
```

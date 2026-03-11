## CleanSight MQTT Source (Test Service)

This project is a lightweight simulator used to validate the MQTT side of the CleanSight bridge.
It generates synthetic CleanSight results and publishes them over MQTT, while also subscribing to
operational data topics to verify that the values are received back over MQTT.

### What it does
- Publishes random CleanSight result payloads to the same MQTT topics used by the bridge.
- Subscribes to operational data topics and logs sample values so you can confirm end-to-end flow.
- Uses DWIS manifests and the same MQTT topic mapping as the production bridge service.

### Published data (CleanSight results)
Defined in `CleanSightResults.cs`, the simulator sends typical CleanSight outputs such as:
- shaker load estimates, average shaker load estimate
- accumulated cuttings recovery rates and volumes
- overall cuttings recovery and theoretical cuttings volume
- shaker load distributions and max caving sizes
- overall max caving size and overall average cutting size

### Subscribed data (operational signals)
Defined in `CleanSightOperationData.cs`, the simulator listens for operational data like depth,
flow, density, pressure, ROP, WOB, torque, RPM, block position, lag time/depth, and more.

### Runtime behavior
The worker connects to MQTT, publishes randomized result values at a fixed loop interval, and logs
both a CleanSight result and an operational signal for quick verification.

## Docker
To install and run the CleanSight MQTT Source. Here is the installation command:
```sh
docker run -dit --name CleanSightMQTTSource -v c:\Volumes\DWISDAQBridgeCleanSightMQTTSource:/home digiwells/dwisdaqbridgecleansightmqttsource:stable
```

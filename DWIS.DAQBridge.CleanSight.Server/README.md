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

## CleanSight OPC UA Source (Test Service)

This project is a simulator for the DWIS Blackboard side of the bridge. It connects to the
Blackboard via OPC UA, writes randomized operational drilling data, and reads back CleanSight
results to confirm that the bridge is publishing correctly.

### What it does
- Publishes synthetic operational data to the DWIS Blackboard using the DWIS vocabulary mappings.
- Registers SPARQL queries for CleanSight results and reads them from the Blackboard.
- Logs representative values from both operational data and CleanSight results for quick checks.

### Published data (operational signals)
Defined in `CleanSightOperationData.cs`, the simulator writes data such as:
- depth, flow rates, flow proportions, active pit volume
- mud properties (density in/out, PV, YP), pressures, ECD/ESD
- ROP, WOB, hook load, torque, RPM, block position
- lag time/depth, gas proportion, gamma ray, and bit diameter

### Read data (CleanSight results)
Defined in `CleanSightResults.cs`, the simulator reads CleanSight outputs including:
- shaker load estimates and distributions
- cuttings recovery rates, accumulated recovery, and overall cuttings recovery
- theoretical cuttings volume, max caving sizes, and average cutting size

### Runtime behavior
The worker connects to the DWIS Blackboard, pushes randomized operational data on a periodic
timer, then reads CleanSight result values and logs a sample of each.

### Configuration note
The OPC UA client configuration is stored in `config/Quickstarts.ReferenceClient.Config.xml` and
controls certificate and endpoint behavior for the Blackboard connection.

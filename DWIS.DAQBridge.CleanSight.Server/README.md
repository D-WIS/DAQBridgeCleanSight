This is a bridge service that communicates with MQTT on one side and the DWIS Blackboard on the other side.
It reads the CleanSight results from MQTT and pushes them on the DWIS Blackboard. It also reads
operational data from DWIS Blackboard and pushes them on MQTT.
# Common
Shared .NET 8 models and building blocks for D-WIS RigOS/ADCS capabilities: core data types, functional equipment, lookup tables, and capability descriptions (finite state and logical statements).

## Projects
- `DWIS.RigOS.Common.Model/` - primitives for readable/assignable variables, tokens, limits, setpoints, safe-operating variables, and semantic descriptors; includes low-level interface structures for hoist/rotation/circulation and function scaffolding (activable/runnable/protection/runnable functions).
- `DWIS.RigOS.Common.FunctionalEquipment/` - typed equipment definitions (string hoist/rotation/circulation, riser boost pump, pumps, slips, valves/choke) annotated with DWIS vocabulary facts and manifest metadata for ADCS interfaces.
- `DWIS.RigOS.Common.LookupTableModel/` - n-D lookup tables (0D-4D) with axis references, direct references, and grid interpolators (1D-4D plus nullable variants) for parameterized limits/setpoints.
- `DWIS.RigOS.Common.LookupTableModel.Tests/` - NUnit coverage for the grid interpolators (1D-4D and nullable variants).
- `DWIS.RigOS.Capabilities.FSA.Model/` - finite state automaton model (states, transitions, current state/time tracking, safe mode management procedures).
- `DWIS.RigOS.Capabilities.LogicalStatement.Model/` - logical statement containers, premises, and conditions used in capability descriptions.
- `.github/workflows/` - CI to build/pack/push NuGet artifacts for each model.
- Other assets: solution `DWIS.RigOS.Common.sln`, shared `LICENSE`, reference doc `setpoints and limits-eric-v1.docx`.

## Build & test
- Build everything: `dotnet build DWIS.RigOS.Common.sln`
- Run lookup-table tests: `dotnet test DWIS.RigOS.Common.LookupTableModel.Tests/DWIS.RigOS.Common.LookupTableModel.Tests.csproj`
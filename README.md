
PrepareLanding
==============

This is an unofficial 1.5, 1.6 and 1.7 update of the Prepare Landing mod created by neitsa.
The dependency on HugsLib has also been removed.

Based on:

- [m00nl1ght-dev/PrepareLanding](https://github.com/m00nl1ght-dev/PrepareLanding): the unofficial 1.5 / 1.6 update this repository is based on.
- [neitsa/PrepareLanding](https://github.com/neitsa/PrepareLanding): the original Prepare Landing mod by neitsa.

Building
--------

Requires the .NET 10 SDK.

```
dotnet build -c Release
```

Two assemblies are built:

- `output/PrepareLanding/1.6/Assemblies` (net472): RimWorld 1.6, referenced through the `Krafs.Rimworld.Ref` NuGet package.
- `output/PrepareLanding/1.7/Assemblies` (net10.0): RimWorld 1.7, referenced from a RimWorld 1.7 install and the Harmony mod's 1.7 assembly.

The 1.7 build uses the default Steam locations. Use `-p:RimWorldDir="D:\Games\RimWorld"` (or the `RIMWORLD_DIR` environment variable)
and `-p:Harmony17Dir=<folder with 0Harmony.dll>` for other locations, or `-p:TargetFramework=net472` to build only for 1.6.
Add `-p:DeployMod=true` to copy the mod into the game's `Mods` folder.

Documentation
-------------

A [detailed manual](https://neitsa.github.io/games/rimworld/preparelanding/) is available online.

Contributing
------------

See the [guidelines](CONTRIBUTING.md) for both bug reports and pull requests.

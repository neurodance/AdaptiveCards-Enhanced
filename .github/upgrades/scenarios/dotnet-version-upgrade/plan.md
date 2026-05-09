# .NET 10 Upgrade Plan

## Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 9 projects (after WPFVisualizer + .wapproj unloaded), all SDK-style and on .NET Standard 2.0 / .NET 5 / .NET 6 / .NET 8. Clear dependency structure, no major API rewrites expected — primarily TFM bumps, package upgrades, and behavioral fixes.

## Scope
Target framework: **net10.0**

### In-Scope Projects (9)
**Libraries**
- `Library/AdaptiveCards/AdaptiveCards.csproj` (netstandard2.0)
- `Library/AdaptiveCards.Templating/AdaptiveCards.Templating.csproj` (netstandard2.0;net6;net8)
- `Library/AdaptiveCards.Templating.CSharp.WinRT/AdaptiveCards.Templating.WinRT.csproj` (net6.0-windows10.0.19041.0)
- `Library/AdaptiveCards.Rendering.Wpf/AdaptiveCards.Rendering.Wpf.csproj` (net462)
- `Library/AdaptiveCards.Rendering.Wpf.Xceed/AdaptiveCards.Rendering.Wpf.Xceed.csproj` (net462)

**Samples**
- `samples/AdaptiveCards.Sample.ImageRender/AdaptiveCards.Sample.ImageRender.csproj` (net462)
- `Samples/ImageRendererServer/ImageRendererServer.csproj` (net462)

**Tests**
- `Test/AdaptiveCards.Test/AdaptiveCards.Test.csproj` (net5.0)
- `Test/AdaptiveCards.Templating.Test/AdaptiveCards.Templating.Test.csproj` (net6.0;net8.0)

### Excluded (unloaded by user)
- `Samples/WPFVisualizer/AdaptiveCards.Sample.WPFVisualizer.csproj`
- `Samples/WPFVisualizer.PackageProject/AdaptiveCards.Sample.WPFVisualizer.PackageProject.wapproj`

## Tasks

### 01-prerequisites
Verify .NET 10 SDK installed; update or remove `global.json` if it pins an older SDK.

### 02-update-tfms
Update `TargetFramework`/`TargetFrameworks` across all 9 projects to `net10.0` (WPF projects use `net10.0-windows`; WinRT project uses `net10.0-windows10.0.19041.0`). ASP.NET project (`ImageRendererServer`) switches SDK to `Microsoft.NET.Sdk.Web` if not already.

### 03-update-packages
Bump NuGet package references to versions compatible with net10.0; remove packages whose functionality is now in the framework (per `NuGet.0003`); replace deprecated packages (per `NuGet.0005` in `ImageRendererServer`).

### 04-fix-code
Resolve API breaking changes (`Api.0001`/`Api.0002`/`Api.0003`) — primarily WPF, GDI+/System.Drawing, and Speech APIs. Update Web API patterns in `ImageRendererServer` (System.Web → ASP.NET Core).

### 05-build-validate
Build full solution; resolve any remaining compilation errors in a single bounded pass.

### 06-test
Run `AdaptiveCards.Test` and `AdaptiveCards.Templating.Test`; fix regressions.

## Execution Constraints
- Single atomic upgrade — all 9 projects updated together
- Validate full solution build after upgrade
- Tests run after build is green
- Commit strategy: **Single Commit at End**

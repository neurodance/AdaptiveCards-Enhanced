# Scenario Instructions: .NET Version Upgrade

## Parameters
- **Target framework**: `net10.0` (.NET 10.0 LTS)
- **Solution**: `source/dotnet/AdaptiveCards.sln`
- **Source branch**: `main`
- **Working branch**: `upgrade-to-NET10`

## Preferences
### Flow Mode
**Automatic** — Run end-to-end, only pause when blocked.

### Commit Strategy
**Single Commit at End** — One atomic upgrade, one commit.

## Strategy
**Selected**: All-At-Once
**Rationale**: 9 in-scope projects (WPFVisualizer + .wapproj unloaded by user), all SDK-style, on .NET Standard 2.0 / .NET 5 / .NET 6 / .NET 8. Straightforward TFM + package bumps with bounded code fixes.

### Execution Constraints
- Single atomic upgrade — all projects updated together
- Order within the upgrade: TFMs → packages → code fixes → build → tests
- Build full solution with 0 errors before running tests
- Single bounded fix pass after build (no retry loops)

## Scope Decisions
- **Excluded** (per user, unloaded from solution):
  - `Samples/WPFVisualizer/AdaptiveCards.Sample.WPFVisualizer.csproj`
  - `Samples/WPFVisualizer.PackageProject/AdaptiveCards.Sample.WPFVisualizer.PackageProject.wapproj`

## Key Decisions Log
- **2025**: User unloaded WPFVisualizer + WPFVisualizer.PackageProject — these are skipped from the upgrade.
- **2025**: All-At-Once strategy auto-selected (9 projects, modern frameworks, no major rewrites).

## User Preferences
### Technical Preferences
_(none recorded)_

### Custom Instructions
_(none)_

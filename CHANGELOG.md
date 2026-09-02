v1.2.0

- Expanded the Base Game editor with Prologue, relationship, constitutional reform, international-empathy, and military expansion/modernization options.
- Paired Base Game military controls now update both related save flags and safely load as enabled only when both flags are enabled.
- Added clearer Base Game ranges, warnings, labels, and grouped controls for core stats, events, politics, and military settings.
- Expanded the Rizia DLC editor with economy guidance, grouped deployable and stockpiled military units, war-menu cheats, internal relations, foreign affairs, and family relationship controls.
- Added missing Rizia controls for Lespia friendliness, military stockpiles, internal relations, and family values, including Lucita romance guidance.

---

v1.1.0

- Added "Revert Black Tuesday" and "Prevent Market Crash" toggles to the Base Game editor.
- These new options allow for direct intervention in major economic game events.
- Added detailed warning labels for the new event toggles to guide usage.

---
v1.0.9

- Refactored GitHub Actions release workflow to trigger accurately on pushes to the master branch.
- Updated automated push scripts (push.ps1 and push.sh) to reliably extract version tags, commit, and push to the master branch.

---
v1.0.5

- Patched build process to use stable .NET 8 LTS, resolving all build and dependency conflicts.
- Fixed critical error in "Export As..." functionality.
- Added null-reference safety checks to prevent crashes when loading data.
- Corrected military modernization warning text as requested.
- Added .gitignore which I forgot earlier (ouch)!
- Other internal hotfixes, git related.

---
v1.0.0

- Initial release of the Suzerain Save Editor.
- Includes support for editing Base Game and Rizia DLC variables.
- Implemented core features like Load, Save, Export As, and Refresh.
- Added dynamic warnings for game-specific conditions (e.g., Black Tuesday).

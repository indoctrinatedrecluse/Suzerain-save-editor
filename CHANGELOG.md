v1.0.8

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
# Suzerain Save Editor

A simple desktop application for editing your Suzerain save files. This tool supports both the base game (Sordland) and the Rizia DLC, allowing you to modify various game parameters to customize your playthrough.

### Disclaimer - IMPORTANT!

If changes are not reflected, or even in general, always go back to main menu and reload the save file from there, not from the pause menu. Especially on the Active save. The save editor works, one just needs to read this disclaimer!

## Features

### Base Game (Sordland)
- **Core Stats:** Edit your Government Budget, Personal Wealth, Country Unrest, Public Opinion, and Bludish Opinion.
- **Politics & Reform:** Influence the Assembly and Supreme Court votes for your constitutional amendments. Ensure key political figures like Albin and Gloria are convinced.
- **Economy:** Adjust the economic status of all four major regions (Agnland, Bergia, Lorren, Gruni).
- **Military:** Manage the expansion and modernization of your Army, Navy, and Airforce.

### Rizia DLC
- **National Resources:** Control Rizia's Budget, Authority, and Energy levels.
- **Military Power:** Modify your total military units (Infantry, Tanks, Ships, etc.), manage stockpiles, and gain unlimited action points and airstrikes during the war.
- **Diplomacy:** Increase your negotiating power with all foreign nations.

### Raw Save Editing
- **Complete Variable Access:** Browse every primitive key-value pair stored in the save file's `variables` data.
- **Search:** Filter variables by key to quickly find specific entries.
- **Safe Input Types:** Edit boolean values (`true`/`false`) and numeric values only.
- **Synchronized Tabs:** Changes made in the Base Game, Rizia DLC, and Raw Save Editing tabs are shared immediately.
- **Warning:** Raw save editing is risky and can cause unexpected game behavior or corrupt a save. Always keep a backup.

## How to Use
1.  Download the latest `SuzerainSaveEditor.exe` from the [Releases](https://github.com/your-username/your-repo/releases) page.
2.  Run the application.
3.  Click **Load Save File** and navigate to your Suzerain save file.
    -   Save files are typically located at: `%APPDATA%\..\LocalLow\Torpor Games\Suzerain`
4.  Modify the desired values in the editor.
5.  Click **Save Changes** to overwrite your save file or **Export As...** to create a new, modified save file.

The **Raw Save Editing** tab includes its own Load, Refresh, Save, and Export controls. Invalid raw values are rejected and marked for correction.

**Disclaimer:** Modifying save files can lead to unexpected game behavior. Always back up your original saves before editing.

## Release signing

Windows release executables are Authenticode-signed with `signtool.exe` when signing configuration is supplied. If the signing values are missing, builds and releases continue normally without code signing.

For personal, testing, or internal releases, generate a self-signed certificate locally with:

```powershell
.\generate-code-signing-secrets.ps1 -CopyBase64ToClipboard
```

The script prompts for a PFX password, creates a temporary self-signed code-signing certificate, and prints the values needed for these GitHub repository secrets:

- `CODE_SIGNING_CERTIFICATE_BASE64`: Base64-encoded PFX certificate.
- `CODE_SIGNING_CERTIFICATE_PASSWORD`: Password for the certificate.
- `CODE_SIGNING_TIMESTAMP_URL`: RFC 3161 timestamp URL.

Self-signed certificates do not establish public trust or remove Windows SmartScreen warnings. They are suitable for testing or controlled distribution, not general public releases. Never commit the generated PFX, password, Base64 output, or GitHub secret values.

The local build scripts and GitHub Actions use `sign-windows-executable.ps1`. Signing is explicitly selected:

```powershell
.\build.ps1 sign
.\build.ps1 nosign
```

The equivalent shell commands are:

```bash
./build.sh sign
./build.sh nosign
```

For releases, use `push.ps1` with the version tag and required signing mode:

```powershell
.\push.ps1 v1.4.0 sign
.\push.ps1 v1.4.0 nosign
```

The tag is annotated with the selected mode, and the release workflow reads it before building. For local builds, `sign` creates an ephemeral self-signed certificate. For GitHub releases, `sign` uses configured certificate secrets when available; if they are missing, the release proceeds unsigned. With `nosign`, signing is skipped.

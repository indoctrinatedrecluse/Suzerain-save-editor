# Suzerain Save Editor

A simple desktop application for editing your Suzerain save files. This tool supports both the base game (Sordland) and the Rizia DLC, allowing you to modify various game parameters to customize your playthrough.

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
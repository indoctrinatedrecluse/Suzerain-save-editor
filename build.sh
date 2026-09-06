#!/bin/bash
# ==============================================================================
# Suzerain Save Editor - Build Script (Cygwin/Linux)
# ==============================================================================
# Dependencies needed to run this script:
# 1. .NET 8.0 SDK (Must be installed manually from Microsoft)
# 2. .NET MAUI Workload (Will be installed by this script if missing)
# ==============================================================================

signing_mode="${1:-nosign}"
if [ "$signing_mode" != "sign" ] && [ "$signing_mode" != "nosign" ]; then
    echo "Usage: ./build.sh [sign|nosign]"
    exit 1
fi

echo "====================================="
echo "Starting Build for Suzerain Save Editor ($signing_mode)"
echo "====================================="

# Check if dotnet is installed
if ! command -v dotnet &> /dev/null
then
    echo ".NET SDK is not installed or not in PATH. Please install .NET 8.0 SDK."
    exit 1
fi

# Check for MAUI workload
echo "Checking for MAUI workload..."
if ! dotnet workload list | grep -q "maui"; then
    echo "MAUI workload not found. Installing..."
    dotnet workload install maui
    if [ $? -ne 0 ]; then
        echo "Failed to install MAUI workload."
        exit $?
    fi
else
    echo "MAUI workload is already installed."
fi

# Restore NuGet packages first to generate the assets file
echo "Restoring NuGet packages..."
dotnet restore
if [ $? -ne 0 ]; then
    echo "Failed to restore packages."
    exit $?
fi

# Clean previous build artifacts
echo "Cleaning previous build..."
dotnet clean
if [ $? -ne 0 ]; then
    echo "Failed to clean the project."
    exit $?
fi

# Publish the single-file executable
echo "Publishing the application (Release mode, Windows x64)..."
# The publish command will implicitly run restore again, which is expected.
dotnet publish -f net8.0-windows10.0.19041.0 -c Release
if [ $? -ne 0 ]; then
    echo "Failed to publish the application."
    exit $?
fi

publish_path="bin/Release/net8.0-windows10.0.19041.0/win-x64/publish"
executable_path="$publish_path/SuzerainSaveEditor.exe"

if [ "$signing_mode" = "sign" ] && command -v pwsh &> /dev/null; then
    export CODE_SIGNING_GENERATE_SELF_SIGNED=true
    pwsh -File "./sign-windows-executable.ps1" -ExecutablePath "$executable_path"
    if [ $? -ne 0 ]; then
        echo "Code signing failed."
        exit $?
    fi
elif [ "$signing_mode" = "sign" ]; then
    echo "PowerShell is unavailable; cannot create a self-signed build."
    exit 1
else
    echo "Code signing disabled."
fi

echo "====================================="
echo "Build complete! The executable is located at:"
echo "bin/Release/net8.0-windows10.0.19041.0/win-x64/publish/SuzerainSaveEditor.exe"
echo "====================================="

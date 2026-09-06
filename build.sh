#!/bin/bash
# ==============================================================================
# Suzerain Save Editor - Build Script (Cygwin/Linux)
# ==============================================================================
# Dependencies needed to run this script:
# 1. .NET 8.0 SDK (Must be installed manually from Microsoft)
# 2. .NET MAUI Workload (Will be installed by this script if missing)
# ==============================================================================

echo "====================================="
echo "Starting Build for Suzerain Save Editor"
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

if [ -n "${CODE_SIGNING_CERTIFICATE_BASE64:-}" ] &&
   [ -n "${CODE_SIGNING_CERTIFICATE_PASSWORD:-}" ] &&
   [ -n "${CODE_SIGNING_TIMESTAMP_URL:-}" ]; then
    sign_tool="$(command -v signtool.exe || command -v signtool || true)"
    if [ -z "$sign_tool" ]; then
        echo "signtool was not found; cannot sign with the supplied configuration."
        exit 1
    else
        certificate_path="$(mktemp --suffix=.pfx)"
        cleanup() {
            rm -f "$certificate_path"
        }
        trap cleanup EXIT

        if ! printf '%s' "$CODE_SIGNING_CERTIFICATE_BASE64" | base64 --decode > "$certificate_path"; then
            echo "Failed to decode the signing certificate."
            exit 1
        elif "$sign_tool" sign \
            /fd SHA256 \
            /td SHA256 \
            /tr "$CODE_SIGNING_TIMESTAMP_URL" \
            /f "$certificate_path" \
            /p "$CODE_SIGNING_CERTIFICATE_PASSWORD" \
            "$executable_path" &&
            "$sign_tool" verify /pa /all "$executable_path"; then
            echo "Executable signed and verified."
        else
            echo "Code signing failed."
            exit 1
        fi
        trap - EXIT
        cleanup
    fi
else
    echo "Code-signing configuration is incomplete; continuing without signing."
fi

echo "====================================="
echo "Build complete! The executable is located at:"
echo "bin/Release/net8.0-windows10.0.19041.0/win-x64/publish/SuzerainSaveEditor.exe"
echo "====================================="

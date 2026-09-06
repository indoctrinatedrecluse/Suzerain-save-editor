# ==============================================================================
# Suzerain Save Editor - Build Script (PowerShell)
# ==============================================================================
# Dependencies needed to run this script:
# 1. .NET 8.0 SDK (Must be installed manually from Microsoft)
# 2. .NET MAUI Workload (Will be installed by this script if missing)
# ==============================================================================

Write-Host "====================================="
Write-Host "Starting Build for Suzerain Save Editor"
Write-Host "====================================="

# Check if dotnet is installed
if (-not (Get-Command "dotnet" -ErrorAction SilentlyContinue)) {
    Write-Error ".NET SDK is not installed or not in PATH. Please install .NET 8.0 SDK."
    exit 1
}

# Check for MAUI workload
Write-Host "Checking for MAUI workload..."
$workloads = dotnet workload list
if ($workloads -notmatch "maui") {
    Write-Host "MAUI workload not found. Installing..."
    dotnet workload install maui
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to install MAUI workload."
        exit $LASTEXITCODE
    }
} else {
    Write-Host "MAUI workload is already installed."
}

# Restore NuGet packages first to generate the assets file
Write-Host "Restoring NuGet packages..."
dotnet restore
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to restore packages."
    exit $LASTEXITCODE
}

# Clean previous build artifacts
Write-Host "Cleaning previous build..."
dotnet clean
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to clean the project."
    exit $LASTEXITCODE
}

# Publish the single-file executable
Write-Host "Publishing the application (Release mode, Windows x64)..."
# The publish command will implicitly run restore again, which is expected.
dotnet publish -f net8.0-windows10.0.19041.0 -c Release
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to publish the application."
    exit $LASTEXITCODE
}

$publishPath = Join-Path $PSScriptRoot "bin\Release\net8.0-windows10.0.19041.0\win-x64\publish"
$executablePath = Join-Path $publishPath "SuzerainSaveEditor.exe"
$certificateBase64 = $env:CODE_SIGNING_CERTIFICATE_BASE64
$certificatePassword = $env:CODE_SIGNING_CERTIFICATE_PASSWORD
$timestampUrl = $env:CODE_SIGNING_TIMESTAMP_URL

if ([string]::IsNullOrWhiteSpace($certificateBase64) -or
    [string]::IsNullOrWhiteSpace($certificatePassword) -or
    [string]::IsNullOrWhiteSpace($timestampUrl)) {
    Write-Host "Code-signing configuration is incomplete; continuing without signing."
} else {
    $certificatePath = Join-Path $env:TEMP "suzerain-code-signing.pfx"
    try {
        $signTool = Get-ChildItem `
            -Path "${env:ProgramFiles(x86)}\Windows Kits\10\bin" `
            -Filter signtool.exe `
            -Recurse `
            -File |
            Where-Object { $_.FullName -match '\\x64\\signtool\.exe$' } |
            Sort-Object FullName -Descending |
            Select-Object -First 1

        if ($null -eq $signTool) {
            throw "signtool.exe was not found; set up the Windows SDK or continue without signing."
        }

        [IO.File]::WriteAllBytes($certificatePath, [Convert]::FromBase64String($certificateBase64))
        & $signTool.FullName sign /fd SHA256 /td SHA256 /tr $timestampUrl /f $certificatePath /p $certificatePassword $executablePath
        if ($LASTEXITCODE -ne 0) {
            throw "signtool.exe signing failed with exit code $LASTEXITCODE."
        }

        & $signTool.FullName verify /pa /all $executablePath
        if ($LASTEXITCODE -ne 0) {
            throw "signtool.exe verification failed with exit code $LASTEXITCODE."
        }
    } finally {
        if (Test-Path $certificatePath) {
            Remove-Item $certificatePath -Force
        }
    }
}

Write-Host "====================================="
Write-Host "Build complete! The executable is located at:"
Write-Host "bin\Release\net8.0-windows10.0.19041.0\win-x64\publish\SuzerainSaveEditor.exe"
Write-Host "====================================="

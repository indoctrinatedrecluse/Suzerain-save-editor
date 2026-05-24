# Stop script on any error
$ErrorActionPreference = "Stop"

# Find the first line in CHANGELOG.md that starts with 'v' followed by a version number
$version = (Get-Content -Path CHANGELOG.md | Select-String -Pattern '^v\d+\.\d+\.\d+' | Select-Object -First 1).Line

if (-not $version) {
    Write-Error "Error: Could not find a version string like 'v1.0.0' at the start of a line in CHANGELOG.md"
    exit 1
}

$version = $version.Trim()
Write-Host "Found version: $version"

# Add, commit, and tag
git add .
git commit -m "Release $version"
git tag -f $version

# Push the commit to master and the tag
Write-Host "Pushing commit to master and tag $version..."
git push origin master
git push origin $version --force

Write-Host "Push complete. Release workflow triggered."
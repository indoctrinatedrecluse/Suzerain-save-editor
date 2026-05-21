# Get the latest version from CHANGELOG.md
$latestVersion = (Get-Content -Path CHANGELOG.md | Select-String -Pattern "\[([0-9]+\.[0-9]+\.[0-9]+)\]" -AllMatches).Matches.Value.Trim('[]')
if (-not $latestVersion) {
    Write-Error "Could not find a version in CHANGELOG.md"
    exit 1
}

$tagName = "v$latestVersion"
Write-Host "Latest version: $tagName"

# Commit all changes
git add .
git commit -m "Release $tagName"

# Create a new tag
git tag $tagName

# Push the commit and the tag
git push
git push origin $tagName
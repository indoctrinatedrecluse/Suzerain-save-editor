# This script automates the release process.
# 1. Reads the version from the first line of CHANGELOG.md.
# 2. Commits all changes with a standardized message.
# 3. Tags the commit with the version.
# 4. Pushes the commit and the tag to the current branch.

# Get the version from the first line of the changelog
$version = (Get-Content -Path CHANGELOG.md -TotalCount 1).Trim()

if (-not $version) {
    Write-Error "Could not read version from CHANGELOG.md. Aborting."
    exit 1
}

# Get the current branch name
$currentBranch = git rev-parse --abbrev-ref HEAD
if (-not $currentBranch) {
    Write-Error "Could not determine the current git branch. Aborting."
    exit 1
}

Write-Host "Preparing release for version: $version on branch: $currentBranch"

# Add all changes to git
git add .

# Commit the changes
$commitMessage = "Release $version"
git commit -m $commitMessage

# Tag the commit
git tag $version

# Push the commit and the tag to the current branch
Write-Host "Pushing commit and tag to $currentBranch..."
git push origin $currentBranch
git push origin $version

Write-Host "Push complete. GitHub Action should now be triggered."

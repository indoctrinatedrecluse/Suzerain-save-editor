#!/bin/bash
# This script automates the release process.
# 1. Reads the version from the first line of CHANGELOG.md.
# 2. Commits all changes with a standardized message.
# 3. Tags the commit with the version.
# 4. Pushes the commit and the tag to the current branch.

# Get the version from the first line of the changelog
version=$(head -n 1 CHANGELOG.md | tr -d '[:space:]')

if [ -z "$version" ]; then
    echo "Could not read version from CHANGELOG.md. Aborting."
    exit 1
fi

# Get the current branch name
current_branch=$(git rev-parse --abbrev-ref HEAD)
if [ -z "$current_branch" ]; then
    echo "Could not determine the current git branch. Aborting."
    exit 1
fi

echo "Preparing release for version: $version on branch: $current_branch"

# Add all changes to git
git add .

# Commit the changes
commit_message="Release $version"
git commit -m "$commit_message"

# Tag the commit
git tag "$version"

# Push the commit and the tag to the current branch
echo "Pushing commit and tag to $current_branch..."
git push origin "$current_branch"
git push origin "$version"

echo "Push complete. GitHub Action should now be triggered."

#!/bin/bash
set -e

# Find the first line in CHANGELOG.md that starts with 'v' followed by a digit.
VERSION=$(grep -m 1 -oP '^v\d+\.\d+\.\d+' CHANGELOG.md)

if [ -z "$VERSION" ]; then
    echo "Error: Could not find a version string like 'v1.0.0' at the start of a line in CHANGELOG.md"
    exit 1
fi

echo "Found version: $VERSION"

# Add, commit, and tag
git add .
git commit -m "Release $VERSION"
git tag -f "$VERSION"

# Push the commit to master and the tag
echo "Pushing commit to master and tag $VERSION..."
git push origin master
git push origin "$VERSION" --force

echo "Push complete. Release workflow triggered."
#!/bin/bash

# Extract the latest version from CHANGELOG.md
# Looks for the first occurrence of [x.y.z]
LATEST_VERSION=$(grep -oP '\[\d+\.\d+\.\d+\]' CHANGELOG.md | head -n 1 | tr -d '[]')

if [ -z "$LATEST_VERSION" ]; then
    echo "Error: Could not find a version tag like [1.0.0] in CHANGELOG.md"
    exit 1
fi

TAG_NAME="v$LATEST_VERSION"
echo "Found version: $TAG_NAME"

# Add and commit all changes
git add .
git commit -m "Release $TAG_NAME"

# Create a tag
git tag "$TAG_NAME"

# Push the commit and the tag to remote
git push
git push origin "$TAG_NAME"
#!/bin/bash
set -e

if [ "$#" -ne 2 ] || [[ ! "$1" =~ ^v[0-9]+\.[0-9]+\.[0-9]+$ ]] || { [ "$2" != "sign" ] && [ "$2" != "nosign" ]; }; then
    echo "Usage: ./push.sh v1.4.0 [sign|nosign]"
    exit 1
fi

VERSION="$1"
SIGNING_MODE="$2"
echo "Preparing release $VERSION ($SIGNING_MODE)"

# Add, commit, and tag
git add .
git commit -m "Release $VERSION"
git tag -a -f "$VERSION" -m "$SIGNING_MODE"

# Push the commit to master and the tag
echo "Pushing commit to master and tag $VERSION..."
git push origin master
git push origin "$VERSION" --force

echo "Push complete. Release workflow triggered."
param(
    [Parameter(Mandatory = $true, Position = 0)]
    [ValidatePattern("^v\d+\.\d+\.\d+$")]
    [string]$Tag,
    [Parameter(Mandatory = $true, Position = 1)]
    [ValidateSet("sign", "nosign")]
    [string]$SigningMode
)

# Stop script on any error
$ErrorActionPreference = "Stop"

Write-Host "Preparing release $Tag ($SigningMode)"

# Add, commit, and tag
git add .
git commit -m "Release $Tag"
git tag -a -f $Tag -m $SigningMode

# Push the commit to master and the tag
Write-Host "Pushing commit to master and tag $Tag..."
git push origin master
git push origin $Tag --force

Write-Host "Push complete. Release workflow triggered."
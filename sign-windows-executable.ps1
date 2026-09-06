param(
    [Parameter(Mandatory = $true)]
    [string]$ExecutablePath,
    [string]$CertificateBase64 = $env:CODE_SIGNING_CERTIFICATE_BASE64,
    [string]$CertificatePassword = $env:CODE_SIGNING_CERTIFICATE_PASSWORD,
    [string]$TimestampUrl = $env:CODE_SIGNING_TIMESTAMP_URL,
    [bool]$GenerateSelfSigned = ($env:CODE_SIGNING_GENERATE_SELF_SIGNED -eq "true")
)

$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($CertificateBase64) -or
    [string]::IsNullOrWhiteSpace($CertificatePassword) -or
    [string]::IsNullOrWhiteSpace($TimestampUrl)) {
    if (-not $GenerateSelfSigned) {
        Write-Host "Code-signing configuration is incomplete; continuing without signing."
        exit 0
    }

    $TimestampUrl = if ([string]::IsNullOrWhiteSpace($TimestampUrl)) {
        "http://timestamp.digicert.com"
    } else {
        $TimestampUrl
    }

    $randomBytes = [byte[]]::new(32)
    [Security.Cryptography.RandomNumberGenerator]::Fill($randomBytes)
    $generatedPassword = [Convert]::ToBase64String($randomBytes)
    $CertificatePassword = $generatedPassword
    $certificate = New-SelfSignedCertificate `
        -Type CodeSigningCert `
        -Subject "CN=Suzerain Save Editor (Self-Signed)" `
        -KeyLength 2048 `
        -HashAlgorithm SHA256 `
        -KeyExportPolicy Exportable `
        -CertStoreLocation "Cert:\CurrentUser\My"
} else {
    $certificate = $null
}

$certificatePath = Join-Path $env:TEMP "suzerain-code-signing-$([guid]::NewGuid()).pfx"
$certificateThumbprint = if ($null -ne $certificate) { $certificate.Thumbprint } else { $null }

try {
    if ($null -ne $certificate) {
        Export-PfxCertificate `
            -Cert $certificate `
            -FilePath $certificatePath `
            -Password (ConvertTo-SecureString $CertificatePassword -AsPlainText -Force) |
            Out-Null
    } else {
        [IO.File]::WriteAllBytes($certificatePath, [Convert]::FromBase64String($CertificateBase64))
    }

    $signTool = Get-ChildItem `
        -Path "${env:ProgramFiles(x86)}\Windows Kits\10\bin" `
        -Filter signtool.exe `
        -Recurse `
        -File |
        Where-Object { $_.FullName -match '\\x64\\signtool\.exe$' } |
        Sort-Object FullName -Descending |
        Select-Object -First 1

    if ($null -eq $signTool) {
        throw "signtool.exe was not found."
    }

    & $signTool.FullName sign `
        /fd SHA256 `
        /td SHA256 `
        /tr $TimestampUrl `
        /f $certificatePath `
        /p $CertificatePassword `
        $ExecutablePath

    if ($LASTEXITCODE -ne 0) {
        throw "signtool.exe signing failed with exit code $LASTEXITCODE."
    }

    & $signTool.FullName verify /pa /all $ExecutablePath
    if ($LASTEXITCODE -ne 0) {
        throw "signtool.exe verification failed with exit code $LASTEXITCODE."
    }

    if ($null -ne $certificate) {
        Write-Host "Executable signed with an ephemeral self-signed certificate."
    } else {
        Write-Host "Executable signed and verified."
    }
} finally {
    if (Test-Path $certificatePath) {
        Remove-Item $certificatePath -Force
    }

    if ($null -ne $certificateThumbprint) {
        Remove-Item "Cert:\CurrentUser\My\$certificateThumbprint" -ErrorAction SilentlyContinue
    }
}

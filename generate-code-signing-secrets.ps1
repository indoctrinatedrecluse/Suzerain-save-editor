param(
    [string]$Subject = "CN=Suzerain Save Editor",
    [string]$OutputPath = (Join-Path $env:TEMP "suzerain-save-editor-codesign.pfx"),
    [string]$TimestampUrl = "http://timestamp.digicert.com",
    [switch]$CopyBase64ToClipboard
)

$ErrorActionPreference = "Stop"

if (-not (Get-Command New-SelfSignedCertificate -ErrorAction SilentlyContinue)) {
    throw "New-SelfSignedCertificate is unavailable. Run this script on Windows PowerShell with the PKI tools installed."
}

$password = Read-Host "Enter a password for the PFX certificate" -AsSecureString
$confirmation = Read-Host "Confirm the PFX certificate password" -AsSecureString

$passwordBytes = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($password)
$confirmationBytes = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($confirmation)
try {
    $passwordText = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($passwordBytes)
    $confirmationText = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($confirmationBytes)
} finally {
    [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($passwordBytes)
    [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($confirmationBytes)
}

if ($passwordText -ne $confirmationText) {
    throw "The passwords do not match."
}

if ([string]::IsNullOrWhiteSpace($passwordText)) {
    throw "The certificate password cannot be empty."
}

$certificate = New-SelfSignedCertificate `
    -Type CodeSigningCert `
    -Subject $Subject `
    -KeyLength 2048 `
    -HashAlgorithm SHA256 `
    -KeyExportPolicy Exportable `
    -CertStoreLocation "Cert:\CurrentUser\My"

try {
    Export-PfxCertificate -Cert $certificate -FilePath $OutputPath -Password $password | Out-Null
    $base64 = [Convert]::ToBase64String([IO.File]::ReadAllBytes($OutputPath))

    Write-Host ""
    Write-Host "Self-signed code-signing certificate created:"
    Write-Host $OutputPath
    Write-Host ""
    Write-Host "Set these GitHub repository secrets:"
    Write-Host "CODE_SIGNING_CERTIFICATE_BASE64:"
    Write-Host $base64
    Write-Host ""
    Write-Host "CODE_SIGNING_CERTIFICATE_PASSWORD:"
    Write-Host $passwordText
    Write-Host ""
    Write-Host "CODE_SIGNING_TIMESTAMP_URL:"
    Write-Host $TimestampUrl
    Write-Host ""
    Write-Host "Self-signed certificates show an unknown publisher unless the certificate is trusted on the target machine."

    if ($CopyBase64ToClipboard) {
        Set-Clipboard -Value $base64
        Write-Host "The Base64 certificate value was copied to the clipboard."
    }
} finally {
    Remove-Item "Cert:\CurrentUser\My\$($certificate.Thumbprint)" -ErrorAction SilentlyContinue
}

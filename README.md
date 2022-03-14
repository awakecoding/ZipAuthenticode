# Zip Authenticode

Do you already sign .msi, .exe, .dll, .ps1 and .cab files with Authenticode and just wish there was a simple way to make it work for .zip files? Look no further! Taking inspiration from the [zipsign](https://github.com/falk-werner/zipsign) project, we have adapted Authenticode to the zip file format in a way that leverages the existing Windows APIs for signing and validation.

## Self-signed Certificate

Start by creating a simple self-signed certificate for Authenticode code signing:

```powershell
$params = @{
    Subject = 'CN=ZipAuthenticode'
    Type = 'CodeSigning'
    CertStoreLocation = 'cert:\CurrentUser\My'
    HashAlgorithm = 'sha256'
}
$cert = New-SelfSignedCertificate @params
```

Find the certificate in the current user store, export it to a file without the private key, and import it into the system trusted root CAs (requires an elevated shell):

```powershell
$cert = @(Get-ChildItem cert:\CurrentUser\My -CodeSigning | Where-Object { $_.Subject -eq "CN=ZipAuthenticode" })[0]
$cert | Export-Certificate -FilePath "~\Documents\ZipAuthenticode.crt"
Import-Certificate -FilePath "~\Documents\ZipAuthenticode.crt" -CertStoreLocation "cert:\LocalMachine\Root"
```

Keep a one-liner command to obtain the correct certificate for code signing, as you will likely need it more than once. This command filters for code signing certificates in the current user certificate store that use the "ZipAuthenticode" subject name. You can also use the thumbprint to uniquely identify the certificate easily.

```powershell
PS C:\> $cert = @(Get-ChildItem cert:\CurrentUser\My -CodeSigning | Where-Object { $_.Subject -eq "CN=ZipAuthenticode" })[0]
PS C:\> $cert

   PSParentPath: Microsoft.PowerShell.Security\Certificate::CurrentUser\My

Thumbprint                                Subject              EnhancedKeyUsageList
----------                                -------              --------------------
6256DFDA7528DF20730950A4D9DC0727CE7EA404  CN=ZipAuthenticode   Code Signing
```

## PowerShell Module

Build the PowerShell module and import it:

```powershell
git clone https://github.com/awakecoding/ZipAuthenticode
cd ZipAuthenticode/PowerShell
dotnet build
Import-Module .\bin\Debug\netstandard2.0\Devolutions.Authenticode.PowerShell.dll
```

The `Get-ZipAuthenticodeSignature` and `Set-ZipAuthenticodeSignature` PowerShell cmdlets should now be available.

```powershell
Get-Command *ZipAuthenticode*

CommandType     Name                                               Version    Source
-----------     ----                                               -------    ------
Cmdlet          Get-ZipAuthenticodeSignature                       1.0.0.0    Devolutions.ZipAuthenticode.PowerShell
Cmdlet          Set-ZipAuthenticodeSignature                       1.0.0.0    Devolutions.ZipAuthenticode.PowerShell
```

## Signing a zip file

Copy a zip file into the current directory (you can use "data\test-unsigned.zip") and rename it to "test.zip". Get the SHA256 file hash of the original file and keep it for later:

```powershell
Get-FileHash test.zip -Algorithm SHA256

Algorithm       Hash                                                                   Path
---------       ----                                                                   ----
SHA256          4667433DD582F5955E7F6355CBB2A39C5E95CBCCC894C1FFAA4286F1ACFED0B7       test.zip
```

Fetch the code signing certificate object:

```powershell
$cert = @(Get-ChildItem cert:\CurrentUser\My -CodeSigning | Where-Object { $_.Subject -eq "CN=ZipAuthenticode" })[0]
```

And then call `Set-ZipAuthenticodeSignature` on the zip file using the certificate:

```powershell
Set-ZipAuthenticodeSignature .\test.zip $cert

SignerCertificate      : [Subject]
                           CN=ZipAuthenticode

                         [Issuer]
                           CN=ZipAuthenticode

                         [Serial Number]
                           1CEDD95663204A804AEA488546F1641F

                         [Not Before]
                           2022-03-12 3:10:04 PM

                         [Not After]
                           2023-03-12 4:30:04 PM

                         [Thumbprint]
                           6256DFDA7528DF20730950A4D9DC0727CE7EA404

TimeStamperCertificate :
Status                 : Valid
StatusMessage          : MshSignature_Valid
Path                   : test.zip.sig.ps1
SignatureType          : Authenticode
IsOSBinary             : False
```

Congratulations, you have just signed your first zip file using Authenticode!

## Validating Zip File Signature

Call `Get-ZipAuthenticodeSignature` to object the Authenticode signature on the signed zip file:

```powershell
Get-ZipAuthenticodeSignature .\test.zip

SignerCertificate      : [Subject]
                           CN=ZipAuthenticode

                         [Issuer]
                           CN=ZipAuthenticode

                         [Serial Number]
                           1CEDD95663204A804AEA488546F1641F

                         [Not Before]
                           2022-03-12 3:10:04 PM

                         [Not After]
                           2023-03-12 4:30:04 PM

                         [Thumbprint]
                           6256DFDA7528DF20730950A4D9DC0727CE7EA404

TimeStamperCertificate :
Status                 : Valid
StatusMessage          : MshSignature_Valid
Path                   : test.zip.sig.ps1
SignatureType          : Authenticode
IsOSBinary             : False
```

## Zip Signature Format

You may have noticed that the reported file path in the signature object is "test.zip.sig.ps1" instead "test.zip". This file is left over from the `Set-ZipAuthenticodeSignature` operation, so let's open it to see what it contains:

```
sha256:4667433dd582f5955e7f6355cbb2a39c5e95cbccc894c1ffaa4286f1acfed0b7
# SIG # Begin signature block
# MIIFbQYJKoZIhvcNAQcCoIIFXjCCBVoCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQURbYOY7I38yZeBoKx0kC3Iqp9
# vR6gggMIMIIDBDCCAeygAwIBAgIQHO3ZVmMgSoBK6kiFRvFkHzANBgkqhkiG9w0B
# AQsFADAaMRgwFgYDVQQDDA9aaXBBdXRoZW50aWNvZGUwHhcNMjIwMzEyMjAxMDA0
# WhcNMjMwMzEyMjAzMDA0WjAaMRgwFgYDVQQDDA9aaXBBdXRoZW50aWNvZGUwggEi
# MA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC5eftpY1WCaq0lZ4nYd43646x/
# rng40z/RrNFfxrAXtI1nLve5QQ75Das65xanvMjnehlXX2SweU1yy1X5tLvIeHb5
# KED4DI03q64Cn7QqtLYFQLFmv868ZIpSB2/URPDgSYn1i7s3yoxxpCjSCZgbaR1n
# HrwBTyDaQWbP5kLSwDo5sw4iehvXBXUmOnbknTa7N/iOy4s5bN/bJH0rtiEXQAWt
# /EvXO4cff4za4/mCBTCbK9ZjzNDlf5t9njd9J/myalYGnSjq04QqTfeUyuZ1RqFY
# zJWhKev/vUhUsBFtOexvz4UBYFU7WwDz4uNUiq24C09nQLhEs4OEGQ1IactZAgMB
# AAGjRjBEMA4GA1UdDwEB/wQEAwIHgDATBgNVHSUEDDAKBggrBgEFBQcDAzAdBgNV
# HQ4EFgQUCm9taG963syCKfZi5gRNYH/BBrIwDQYJKoZIhvcNAQELBQADggEBAHNC
# e+un62MBUuR81qo+2QUvDZLa0n2LV2HX8Co7ZhFIGR9b/dUTmRfsdvh2IkUhj6B8
# 9VObrntG0+DenZtuRpG10qM8uQMRJTY9OF2HoxPD5mK+NBOXT3oGyJFkv1hdypTR
# c2eOfgy7ea+bNC7MrWYEonpX0z0SMXNXezYcP2LaQdMHn7P5oGnGbE0CquxYH778
# i99Bd+EjZkkJrkPUSlh3TPZt1QCYhBGhS55csDiRGy1YkkmsiRDCowMZn355dEce
# viGuqxYdStXHIxykN9vKcMsc26FLdPACsZKJxlAyHfAoO1wh6eQY3au6d25GUok3
# fFvpExHPQvjBPKmGuxkxggHPMIIBywIBATAuMBoxGDAWBgNVBAMMD1ppcEF1dGhl
# bnRpY29kZQIQHO3ZVmMgSoBK6kiFRvFkHzAJBgUrDgMCGgUAoHgwGAYKKwYBBAGC
# NwIBDDEKMAigAoAAoQKAADAZBgkqhkiG9w0BCQMxDAYKKwYBBAGCNwIBBDAcBgor
# BgEEAYI3AgELMQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG9w0BCQQxFgQUHddqGBbW
# A9kaztmGxDrq6aBs/CowDQYJKoZIhvcNAQEBBQAEggEAlHr1eATZul6XjQXQ4vOD
# skD/6s+ILkCX3hKE38C267zbbu8KoIQrTFJV8jfftwCQbsj4M5l1l19nLkpntQDk
# GS7r3/MeNOf9+4eQe5YVBzfqQsHEoywfkyHw6upuj+rsp0ZbWw81E0QQG6Ap2cM7
# 6XBWi7V9b3v7AgfB3g8eq0aG8N+54Rq+ci0fPbRv4HBq0P4AMHvA4rUUP3tn/pTB
# J/y1DJc87e08dPcwXEzZHDB2Vxq8BR8CRHvVAR01DBjggLJQBSKjk9Vs50WDKR5r
# ry4QusxeMUwgvJzXBei8VSFvWuezGySkSVCa/lIMZ3+w4P6z9nHF/1sXO9qsuJTo
# +g==
# SIG # End signature block
```

Since Authenticode doesn't support the zip file format natively, we use a file hash of the zip file as if it had no comment appended at the end of it. We convert this hash to the [OCI digest string](https://github.com/opencontainers/image-spec/blob/v1.0.1/descriptor.md#digests) string format and create a one-line .sig.ps1 file with it. This file is then signed like a PowerShell script, except it doesn't contain executable code. The digest string and the signature block are then reformatted to be embedded as a single-line comment inside the zip file format, like this:

```
ZipAuthenticode=sha256:4667433dd582f5955e7f6355cbb2a39c5e95cbccc894c1ffaa4286f1acfed0b7,MIIFbQYJKoZIhvcNAQcCoIIFXjCCBVoCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQBgjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNRAgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQURbYOY7I38yZeBoKx0kC3Iqp9vR6gggMIMIIDBDCCAeygAwIBAgIQHO3ZVmMgSoBK6kiFRvFkHzANBgkqhkiG9w0BAQsFADAaMRgwFgYDVQQDDA9aaXBBdXRoZW50aWNvZGUwHhcNMjIwMzEyMjAxMDA0WhcNMjMwMzEyMjAzMDA0WjAaMRgwFgYDVQQDDA9aaXBBdXRoZW50aWNvZGUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC5eftpY1WCaq0lZ4nYd43646x/rng40z/RrNFfxrAXtI1nLve5QQ75Das65xanvMjnehlXX2SweU1yy1X5tLvIeHb5KED4DI03q64Cn7QqtLYFQLFmv868ZIpSB2/URPDgSYn1i7s3yoxxpCjSCZgbaR1nHrwBTyDaQWbP5kLSwDo5sw4iehvXBXUmOnbknTa7N/iOy4s5bN/bJH0rtiEXQAWt/EvXO4cff4za4/mCBTCbK9ZjzNDlf5t9njd9J/myalYGnSjq04QqTfeUyuZ1RqFYzJWhKev/vUhUsBFtOexvz4UBYFU7WwDz4uNUiq24C09nQLhEs4OEGQ1IactZAgMBAAGjRjBEMA4GA1UdDwEB/wQEAwIHgDATBgNVHSUEDDAKBggrBgEFBQcDAzAdBgNVHQ4EFgQUCm9taG963syCKfZi5gRNYH/BBrIwDQYJKoZIhvcNAQELBQADggEBAHNCe+un62MBUuR81qo+2QUvDZLa0n2LV2HX8Co7ZhFIGR9b/dUTmRfsdvh2IkUhj6B89VObrntG0+DenZtuRpG10qM8uQMRJTY9OF2HoxPD5mK+NBOXT3oGyJFkv1hdypTRc2eOfgy7ea+bNC7MrWYEonpX0z0SMXNXezYcP2LaQdMHn7P5oGnGbE0CquxYH778i99Bd+EjZkkJrkPUSlh3TPZt1QCYhBGhS55csDiRGy1YkkmsiRDCowMZn355dEceviGuqxYdStXHIxykN9vKcMsc26FLdPACsZKJxlAyHfAoO1wh6eQY3au6d25GUok3fFvpExHPQvjBPKmGuxkxggHPMIIBywIBATAuMBoxGDAWBgNVBAMMD1ppcEF1dGhlbnRpY29kZQIQHO3ZVmMgSoBK6kiFRvFkHzAJBgUrDgMCGgUAoHgwGAYKKwYBBAGCNwIBDDEKMAigAoAAoQKAADAZBgkqhkiG9w0BCQMxDAYKKwYBBAGCNwIBBDAcBgorBgEEAYI3AgELMQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG9w0BCQQxFgQUHddqGBbWA9kaztmGxDrq6aBs/CowDQYJKoZIhvcNAQEBBQAEggEAlHr1eATZul6XjQXQ4vODskD/6s+ILkCX3hKE38C267zbbu8KoIQrTFJV8jfftwCQbsj4M5l1l19nLkpntQDkGS7r3/MeNOf9+4eQe5YVBzfqQsHEoywfkyHw6upuj+rsp0ZbWw81E0QQG6Ap2cM76XBWi7V9b3v7AgfB3g8eq0aG8N+54Rq+ci0fPbRv4HBq0P4AMHvA4rUUP3tn/pTBJ/y1DJc87e08dPcwXEzZHDB2Vxq8BR8CRHvVAR01DBjggLJQBSKjk9Vs50WDKR5rry4QusxeMUwgvJzXBei8VSFvWuezGySkSVCa/lIMZ3+w4P6z9nHF/1sXO9qsuJTo+g==
```

To validate the signature, we extract lines beginning with "ZipAuthenticode" from the zip file comment field, and reconstruct original script formatting. We then compute the zip file digest excluding the comment field itself, compare it with the digest embedded in the signature, and validate the signature file as a PowerShell script. If the digest strings match and the signature on the script is valid, then the zip file is correctly signed.

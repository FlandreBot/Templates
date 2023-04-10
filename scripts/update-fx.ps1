param (
    [Parameter(Mandatory)]
    [string] $FrameworkVersion
)

$previousLocation = Get-Location

# Check current location
if ((Get-Location).Path -eq $PSScriptRoot) {
    $RepoRoot = ".."
}
elseif (Test-Path "Flandre.Templates.csproj") {
    $RepoRoot = "."
}
else {
    Write-Error "请在仓库根目录执行本脚本。"
    return
}
Set-Location $RepoRoot

$FrameworkVersion = $FrameworkVersion.TrimStart('v')

$FrameworkTemplateProjectPath = Join-Path $PSScriptRoot .. templates "Flandre.Templates.Framework" "Flandre.Templates.Framework.csproj"
$TemplatesRootProjectPath = Join-Path $PSScriptRoot .. "Flandre.Templates.csproj"

Write-Output "Updating Flandre.Templates.Framework version..."
$content = [System.IO.File]::ReadAllText($FrameworkTemplateProjectPath)
[System.IO.File]::WriteAllText(
        $FrameworkTemplateProjectPath,
        [Regex]::Replace(
                $content,
                "(?<=<PackageReference.*Include=""Flandre\.Framework"".*Version="").*(?="".*/>)",
                $FrameworkVersion
        )
)

Write-Output "Updating Flandre.Templates version..."
$content = [System.IO.File]::ReadAllText($TemplatesRootProjectPath)
[System.IO.File]::WriteAllText(
        $TemplatesRootProjectPath,
        [Regex]::Replace(
                $content,
                "(?<=<PackageVersion>).*(?=</PackageVersion>)",
                $FrameworkVersion
        )
)

Write-Output "Committing..."
$releaseMessage = "release(templates-fx): v" + $FrameworkVersion
git add .
git commit -m $releaseMessage

Write-Output "Pushing..."
git push origin main

Set-Location $previousLocation

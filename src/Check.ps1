#!/usr/bin/env pwsh

<#
This script runs locally the checks from the continous integration.
#>

function Main
{
    Push-Location

    Set-Location $PSScriptRoot

    $nl = [System.Environment]::NewLine

    Write-Host "Check.ps1: Checking the format...$nl"
    dotnet format --verify-no-changes
    if ($LASTEXITCODE -ne 0)
    {
        throw "Format check failed."
    }

    $env:AAS_CORE_AAS3_0_RC02_TESTS_TEST_DATA_DIR = (
        Join-Path (Split-Path $PSScriptRoot -Parent) "test_data"
    )

    Write-Host "${nl}Check.ps1: Running the unit tests...${nl}"
    dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
    if ($LASTEXITCODE -ne 0)
    {
        throw "The unit tests failed."
    }

    Write-Host "${nl}Check.ps1: Inspecting the code with JetBrains InspectCode...${nl}"
    & "$PSScriptRoot\InspectCode.ps1"

    $outDir = Join-Path (Split-Path -Parent $PSScriptRoot) "out"
    Write-Host "${nl}Check.ps1: Publishing to $outDir ... ${nl}"
    dotnet publish -c Release -o $outDir

    Pop-Location
}

Main

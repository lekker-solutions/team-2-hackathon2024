
Write-Host " ----------------------------- "
Write-Host "      INITIAL REPO RENAMER     "
Write-Host " ----------------------------- "
Write-Host
Write-Host
Write-Host
Write-Host " ----------------------------- "
Write-Host "    Provide the Project Name   "
Write-Host " ----------------------------- "
$projName = Read-Host

Write-Host
Write-Host
Write-Host
Write-Host " ----------------------------- "
Write-Host "    Provide the Client Name    "
Write-Host " ----------------------------- "
$clientName = Read-Host

Write-Host
Write-Host
Write-Host
Write-Host " ----------------------------- "
Write-Host "  Provide the Client Full Name "
Write-Host " ----------------------------- "
$clientFullName = Read-Host

Write-Host
Write-Host
Write-Host
Write-Host " ----------------------------- "
Write-Host "   Provide the Solution Name   "
Write-Host " ----------------------------- "
$solutionName = Read-Host

Write-Host
Write-Host
Write-Host
Write-Host " ----------------------------- "
Write-Host "      Provide the Version   "
Write-Host " ----------------------------- "
$versionName = Read-Host
$versionNameUnderscore = $versionName.Replace(".", "_")

$curDir = Get-Location
[RegEx]$projNameSearch = 'TemplateProject'
[RegEx]$clientNameSearch = 'TemplateClient'
[RegEx]$clientFullNameSearch = 'TemplateClientName'
[RegEx]$solutionNameSearch = 'TemplateSolution'
[RegEx]$versionNameSearch = 'TemplateVersion'
[RegEx]$versionNameUnderscoreSearch = 'TemplateVersionUnderscore'

Write-Host 
Write-Host 
Write-Host "---------------------------"
Write-Host "       Renaming Files"
Write-Host "---------------------------"

# Edit Names
Get-ChildItem -Path $curDir -Recurse -Filter *$($projNameSearch)*  -Directory | Rename-Item -NewName {$_.name -replace $projNameSearch,$projName } 
Get-ChildItem -Path $curDir -Recurse -Filter *$($clientNameSearch)*  -Directory | Rename-Item -NewName {$_.name -replace $clientNameSearch,$clientName }
Get-ChildItem -Path $curDir -Recurse -Filter *$($clientFullNameSearch)*  -Directory | Rename-Item -NewName {$_.name -replace $clientFullNameSearch,$clientFullName }
Get-ChildItem -Path $curDir -Recurse -Filter *$($solutionNameSearch)*  -Directory | Rename-Item -NewName {$_.name -replace $solutionNameSearch,$solutionName }
Get-ChildItem -Path $curDir -Recurse -Filter *$($versionNameUnderscoreSearch)* -Directory | Rename-Item -NewName {$_.name -replace $versionNameUnderscoreSearch,$versionNameUnderscore }
Get-ChildItem -Path $curDir -Recurse -Filter *$($versionNameSearch)* -Directory | Rename-Item -NewName {$_.name -replace $versionNameSearch,$versionName }

Get-ChildItem -Path $curDir -Recurse -File | Rename-Item -NewName {$_.fullname -replace $projNameSearch,$projName } 
Get-ChildItem -Path $curDir -Recurse -File | Rename-Item -NewName {$_.fullname -replace $clientNameSearch,$clientName }
Get-ChildItem -Path $curDir -Recurse -File | Rename-Item -NewName {$_.fullname -replace $clientFullNameSearch,$clientFullName }
Get-ChildItem -Path $curDir -Recurse -File | Rename-Item -NewName {$_.fullname -replace $solutionNameSearch,$solutionName }
Get-ChildItem -Path $curDir -Recurse -File | Rename-Item -NewName {$_.fullname -replace $versionNameUnderscoreSearch,$versionNameUnderscore }
Get-ChildItem -Path $curDir -Recurse -File | Rename-Item -NewName {$_.fullname -replace $versionNameSearch,$versionName }



Write-Host 
Write-Host 
Write-Host "---------------------------"
Write-Host "     Renaming Content      "
Write-Host "---------------------------"

# Edit content
ForEach ($File in (Get-ChildItem -Path $curDir -Recurse -File)) {
    (Get-Content $File) -Replace $projNameSearch,$projName |
        Set-Content $File
}
ForEach ($File in (Get-ChildItem -Path $curDir -Recurse -File)) {
    (Get-Content $File) -Replace $clientNameSearch,$clientName |
        Set-Content $File
}
ForEach ($File in (Get-ChildItem -Path $curDir -Recurse -File)) {
    (Get-Content $File) -Replace $clientFullNameSearch,$clientFullName |
        Set-Content $File
}
ForEach ($File in (Get-ChildItem -Path $curDir -Recurse -File)) {
    (Get-Content $File) -Replace $solutionNameSearch,$solutionName |
        Set-Content $File
}
ForEach ($File in (Get-ChildItem -Path $curDir -Recurse -File)) {
    (Get-Content $File) -Replace $versionNameUnderscoreSearch,$versionNameUnderscore |
        Set-Content $File
}
ForEach ($File in (Get-ChildItem -Path $curDir -Recurse -File)) {
    (Get-Content $File) -Replace $versionNameSearch,$versionName |
        Set-Content $File
}


Write-Host " ----------------------------- "
Write-Host "           COMPLETE          "
Write-Host " ----------------------------- "

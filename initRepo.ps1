$path = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Definition)
$name = "CarbonAccounting"
function InstallSite([string] $version){
    $versionString = $version.Replace(".", "_").Substring(0,4);
    $path = "$($path)\$($name)_$($versionString)";
    if(Test-Path "$($path)\web.config"){
        Write-Output "Version $($version)" Exists Already
        return;
    }
    Write-Output "Installing $version"
    Add-AcuSite -nv "$($name)_$($versionString)" -v $version -p $path
}

InstallSite("23.207.0025")

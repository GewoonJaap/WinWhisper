@echo off

:: Clean up Publish folder if exists, remove all contents then create new one
if exist ".\Publish" (
    rmdir /s /q ".\Publish"
)
mkdir ".\Publish"

:: Ask for the new version number
set /p version="New version number (e.g. 1.0.0): "

:: Install VeloPack (vpk) tool
dotnet tool update -g vpk

:: Ensure the global tools path is in the PATH environment variable
set PATH=%PATH%;%USERPROFILE%\.dotnet\tools

:: Publish the .NET application
dotnet publish -c Release --self-contained -r win-x64 -o ".\Publish" /p:AssemblyVersion=%version% /p:FileVersion=%version% /p:InformationalVersion=%version%

:: Set the URL for downloading the currently live version
set LIVE_VERSION_URL=https://winwhisper.ams3.digitaloceanspaces.com

:: Download the currently live version
vpk download http --url "%LIVE_VERSION_URL%"

:: Build new version and delta updates
vpk pack ^
 -u "WinWhisper" ^
 -v "%version%" ^
 -p ".\Publish" ^
 -e "WinWhisper.exe" ^
 -i ".\ConsoleApp\WinWhisper-White_Icon.ico" ^
 -s ".\Assets\Cover\1x\WinWhisper-Blue_Cover.png" ^
 -o ".\Releases"

:: Create a .isStandalone file in the Publish directory to indicate that this is a standalone release
echo 1 > .\Publish\.isStandalone

:: Zip all files in Publish to WinWhisper-%version%-standalone.zip
powershell Compress-Archive -Path ".\Publish\*" -DestinationPath ".\Publish\WinWhisper-%version%-standalone.zip"
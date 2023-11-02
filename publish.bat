@echo off

set /p version="New version number (e.g. 1.0.0): "
dotnet publish -c Release -o ".\Publish" /p:AssemblyVersion=%version% /p:FileVersion=%version% /p:InformationalVersion=%version%

:: Set the path to Squirrel.exe
set SQUIRREL_PATH=.\SquirrelTools\Squirrel.exe

:: Download the currently live version
%SQUIRREL_PATH% http-down --url "https://winwhisper.ams3.digitaloceanspaces.com"

:: Ask for the new version number

:: Build new version and delta updates
%SQUIRREL_PATH% pack ^
 --framework net7,vcredist143-x86 ^
 --packId "WinWhisper" ^
 --packVersion %version% ^
 --packAuthors "Jaap" ^
 --packDir ".\Publish" ^
 --icon ".\ConsoleApp\WinWhisper-White_Icon.ico" ^
 --splashImage ".\Assets\Cover\1x\WinWhisper-Blue_Cover.png" ^
 --releaseDir ".\Releases"

:: Zip all files in Publish to WinWHisper-<version>-standalone.zip
powershell Compress-Archive -Path ".\Publish\*" -DestinationPath ".\Publish\WinWhisper-%version%-standalone.zip"
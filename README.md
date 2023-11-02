![WinWhisper-Blue_Cover](https://github.com/GewoonJaap/WinWhisper/assets/33700526/dcd0d5e4-1d09-4697-95ec-15d12a712dd3)


# WinWhisper
Whisper AI for Windows, built with C# and [Whisper.NET](https://github.com/sandrohanea/whisper.net)

## What is WinWhisper?
WinWhisper is a Windows application that uses [Whisper.NET](https://github.com/sandrohanea/whisper.net) to provide a simple console interface for interacting with Whisper AI.

### Supported Operating Systems
- Windows 64-bit
- Windows on ARM64 (Check out the [Windows 2023 Dev kit](https://learn.microsoft.com/en-us/windows/arm/dev-kit/))
- Linux 64-bit (Not tested but should work, if not please create an issue)
- Linux on ARM64 (Not tested but should work, if not please create an issue)
- MacOS 64-bit (Not tested but should work, if not please create an issue)
- MacOS on ARM64 (Not tested but should work, if not please create an issue)

### Showcase
![WinWhisper showcase](https://user-images.githubusercontent.com/33700526/222954203-adb416b6-9fe3-490b-b33f-9051e9579031.gif)


With WinWhisper you can easily generate subtitles for your videos. These subtitles will be exported in .SRT format, which is supported by most video players.

## How to use WinWhisper?
1. Download the latest release from the [Releases Page](https://github.com/GewoonJaap/WinWhisper/releases)
2. Download the latest DotNet 7.0.X Runtime Installer from [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).
![afbeelding](https://github.com/GewoonJaap/WinWhisper/assets/33700526/10d9684f-ba22-4d70-b43c-f8f56029e045)

3. Run the installer.
4. Extract the WinWhisper zip file
5. Run WinWhisper.exe
6. Enter the path to your video file (Simply drag the video file into the console)
7. Enter the languagecode (en, nl, de etc) of the video, or leave it blank for automatic language detection
8. Wait for the subtitles to be generated. They will be saved in the `Subtitles` folder, located in the same folder as the executable.
9. Enjoy your subtitles!

## How to build WinWhisper?
1. Clone this repository
2. Open the solution in Visual Studio 2022
3. Build the solution
4. Run WinWhisper.exe

## How to contribute?
1. Fork this repository
2. Create a new branch
3. Make your changes
4. Create a pull request


## How to generate a publishable release?
1. Open the `publish.bat` file
2. Enter the new version number
3. Upload all files inside `Releases` to the storage bucket
4. Create a new release on GitHub
5. Upload the WinWhisperSetup.exe file to the release


## Thanks to
- [@hirowa](https://github.com/hirowa) for creating the WinWhisper logo and banner ❤️


![WinWhisper-Blue_Footer0 5x](https://github.com/GewoonJaap/WinWhisper/assets/33700526/39db2529-f17d-476e-a67b-c7ddc86438f5)


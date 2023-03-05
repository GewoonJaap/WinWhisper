![Header image](https://user-images.githubusercontent.com/33700526/222953487-2e7c4ec4-ce4e-4675-ae2f-ea5972aef669.png)

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
2. Extract the zip file
3. Run WinWhisper.exe
4. Enter the path to your video file (Simply drag the video file into the console)
5. Enter the languagecode (en, nl, de etc) of the video, or leave it blank for automatic language detection
6. Wait for the subtitles to be generated. They will be saved in the `Subtitles` folder, located in the same folder as the executable.
7. Enjoy your subtitles!

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


![Logo](https://user-images.githubusercontent.com/33700526/222953513-d2122c07-4bac-4169-9ce1-16ffe74273c6.png)

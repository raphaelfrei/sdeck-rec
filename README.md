# 🎮 Steam Deck - Convert Recording to Video Files

A simple Windows tool to **automate the conversion of Steam Deck recordings** (`.mpd`) into playable `.mp4` video files using **FFmpeg**.

</tr>

## 📌 Purpose

Steam Deck saves recordings in the **MPEG-DASH format**, typically producing `.mpd` files alongside `.m4s` segments. Converting these recordings manually using FFmpeg can be repetitive and time-consuming.

This tool automates the process, allowing you to:
- Convert **multiple recordings** from entire folders (batch mode)
- Monitor progress through **visual progress bars**

</tr>

## 🛠 Features

- 🔍 Scan and list all `.mpd` files in a selected root folder  
- 📤 Choose an output directory for the final `.mp4` files  
- 📁 Batch convert multiple files in one go  
- ⏱ Progress bars for both global and per-file conversion  
- ⚙️ FFmpeg integration (included in `libs/ffmpeg.exe`)  

</tr>

## ▶️ How to Use

1. **Copy your Steam Deck recordings** to your computer. These are usually found under:
````/home/deck/Videos/SteamDeck/````

2. **Open the application.**
3. Click **"Root Directory"** to select the folder containing the `.mpd` files.
4. Click **"Output Folder"** to select the destination folder for the `.mp4` files.
5. Click **"Convert"** to start the conversion process.
6. Sit back and relax while the tool processes all files automatically.

</tr>

## 🧩 Requirements

- Windows 10 or higher  
- [.NET 8.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- FFmpeg (included in the project under `libs/ffmpeg.exe`)

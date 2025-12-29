# Secretary of War Simulator v2.0
## Operation Southern Spear

A satirical GTA V / LSPDFR mod that lets you play as the Secretary of War, conducting "operations" against narco-terrorist vessels with increasingly hilarious political and bureaucratic consequences.

## 🎮 Features

### Core Gameplay
- **Wave-Based Missions**: Fight through multiple waves of increasingly difficult narco-boat encounters
- **Attack Helicopter Combat**: Spawn in various military helicopters (Hunter, Buzzard, Savage, Annihilator)
- **Dynamic Spawn Locations**: Random deployment points across the map (Vespucci Beach, LSIA, Sandy Shores, Fort Zancudo)
- **Progressive Difficulty**: Each wave adds more boats and increases the challenge

### 🆕 Version 2.0 Features

#### Escalating Political Consequences
Watch as your "decisive military actions" create increasing political fallout:
- Congressional hearings scheduled
- Twitter trends (#WarCriminal, #SecretaryOfWar, #ImpeachNow)
- House impeachment resolutions
- UN Security Council emergency sessions
- International arrest warrant threats
- Mission-end political status summary

#### Media Spin System
Experience both sides of the media spectrum:
- **Liberal Outlets** (CNN, MSNOW, NPR): Impeachment questions, environmental concerns, war crimes allegations
- **Conservative Outlets** (Fox News, Newsmax, OANN): Praise for "strong leadership," America First messaging, veteran approval

#### Bureaucracy Humor
Navigate the absurdity of military bureaucracy:
- Pentagon budget concerns: *"$4 million in missiles on a $50,000 boat"*
- State Department approval delays: *"6-8 weeks for diplomatic review"*
- Budget Committee: *"Have you considered... just asking them nicely to stop?"*
- Legal Department: *"47-page Rules of Engagement manual, updated this morning"*
- HR overtime approval requirements
- Environmental Impact Statements

#### Progressive Victory Audio
Celebrate with escalating intensity:
- **Wave 1**: Short victory audio
- **Wave 2**: Standard victory audio
- **Wave 3 / Mission Complete**: Full victory celebration
- Victory audio never interrupts combat or gets cut off

#### Configurable Notification Duration
Adjust how long notifications stay on screen so you can read and enjoy them:
- Default: 8 seconds
- Range: 3-15 seconds
- Perfect for savoring the satirical humor

## 📋 Requirements

- **GTA V** (PC)
- **Rage Plugin Hook (RPH)** - [Download](http://ragepluginhook.net/)
- **LSPDFR** (optional but recommended) - [Download](https://www.lcpdfr.com/downloads/gta5mods/g17media/7792-lspd-first-response/)
- **NAudio.dll** - Included with LSPDFR or available separately

## 💾 Installation

1. **Install Prerequisites**:
   - Install GTA V
   - Install Rage Plugin Hook
   - Install LSPDFR (optional)

2. **Download the Mod**:
   - Download the latest release from the [Releases page](https://github.com/yourusername/SecretaryOfWar/releases)
   - Extract the ZIP file

3. **Install Plugin Files**:
   ```
   Grand Theft Auto V/
   ├── Plugins/
   │   ├── NarcoBoatsRPH.dll
   │   └── NarcoBoatsRPH.ini (auto-generated on first run)
   ```

4. **Install Audio Files**:
   ```
   Grand Theft Auto V/
   └── lspdfr/
       └── audio/
           └── scanner/
               └── NarcoBoats/
                   ├── trump_victory_short.wav
                   ├── trump_victory.wav
                   ├── trump_victory_long.wav
                   └── [your other audio files].wav
   ```

5. **Launch the Game**:
   - Start GTA V via Rage Plugin Hook
   - The mod will load automatically
   - Press **F6** (default) to start a mission

## 🔄 Upgrading from Version 1.0

**⚠️ IMPORTANT**: Before running v2.0 for the first time:

1. **Delete your old configuration file**:
   - Navigate to `Grand Theft Auto V/Plugins/`
   - Delete `NarcoBoatsRPH.ini`
   - The mod will generate a new one with all v2.0 settings

2. **Add new victory audio files** (see Installation step 4 above)

3. **Your existing audio files will continue to work** - no changes needed!

## ⚙️ Configuration

The mod generates a configuration file at `Plugins/NarcoBoatsRPH.ini` on first launch.

### Basic Settings
```ini
# Mission hotkey (default F6)
MissionHotkey=F6

# Starting boats in first wave (1-15)
StartingBoatCount=5

# Additional boats per wave (1-5)
BoatsPerWave=3

# Maximum waves before mission complete (1-10)
MaxWaves=3

# Helicopter model (HUNTER, BUZZARD, SAVAGE, ANNIHILATOR)
HelicopterModel=HUNTER

# Spawn location (Beach, Airport, Sandy, Fort, Random)
SpawnLocation=Random
```

### Version 2.0 Settings
```ini
# How long notifications stay on screen (3000-15000 milliseconds)
# 8000 = 8 seconds (default), 10000 = 10 seconds (recommended)
NotificationDuration=8000

# Victory audio configuration
EnableVictoryAudio=true
PlayVictoryAudioAfterEachWave=true
UseProgressiveVictoryAudio=true
VictoryAudioShort=trump_victory_short.wav
VictoryAudioNormal=trump_victory.wav
VictoryAudioLong=trump_victory_long.wav

# Feature toggles (set to false to disable)
EnablePoliticalConsequences=true
EnableMediaSpin=true
EnableBureaucracyHumor=true
```

### Audio Settings
```ini
# Enable/disable radio chatter
EnableAudio=true

# Volume (0.0 to 1.0)
AudioVolume=0.5

# Audio folder path
AudioFolder=lspdfr\audio\scanner\NarcoBoats\
```

## 🎮 How to Play

1. **Launch the game** via Rage Plugin Hook
2. **Press F6** (or your configured hotkey) to start a mission
3. **Fly to the hostile boats** marked in red on your map
4. **Destroy all boats** using your helicopter's weapons
5. **Complete all waves** to finish the mission
6. **Watch the political fallout** unfold as you rack up kills!

### Tips
- The more boats you destroy, the higher your "political heat" level
- Media spin messages appear randomly during combat
- Bureaucracy humor shows up between waves
- Your political status is summarized at mission end
- All v2.0 features can be toggled on/off in the INI if desired

## 🎵 Custom Audio

### Adding Your Own Audio
1. Place WAV files in `lspdfr/audio/scanner/NarcoBoats/`
2. Any file **NOT** containing "trump_victory" will be used as random radio chatter
3. Files containing "trump_victory" are reserved for victory celebrations

### Audio File Requirements
- Format: WAV (44.1kHz recommended)
- Victory audio files: `trump_victory_short.wav`, `trump_victory.wav`, `trump_victory_long.wav`
- Radio chatter: Any WAV file without "trump_victory" in the filename

## 🐛 Troubleshooting

### Mod doesn't load
- Ensure Rage Plugin Hook is installed correctly
- Check `RagePluginHook.log` for errors
- Verify `NarcoBoatsRPH.dll` is in the `Plugins/` folder

### No audio playing
- Verify audio files are in `lspdfr/audio/scanner/NarcoBoats/`
- Check that `EnableAudio=true` in the INI
- Ensure files are in WAV format
- Check `RagePluginHook.log` for audio errors

### Audio files are corrupt or won't play
- Re-download or re-convert audio files
- Ensure they're proper WAV format (not renamed MP3s)
- Try reducing the audio file size/length

### Victory audio plays during combat
- This was fixed in v2.0 - make sure you're using the latest version
- Victory audio files should contain "trump_victory" in the filename

### Notifications disappear too quickly
- Increase `NotificationDuration` in the INI (recommend 10000 for 10 seconds)
- Range is 3000-15000 milliseconds

### Boats spawn incorrectly
- This is usually fixed by restarting the mission
- Try a different spawn location in the INI
- Check RagePluginHook.log for spawn errors

## 📝 Known Issues

- Drug props may occasionally not attach to boats (visual only, doesn't affect gameplay)
- Very high drug prop counts (>15) may cause performance issues
- Audio cleanup on rapid mission restarts may occasionally cause brief audio stutter

## 🤝 Contributing

Contributions are welcome! If you have ideas for:
- New media outlet messages
- Additional bureaucracy humor
- More political consequence escalations
- Bug fixes or optimizations

Please open an issue or submit a pull request on GitHub.

## 📄 License

This mod is released under the MIT License. See LICENSE file for details.

## 🙏 Credits

- **Development**: TheTechSupportDude
- **Rage Plugin Hook**: By the RPH development team
- **NAudio**: For audio playback functionality
- **Inspiration**: Current world events and political satire

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/yourusername/SecretaryOfWar/issues)
- **Discord**: https://discord.gg/Q8RNd2xKAH 
- **LSPDFR Forums**: https://www.lcpdfr.com/downloads/gta5mods/scripts/52879-secretary-of-war-simulator-operation-southern-spear/ 

## 📜 Changelog

See [CHANGELOG.md](CHANGELOG.md) for full version history and detailed changes.

---

## ⚖️ Disclaimer

This mod is a work of satire and parody. It is not affiliated with, endorsed by, or representative of any government entity, political party, news organization, or military operation. Any resemblance to real persons, organizations, or events is purely coincidental and intended for entertainment purposes only.

---

**Enjoy the chaos, Mr. Secretary!** 🚁💥

*"Sir, the Pentagon wants to know why you used $4 million in missiles on a $50,000 boat..."*

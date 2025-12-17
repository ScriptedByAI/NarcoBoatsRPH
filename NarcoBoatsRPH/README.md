# Secretary of War Simulator: Operation Southern Spear

A RagePluginHook plugin for GTA V that puts you in command of coastal defense operations against narco-terrorist vessels.

## 🚁 Features

### Combat System
- **Wave-Based Combat**: Face increasingly difficult waves of hostile boats
- **Progressive Difficulty**: Each wave spawns more boats than the last
- **Dynamic Targeting**: AI-controlled boats attempt evasive maneuvers
- **Explosive Combat**: Spectacular explosions and fire effects on boat destruction

### Customization
- **Multiple Helicopter Options**: Hunter, Buzzard, Savage, or Annihilator
- **Flexible Spawn Locations**: Beach, Fort Zancudo, Sandy Shores, Airport, or random deployment
- **Full INI Configuration**: Customize difficulty, boats, waves, and more
- **Adjustable Drug Cargo**: Control visual contraband package density

### Immersion Features
- **AI Radio Chatter**: Optional military communications during combat (WAV files)
- **Drug Cargo Visualization**: Boats loaded with visible contraband packages
- **Enhanced HUD**: Color-coded mission stats with real-time tracking
- **Performance Ratings**: Get rated on your combat effectiveness
- **Career Statistics**: Track missions completed and personal records
- **Mission Briefings**: Detailed operation orders before each deployment

## 📋 Requirements

- Grand Theft Auto V
- RagePluginHook (latest version)
- LSPDFR (optional but recommended)
- EUP (optional but recommended)

## 🔧 Installation

**EASY INSTALLATION - Everything is pre-organized!**

1. **Download the latest release** and extract the ZIP file

2. **Copy all files to your GTA V directory:**
   - Simply drag the `lspdfr` and `plugins` folders into your GTA V root directory
   - Drag all the `.dll` files (NAudio files and RAGENativeUI.dll) into your GTA V root directory
   
   Your GTA V folder should look like this:
   ```
   Grand Theft Auto V/
   ├── NAudio.dll
   ├── NAudio_Asio.dll
   ├── NAudio_Core.dll
   ├── NAudio_Midi.dll
   ├── NAudio_Wasapi.dll
   ├── NAudio_WinForms.dll
   ├── NAudio_WinMM.dll
   ├── RAGENativeUI.dll
   ├── lspdfr/
   │   └── audio/
   │       └── scanner/
   │           └── NarcoBoats/
   │               └── (your WAV files go here)
   └── plugins/
       ├── NarcoBoatsRPH.dll
       └── NarcoBoatsRPH.ini
   ```

3. **Launch GTA V through RagePluginHook**

4. **Press F6** (or your configured hotkey) to start a mission

**That's it!** The folder structure is already set up for you - everything will be in the right place automatically.

## ⚙️ Configuration

The plugin includes a pre-configured `NarcoBoatsRPH.ini` file in the `plugins/` folder.

### Configuration Options

```ini
# Hotkey to start the mission
MissionHotkey=F6

# Number of boats in the first wave (1-15)
StartingBoatCount=5

# Number of additional boats per wave (1-5)
BoatsPerWave=3

# Maximum number of waves (1-10)
MaxWaves=3

# Drug prop settings (packages visible on boats)
# WARNING: Setting above 15 may cause performance issues!
MinDrugProps=6
MaxDrugProps=10

# Audio folder for radio chatter WAV files
AudioFolder=lspdfr\audio\scanner\NarcoBoats\

# Enable/disable radio chatter audio
EnableAudio=true

# Audio volume (0.0 to 1.0)
AudioVolume=0.5

# Helicopter model
# Options: HUNTER, BUZZARD, SAVAGE, ANNIHILATOR
HelicopterModel=HUNTER

# Spawn location
# Options: Beach, Airport, Sandy, Fort, Random
SpawnLocation=Fort
```

### Available Hotkeys
You can use any F-key: `F6`, `F7`, `F8`, `F9`, `F10`, `F11`, `F12`

### Helicopter Options
- **HUNTER** - Attack helicopter with explosive cannon and homing missiles (recommended)
- **BUZZARD** - Light attack helicopter with good maneuverability
- **SAVAGE** - Heavy gunship with devastating firepower
- **ANNIHILATOR** - Military transport helicopter with side-mounted weapons

### Spawn Locations
- **Beach** - Vespucci Beach helipad (scenic ocean view)
- **Airport** - LSIA northeast gate (civilian airport)
- **Sandy** - Sandy Shores Airfield (desert location)
- **Fort** - Fort Zancudo military base (authentic military start)
- **Random** - Randomizes between all locations (variety)

## 🎙️ Adding Radio Chatter (Optional)

To add immersive military radio communications:

### Step 1: Create or Obtain Audio Files

Create WAV audio files with military-style callouts:
- "Target destroyed"
- "Good hit" / "Direct hit"
- "Hostile neutralized"
- "Splash one" / "Splash two"
- "Stay on target"
- "Continue engagement"
- "Confirmed kill"

### Step 2: Place Files in Audio Folder

**The audio folder is already created for you!**

Simply place your WAV files in:
```
Grand Theft Auto V\lspdfr\audio\scanner\NarcoBoats\
```

### Step 3: File Format Requirements
- **Format:** WAV only (MP3/OGG will not work)
- **Quality:** 16-bit PCM recommended
- **Sample Rate:** 44.1kHz or 48kHz
- **Channels:** Mono or Stereo

### Suggested Tools for Creating Audio
- **ElevenLabs** - AI voice generation (free tier available)
- **Audacity** - Free audio editing and conversion
- **Freesound.org** - Free sound effects library
- **Online WAV Converter** - Convert MP3 to WAV if needed

## 🎮 Gameplay Tips

### Combat Tactics
- **Hunter helicopter** has the best weapons for this mission (homing missiles + explosive cannon)
- Use **homing missiles** to lock onto boats from a distance
- Lead your shots when using cannon - boats will try to evade
- **Stay mobile** - don't hover in one spot too long

### Target Acquisition
- Boats are marked with **red blips** on your radar
- Look for **drug packages** stacked on boats for visual identification
- Boats will **drive erratically** trying to evade - anticipate their movements

### Wave Strategy
- **Complete waves quickly** for better ratings
- Mission difficulty **increases each wave** (more boats spawn)
- Track your **kills per minute** for performance rating
- Complete all waves to finish the operation successfully

### Performance Ratings
- **Excellent:** 4+ kills per minute
- **Good:** 3 kills per minute
- **Adequate:** 2 kills per minute
- **Needs Improvement:** Under 1 kills per minute

## 🛠️ Troubleshooting

### Plugin Won't Load
- Ensure RagePluginHook is up to date
- Check `RagePluginHook.log` for errors
- Verify all NAudio DLL files are in your GTA V root directory
- Make sure you're running GTA V through RagePluginHook, not Steam/Rockstar directly

### Boats Not Spawning
- Make sure you're in **single-player mode**
- Try reloading the plugin (F4 → Reload Plugins in RPH)
- Check the `NarcoBoatsRPH.ini` for valid settings
- Ensure `StartingBoatCount` is between 1-15

### Game Crashes During Mission
- **Reduce drug prop count** in config (set `MaxDrugProps` to 8 or lower)
- Lower `StartingBoatCount` to 3-4
- Disable other heavy mods temporarily
- Check RagePluginHook.log for specific errors

### Can't Lock Onto Boats
- Make sure boats have drivers (they should spawn automatically)
- Try reducing drug props - too many can interfere with targeting
- Use explosive cannon if homing missiles fail
- Get closer to targets for better lock-on

### No Radio Chatter Playing
- Verify WAV files are in: `lspdfr\audio\scanner\NarcoBoats\`
- Files **must** be in WAV format (not MP3/OGG)
- Check `EnableAudio=true` in the INI file
- Check RagePluginHook.log for audio errors

### Performance Issues
- Lower `StartingBoatCount` and `BoatsPerWave` values
- Reduce `MaxDrugProps` to 5 or lower
- Limit `MaxWaves` to 2-3
- Close other applications while playing
- Update graphics drivers

## 📊 Statistics & Tracking

The mod tracks your performance across sessions:
- **Missions Completed** - Total operations finished
- **Performance Ratings** - Based on kills per minute

Statistics are displayed at mission completion and persist across game sessions.

## 🎯 What's New in Version 1.0

### Features
- ✅ **Wave-based combat** - Progressive difficulty with multiple waves
- ✅ **Multiple spawn locations** - Beach, Airport, Sandy Shores, Fort Zancudo, or Random
- ✅ **Helicopter selection** - Choose from Hunter, Buzzard, Savage, or Annihilator
- ✅ **Radio chatter support** - Optional military communications
- ✅ **Enhanced HUD** - Color-coded mission stats and real-time tracking
- ✅ **Performance ratings** - Get rated on combat effectiveness
- ✅ **Drug cargo visualization** - Boats loaded with visible contraband
- ✅ **Professional notifications** - Clean, color-coded mission updates
- ✅ **NAudio integration** - Non-blocking audio playback system

## 🤝 Credits

**Author:** TheTechSupportDude (Daniel)  
**Version:** 1.0  
**Release Date:** December 2025  
**Built With:** C#, RagePluginHook, NAudio

**Special Thanks:**
- LSPDFR community
- RagePluginHook developers
- NAudio library developers

## ⚖️ Disclaimer

This is a fictional gameplay mod for entertainment purposes only. It does not represent actual military operations, endorse any real-world activities, or promote illegal behavior. All assets and content are part of the Grand Theft Auto V game environment. This mod is not affiliated with Rockstar Games or Take-Two Interactive.

## 📜 License

This mod is free to use and modify for personal use. Credit to the original author is appreciated if redistributing or creating derivative works.

## 🆘 Support

For bug reports, suggestions, or support:
- Post in the LSPDFR forums
- Check RagePluginHook.log for error details
- Report issues on the mod download page
- Join my Discord https://discord.gg/Q8RNd2xKAH 

---

**Enjoy your operations, Mr. Secretary!** 🚁💥

*Remember: This is a game. Play responsibly and respect others.*
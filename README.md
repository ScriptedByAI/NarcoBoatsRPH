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
- **Flexible Spawn Locations**: Beach, Fort Zancudo, or random deployment
- **Full INI Configuration**: Customize difficulty, boats, waves, and more
- **Adjustable Drug Cargo**: Control visual contraband package density

### Immersion Features
- **AI Radio Chatter**: Optional military communications during combat (WAV files)
- **Drug Cargo Visualization**: Boats loaded with visible contraband packages
- **Enhanced HUD**: Color-coded mission stats with real-time tracking
- **Performance Ratings**: Get rated on your combat effectiveness
- **Career Statistics**: Track missions completed and personal records
- **Mission Briefings**: Detailed operation orders before each deployment

### Quality of Life
- **Smart Audio System**: Uses LSPDFR standard audio path
- **Enhanced Notifications**: Color-coded, professional mission updates
- **Auto-Configuration**: Creates default config on first run
- **Persistent Statistics**: Track your performance across sessions

## 📋 Requirements

- Grand Theft Auto V
- RagePluginHook (latest version)
- LSPDFR (optional but recommended)

## 🔧 Installation

### Installation Steps

1. **Download the latest release** from the releases page

2. **Extract the DLL file:**
   ```
   Grand Theft Auto V/
   └── Plugins/
       └── NarcoBoatsRPH.dll
   ```

3. **The plugin will auto-generate config on first run:**
   ```
   Grand Theft Auto V/
   └── Plugins/
       └── NarcoBoatsRPH.ini
   ```

4. **(Optional) Add radio chatter audio:**
   ```
   Grand Theft Auto V/
   └── lspdfr/
       └── audio/
           └── scanner/
               └── NarcoBoats/
                   ├── contact.wav
                   ├── hit.wav
                   ├── splash.wav
                   └── [your other WAV files]
   ```

5. **Launch GTA V through RagePluginHook**

6. **Press F6** (or your configured hotkey) to start a mission

## ⚙️ Configuration

The plugin automatically creates a `NarcoBoatsRPH.ini` file in the `Plugins/` folder on first run.

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
# Uses standard LSPDFR audio location
AudioFolder=lspdfr\audio\scanner\NarcoBoats\

# Helicopter model
# Options: HUNTER, BUZZARD, SAVAGE, ANNIHILATOR
HelicopterModel=HUNTER

# Spawn location
# Options: Beach, Fort, Random
SpawnLocation=Random
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
- **Fort** - Fort Zancudo military base (authentic military start)
- **Random** - Randomizes between Beach, Fort, and Airport (variety)

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

### Step 2: Place Files in Correct Location

Place WAV files in:
```
C:\Program Files (x86)\Steam\steamapps\common\Grand Theft Auto V\lspdfr\audio\scanner\NarcoBoats\
```

Or if you have a non-Steam installation:
```
[Your GTA V Path]\lspdfr\audio\scanner\NarcoBoats\
```

**Important:** The folder structure must exist. Create any missing folders.

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
- **Excellent:** 10+ kills per minute
- **Good:** 7-10 kills per minute
- **Adequate:** 5-7 kills per minute
- **Needs Improvement:** Under 5 kills per minute

## 🛠️ Troubleshooting

### Plugin Won't Load
- Ensure RagePluginHook is up to date
- Check `RagePluginHook.log` for errors
- Verify `NarcoBoatsRPH.dll` is in the `Plugins/` folder
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

### Boat Blips Are White Instead of Red
- This should be fixed in the latest version
- If still occurring, reload the plugin (F4 → Reload Plugins)
- Check that you're using the latest version of the mod

### No Radio Chatter Playing
- Verify WAV files are in: `lspdfr\audio\scanner\NarcoBoats\`
- Check `AudioFolder` path in `NarcoBoatsRPH.ini`
- Files **must** be in WAV format (not MP3/OGG)
- Check RagePluginHook.log for audio errors
- Make sure the folder exists (create it if missing)

### GTA V Audio Not Working
- This should be fixed in the latest version (audio path updated)
- Restart GTA V and RagePluginHook completely
- Check Windows audio mixer - ensure GTA V isn't muted
- Verify other mods aren't conflicting with audio system

### Performance Issues
- Lower `StartingBoatCount` and `BoatsPerWave` values
- Reduce `MaxDrugProps` to 5 or lower
- Limit `MaxWaves` to 2-3
- Close other applications while playing
- Update graphics drivers

## 📊 Statistics & Tracking

The mod tracks your performance across sessions:
- **Missions Completed** - Total operations finished
- **Best Wave Record** - Highest wave reached
- **Best Time** - Fastest mission completion
- **Performance Ratings** - Based on kills per minute

Statistics are displayed at mission completion and persist across game sessions.

## 🎯 What's New in Latest Version

### Version 1.0.1 Updates
- ✅ **Fixed audio path** - Now uses LSPDFR standard location
- ✅ **Fixed white blips** - Boats now properly show as red enemies
- ✅ **Fixed GTA V audio** - No more audio conflicts
- ✅ **Enhanced HUD** - Color-coded mission stats
- ✅ **Performance ratings** - Get rated on combat effectiveness
- ✅ **Career statistics** - Track missions and records
- ✅ **Improved notifications** - Professional, color-coded updates
- ✅ **Better audio system** - Async playback prevents conflicts

See `ChangeLog.txt` for complete version history.

## 💡 Planned Features (Future Updates)

### Coming Soon
- Additional helicopter models (Valkyrie, Akula)
- Difficulty presets (Easy, Normal, Hard, Insane)
- Mission failure conditions (boats reaching shore)
- Custom blip colors by wave
- Additional mission types

### Under Consideration
- Co-op support (multiplayer compatibility)
- Achievement system
- In-game statistics screen
- Save/load best times
- Medal/rank system based on performance

## 🤝 Credits

**Author:** TheTechSupportDude (Daniel)  
**Version:** 1.0.1  
**Last Updated:** December 2025  
**Built With:** C#, RagePluginHook, ScriptHookVDotNet principles

## ⚖️ Disclaimer

This is a fictional gameplay mod for entertainment purposes only. It does not represent actual military operations, endorse any real-world activities, or promote illegal behavior. All assets and content are part of the Grand Theft Auto V game environment. This mod is not affiliated with Rockstar Games or Take-Two Interactive.

## 📜 License

This mod is free to use and modify for personal use. Credit to the original author is appreciated if redistributing or creating derivative works.

## 🆘 Support

For bug reports, suggestions, or support:
- Post in the LSPDFR forums
- Create an issue on GitHub (if available)
- Check RagePluginHook.log for error details

## 🙏 Acknowledgments

Special thanks to:
- The LSPDFR community
- RagePluginHook developers
- All beta testers and contributors

---

**Enjoy your operations, Mr. Secretary!** 🚁💥

*Remember: This is a game. Play responsibly and respect others.*

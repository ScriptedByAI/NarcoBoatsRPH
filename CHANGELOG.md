# Changelog - Secretary of War Simulator

## Version 2.0 - "The Political Reckoning Update" (December 2025)

### 🎯 Major New Features

#### Escalating Political Consequences System
- **First boat destroyed**: Simple mission success notification
- **Second boat**: Congressional oversight hearing scheduled with warning about classified briefing
- **Third boat**: Trending #1 on X (formerly Twitter) as #WarCriminal, along with #SecretaryOfWar and #ImpeachNow
- **Subsequent boats**: Random escalating consequences including:
  - House impeachment resolutions
  - UN Security Council emergency sessions
  - Special Prosecutor appointments
  - White House "confidence" statements
  - Joint Chiefs concerns about "unorthodox tactics"
- **Mission end summary**: Political status display (Stable/Elevated/Critical) based on your actions
- Fully configurable via `EnablePoliticalConsequences=true/false` in INI

#### Media Spin System
- **Liberal outlets** (MSNOW, CNN, The Washington Compost, NPR, The New York Crimes, MSDNC, Fake News Network):
  - Negative spin on strikes
  - Impeachment questions
  - "Purely political theater" claims
  - Environmental concerns
  - War crimes allegations
  - International community condemnation
  - Constitutional questions
  - Fact-checking challenges
- **Conservative outlets** (Fox News, Newsmax, The Daily Wire, OANN, The Federalist, Breitbart, Real News America):
  - Positive coverage
  - "Protecting American lives" messaging
  - "America First" rhetoric
  - Praising "decisive action"
  - "Strong leadership" commentary
  - Intelligence confirmations
  - Veteran approval ratings
- 30% chance to appear after each boat destruction
- Fully configurable via `EnableMediaSpin=true/false` in INI

#### Bureaucracy Humor System
- Pentagon budget concerns: *"$4 million in missiles on a $50,000 boat"*
- State Department delays: *"6-8 weeks for diplomatic review, submit Form DS-4207 in triplicate"*
- Budget Committee questioning: *"Have you considered... just asking them nicely to stop?"*
- Legal Department requirements: *"47-page Rules of Engagement manual, updated this morning"*
- Pentagon memos about authorization forms
- Comptroller concerns about "excessive ammunition expenditure"
- Venezuelan ambassador complaints
- Inter-service rivalry with the Navy
- HR Department overtime approval requirements
- Environmental Impact Statement delays (18-24 months)
- 40% chance to appear between waves, 50% chance at mission start
- Fully configurable via `EnableBureaucracyHumor=true/false` in INI

#### Progressive Victory Audio System
- **Wave 1 completion**: Plays `trump_victory_short.wav` (quick celebration)
- **Wave 2 completion**: Plays `trump_victory.wav` (standard celebration)
- **Wave 3 / Mission Complete**: Plays `trump_victory_long.wav` (full celebration!)
- Victory audio files are now **automatically excluded** from random radio chatter rotation
- Victory audio has playback priority and won't be interrupted
- Configurable options:
  - `EnableVictoryAudio=true/false` - Turn victory audio on/off
  - `PlayVictoryAudioAfterEachWave=true/false` - Play after each wave or only at mission end
  - `UseProgressiveVictoryAudio=true/false` - Escalating audio or same audio each time
  - `VictoryAudioShort/Normal/Long` - Customize filenames

#### Configurable Notification Duration
- New INI setting: `NotificationDuration` (in milliseconds)
- Default: 8000ms (8 seconds) - up from GTA V's default 5 seconds
- Range: 3000-15000ms (3-15 seconds)
- Recommended settings:
  - 8000ms - Good balance
  - 10000ms - Perfect for savoring the comedy
  - 12000ms - Maximum enjoyment mode
- Applies to all humorous notifications (media spin, political consequences, bureaucracy humor, mission briefings)
- Combat HUD timing unchanged (still updates every 100ms as designed)

### 🔧 Technical Improvements
- Improved audio filtering system to prevent victory audio from playing during combat
- Enhanced notification system with custom duration support using GTA V native functions
- Better audio management with proper cleanup and priority handling
- More robust error handling for notification display

### 📝 Configuration Changes
**⚠️ IMPORTANT FOR v1.0 USERS**: Delete your existing `NarcoBoatsRPH.ini` file to allow the new configuration file with all v2.0 options to be generated automatically.

New INI options added:
```ini
# V2.0 Feature Controls
NotificationDuration=8000
EnableVictoryAudio=true
PlayVictoryAudioAfterEachWave=true
UseProgressiveVictoryAudio=true
VictoryAudioShort=trump_victory_short.wav
VictoryAudioNormal=trump_victory.wav
VictoryAudioLong=trump_victory_long.wav
EnablePoliticalConsequences=true
EnableMediaSpin=true
EnableBureaucracyHumor=true
```

All v1.0 settings remain compatible and unchanged.

### 🎨 Quality of Life Improvements
- More time to read and appreciate humorous notifications
- Victory celebrations that match mission intensity
- Political fallout tracking adds narrative depth
- Satirical media coverage enhances immersion
- Bureaucratic obstacles add comedic realism

---

## Version 1.0 - Initial Release

### Core Features
- Wave-based mission system with configurable difficulty
- Attack helicopter gameplay with various spawn locations
- Drug-laden narco boats as targets
- Radio chatter audio system using NAudio
- Configurable mission parameters (boat counts, waves, spawn locations)
- Performance tracking and statistics
- Multiple helicopter options (Hunter, Buzzard, Savage, Annihilator)
- Dynamic spawn point selection based on player location
- Visual drug props on boats for immersion
- Mission briefings and completion stats

### Audio System
- Support for custom radio chatter WAV files
- Volume control
- Non-blocking audio playback
- Automatic cleanup and disposal

### Configuration
- Hotkey customization
- Boat count and wave customization
- Drug prop density settings
- Audio folder configuration
- Helicopter model selection
- Spawn location presets

---

## Upgrade Notes from v1.0 to v2.0

1. **Delete your old INI file** before running v2.0 for the first time
2. Your existing v1.0 audio files will work perfectly with v2.0
3. Add the three Trump victory audio files to your audio folder:
   - `trump_victory_short.wav`
   - `trump_victory.wav`
   - `trump_victory_long.wav`
4. All v2.0 features can be toggled on/off individually if you prefer v1.0 behavior
5. Default settings provide the full v2.0 experience - customize as desired!

---

## Coming Soon (Potential Future Features)
- Additional media outlets and messages
- More bureaucracy humor variations
- Expanded political consequence chains
- Customizable consequence thresholds
- Additional victory audio options

---

**Developer**: TheTechSupportDude  
**License**: MIT
**GitHub**: [https://github.com/ScriptedByAI/NarcoBoatsRPH.git]

using Rage;
using Rage.Native;
using Rage.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using NAudio.Wave;

[assembly: Rage.Attributes.Plugin("SecretaryOfWar", Description = "Secretary of War Simulator: Operation Southern Spear", Author = "TheTechSupportDude")]

namespace NarcoBoatsRPH
{
    public class EntryPoint
    {
        private static bool missionActive = false;
        private static List<Vehicle> narcoBoats = new List<Vehicle>();
        private static List<Blip> boatBlips = new List<Blip>();
        private static int boatsDestroyed = 0;
        private static int totalBoats = 5;
        private static Vehicle playerHeli = null;
        private static Random random = new Random();

        // Configuration variables
        private static string configPath = "Plugins/NarcoBoatsRPH.ini";
        private static Keys missionHotkey = Keys.F6;
        private static int startingBoatCount = 5;
        private static int boatsPerWave = 3;
        private static int maxWaves = 3;
        private static int minDrugProps = 6;
        private static int maxDrugProps = 10;
        private static string audioFolder = "lspdfr\\audio\\scanner\\NarcoBoats\\";
        private static string helicopterModel = "HUNTER";
        private static string spawnLocation = "Beach";
        private static bool missionStarting = false;
        private static bool enableAudio = true;
        private static float audioVolume = 0.5f;

        // Wave system variables
        private static int currentWave = 1;
        private static int totalKills = 0;
        private static DateTime missionStartTime;
        private static bool waitingForNextWave = false;
        private static GameFiber missionFiber;

        // Enhanced features
        private static int missionsDone = 0;
        private static int bestWaveRecord = 0;
        private static TimeSpan bestTime = TimeSpan.MaxValue;

        // NAudio handling
        private static List<string> audioFiles = new List<string>();
        private static bool audioLoaded = false;
        private static WaveOutEvent outputDevice;

        public static void Main()
        {
            // Load configuration
            LoadConfig();

            // Load audio files list
            LoadAudioFiles();

            Game.LogTrivial("Secretary of War Simulator: Operation Southern Spear loaded!");
            Game.DisplayNotification($"~b~SECWAR SIMULATOR~w~~n~~g~Operation Southern Spear loaded!~w~~n~Press ~y~{missionHotkey}~w~ to start mission");

            // Start the main fiber
            missionFiber = GameFiber.StartNew(MainLoop);

            // Keep plugin alive
            while (true)
            {
                GameFiber.Sleep(1000);
            }
        }

        public static void OnUnload(bool isTerminating)
        {
            // Clean up audio
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }

            Game.LogTrivial("Secretary of War Simulator unloaded");
        }

        private static void LoadAudioFiles()
        {
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), audioFolder);

                if (Directory.Exists(fullPath))
                {
                    audioFiles = Directory.GetFiles(fullPath, "*.wav").ToList();

                    if (audioFiles.Count > 0)
                    {
                        audioLoaded = true;
                        Game.LogTrivial($"Loaded {audioFiles.Count} audio files from {audioFolder}");
                    }
                    else
                    {
                        Game.LogTrivial("No WAV files found in audio folder");
                        audioLoaded = false;
                    }
                }
                else
                {
                    Game.LogTrivial($"Audio folder not found: {fullPath}");
                    audioLoaded = false;
                }
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"Error loading audio files: {ex.Message}");
                audioLoaded = false;
            }
        }

        private static void LoadConfig()
        {
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), configPath);

                if (!File.Exists(fullPath))
                {
                    CreateDefaultConfig(fullPath);
                    Game.LogTrivial("Created default NarcoBoatsRPH.ini");
                    return;
                }

                string[] lines = File.ReadAllLines(fullPath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#") || line.TrimStart().StartsWith(";"))
                        continue;

                    string[] parts = line.Split('=');
                    if (parts.Length != 2) continue;

                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    switch (key)
                    {
                        case "MissionHotkey":
                            if (Enum.TryParse(value, out Keys parsedKey))
                                missionHotkey = parsedKey;
                            break;
                        case "StartingBoatCount":
                            if (int.TryParse(value, out int boats))
                                startingBoatCount = Math.Max(1, Math.Min(boats, 15));
                            break;
                        case "BoatsPerWave":
                            if (int.TryParse(value, out int perWave))
                                boatsPerWave = Math.Max(1, Math.Min(perWave, 5));
                            break;
                        case "MaxWaves":
                            if (int.TryParse(value, out int waves))
                                maxWaves = Math.Max(1, Math.Min(waves, 10));
                            break;
                        case "MinDrugProps":
                            if (int.TryParse(value, out int minProps))
                                minDrugProps = Math.Max(0, Math.Min(minProps, 20));
                            break;
                        case "MaxDrugProps":
                            if (int.TryParse(value, out int maxProps))
                                maxDrugProps = Math.Max(minDrugProps, Math.Min(maxProps, 30));
                            break;
                        case "AudioFolder":
                            audioFolder = value;
                            break;
                        case "HelicopterModel":
                            helicopterModel = value.ToUpper();
                            break;
                        case "SpawnLocation":
                            spawnLocation = value;
                            break;
                        case "EnableAudio":
                            if (bool.TryParse(value, out bool audioEnabled))
                                enableAudio = audioEnabled;
                            break;
                        case "AudioVolume":
                            if (float.TryParse(value, out float volume))
                                audioVolume = Math.Max(0f, Math.Min(volume, 1f));
                            break;
                    }
                }

                Game.LogTrivial($"Config loaded - Boats: {startingBoatCount}, Waves: {maxWaves}, Per Wave: {boatsPerWave}, Audio: {enableAudio}, Spawn: {spawnLocation}");
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"Error loading config: {ex.Message}");
                CreateDefaultConfig(Path.Combine(Directory.GetCurrentDirectory(), configPath));
            }
        }

        private static void CreateDefaultConfig(string path)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                string defaultConfig = @"# Secretary of War Simulator: Operation Southern Spear
# Configuration File

# Hotkey to start the mission (F6, F7, F8, F9, F10, etc.)
MissionHotkey=F6

# Number of boats in the first wave (1-15)
StartingBoatCount=5

# Number of additional boats per wave (1-5)
BoatsPerWave=3

# Maximum number of waves before mission complete (1-10)
MaxWaves=3

# Drug prop settings (packages visible on boats)
# WARNING: Setting these too high (above 15) may cause performance issues or crashes!
MinDrugProps=6
MaxDrugProps=10

# Audio folder for radio chatter WAV files
# Standard LSPDFR audio location
AudioFolder=lspdfr\audio\scanner\NarcoBoats\

# Enable/disable radio chatter audio (true/false)
EnableAudio=true

# Audio volume (0.0 to 1.0, where 1.0 is max volume)
AudioVolume=0.5

# Helicopter model (HUNTER, BUZZARD, SAVAGE, ANNIHILATOR)
HelicopterModel=HUNTER

# Spawn location (Beach, Airport, Sandy, Fort, Random)
# Beach = Vespucci Beach helipad
# Airport = LSIA northeast gate
# Sandy = Sandy Shores Airfield
# Fort = Fort Zancudo military base
# Random = Random location each time
SpawnLocation=Random
";

                File.WriteAllText(path, defaultConfig);
                Game.LogTrivial("Created default configuration file");
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"Error creating config: {ex.Message}");
            }
        }

        private static void MainLoop()
        {
            while (true)
            {
                GameFiber.Yield();

                // Check for hotkey press
                if (Game.IsKeyDown(missionHotkey) && !missionActive && !missionStarting)
                {
                    missionStarting = true;
                    GameFiber.Sleep(500); // Debounce
                    StartMission();
                    missionStarting = false;
                }

                // Mission tick
                if (missionActive)
                {
                    MissionTick();
                }
            }
        }

        private static void MissionTick()
        {
            // Keep wanted level at 0
            Game.LocalPlayer.WantedLevel = 0;
            Game.MaxWantedLevel = 0;

            // Check for destroyed boats
            for (int i = narcoBoats.Count - 1; i >= 0; i--)
            {
                if (!narcoBoats[i] || !narcoBoats[i].IsValid() || narcoBoats[i].IsDead)
                {
                    Vector3 boatPos = narcoBoats[i].Position;

                    // Create explosion
                    World.SpawnExplosion(boatPos, 13, 10f, true, false, 0f);

                    // Add fire
                    NativeFunction.Natives.START_SCRIPT_FIRE(boatPos.X, boatPos.Y, boatPos.Z, 25, true);

                    // Play radio chatter using NAudio (NON-BLOCKING)
                    PlayRandomRadioChatter();

                    // Remove blip
                    if (boatBlips[i] && boatBlips[i].IsValid())
                    {
                        boatBlips[i].Delete();
                    }

                    narcoBoats.RemoveAt(i);
                    boatBlips.RemoveAt(i);
                    boatsDestroyed++;
                    totalKills++;
                }
            }

            // Calculate elapsed time
            TimeSpan elapsed = DateTime.Now - missionStartTime;
            string timeString = $"{(int)elapsed.TotalMinutes:D2}:{elapsed.Seconds:D2}";

            // Enhanced HUD with color coding
            string waveColor = currentWave == maxWaves ? "~r~" : "~y~";
            string killColor = totalKills > (startingBoatCount * currentWave) ? "~g~" : "~w~";

            Game.DisplaySubtitle($"{waveColor}WAVE {currentWave}/{maxWaves}~w~ | Boats: ~o~{boatsDestroyed}~w~/~r~{totalBoats}~w~ | Kills: {killColor}{totalKills}~w~ | Time: ~b~{timeString}", 100);

            // Check if wave complete
            if (boatsDestroyed >= totalBoats && !waitingForNextWave)
            {
                waitingForNextWave = true;
                WaveComplete();
            }

            // Ensure blips stay red
            for (int i = 0; i < boatBlips.Count; i++)
            {
                if (boatBlips[i] && boatBlips[i].IsValid())
                {
                    boatBlips[i].Color = System.Drawing.Color.Red;
                }
            }
        }

        private static void StartMission()
        {
            try
            {
                // Cleanup any existing mission
                if (playerHeli && playerHeli.IsValid())
                {
                    playerHeli.Delete();
                    playerHeli = null;
                }

                missionActive = true;
                boatsDestroyed = 0;
                narcoBoats.Clear();
                boatBlips.Clear();

                Game.LocalPlayer.WantedLevel = 0;
                Game.MaxWantedLevel = 0;

                // Enhanced briefing
                string rank = missionsDone < 5 ? "~w~" : missionsDone < 10 ? "~b~" : "~g~";
                Game.DisplayNotification($"~r~CLASSIFIED OPERATION~w~~n~~b~OPERATION SOUTHERN SPEAR~w~~n~~n~Intelligence confirms narco-terrorist vessels approaching territorial waters.~n~~n~{rank}Mission #{missionsDone + 1}~w~~n~~r~ORDERS: ~w~Eliminate all hostile boats before they reach shore.~n~~n~~g~Weapons free, Mr. Secretary.");

                // Get spawn location
                Vector3 heliSpawn;
                float heliHeading;

                switch (spawnLocation.ToLower())
                {
                    case "beach":
                        // Vespucci Helipad - Large, clear area
                        heliSpawn = new Vector3(-1113.5f, -2883.9f, 13.9f);
                        heliHeading = 150f;
                        break;
                    case "airport":
                        // LSIA near northeast gate - Open helipad
                        heliSpawn = new Vector3(-1145.9f, -2864.1f, 13.9f);
                        heliHeading = 150f;
                        break;
                    case "sandy":
                        // Sandy Shores Airfield - Trevor's helipad
                        heliSpawn = new Vector3(1770.5f, 3239.6f, 42.1f);
                        heliHeading = 120f;
                        break;
                    case "fort":
                        // Fort Zancudo
                        heliSpawn = new Vector3(-2055.83f, 3098.56f, 34.18f);
                        heliHeading = 150f;
                        break;
                    case "random":
                        // Arrays for random spawn locations
                        Vector3[] randomSpawns = new Vector3[]
                        {
                            new Vector3(-1113.5f, -2883.9f, 13.9f),  // Vespucci Beach helipad
                            new Vector3(-1145.9f, -2864.1f, 13.9f),  // LSIA northeast
                            new Vector3(1770.5f, 3239.6f, 42.1f),    // Sandy Shores airfield
                            new Vector3(-2055.83f, 3098.56f, 34.18f) // Fort Zancudo
                        };
                        float[] randomHeadings = new float[]
                        {
                            150f,  // Beach heading
                            150f,  // Airport heading
                            120f,  // Sandy Shores heading
                            150f   // Fort Zancudo heading
                        };

                        // Force truly random selection
                        int randomIndex = new Random(Environment.TickCount).Next(randomSpawns.Length);
                        heliSpawn = randomSpawns[randomIndex];
                        heliHeading = randomHeadings[randomIndex];

                        // Log which location was chosen for debugging
                        string[] locationNames = { "Vespucci Beach", "LSIA", "Sandy Shores", "Fort Zancudo" };
                        Game.LogTrivial($"[SECWAR] Random spawn selected: {locationNames[randomIndex]} (Index: {randomIndex})");
                        break;
                    default: // Beach
                        heliSpawn = new Vector3(-1113.5f, -2883.9f, 13.9f);
                        heliHeading = 150f;
                        break;
                }

                // Teleport player
                Game.LocalPlayer.Character.Position = heliSpawn;

                GameFiber.Sleep(500);

                // Spawn helicopter
                playerHeli = new Vehicle(helicopterModel, heliSpawn, heliHeading);
                playerHeli.IsPersistent = true;
                playerHeli.IsInvincible = false;

                Game.LocalPlayer.Character.WarpIntoVehicle(playerHeli, -1);

                // Initialize mission stats
                currentWave = 1;
                totalKills = 0;
                missionStartTime = DateTime.Now;
                waitingForNextWave = false;

                GameFiber.Sleep(1000);

                // Spawn first wave
                Game.DisplayNotification("~y~Deploying to operational area...~w~~n~Stand by for target acquisition...");
                GameFiber.Sleep(2000);

                SpawnBoatWave(startingBoatCount);
                Game.DisplayNotification($"~r~CONTACT!~w~~n~~o~{totalBoats}~r~ hostile contacts identified.~n~~g~Weapons free! Engage at will!");
            }
            catch (Exception ex)
            {
                Game.DisplayNotification($"~r~Mission Error: {ex.Message}");
                Game.LogTrivial($"Mission Error: {ex}");
            }
        }

        private static void WaveComplete()
        {
            currentWave++;

            if (currentWave <= maxWaves)
            {
                TimeSpan waveTime = DateTime.Now - missionStartTime;
                Game.DisplayNotification($"~g~WAVE {currentWave - 1} COMPLETE~w~~n~All targets neutralized.~n~~n~Total Kills: ~g~{totalKills}~w~~n~Time: ~b~{(int)waveTime.TotalMinutes:D2}:{waveTime.Seconds:D2}~w~~n~~n~~y~Additional hostiles inbound...");

                GameFiber.Sleep(4000);

                int previousTotal = totalBoats;
                SpawnBoatWave(previousTotal + boatsPerWave);

                Game.DisplayNotification($"~r~WAVE {currentWave} INCOMING~w~~n~~o~{totalBoats}~r~ hostile vessels detected!~n~~n~~y~Maintain air superiority!");

                waitingForNextWave = false;
                boatsDestroyed = 0;
            }
            else
            {
                EndMission(true);
            }
        }

        private static void SpawnBoatWave(int numBoats)
        {
            totalBoats = numBoats;
            Game.LogTrivial($"Spawning wave {currentWave} with {totalBoats} boats");

            Vector3[] baseSpawnPoints;

            // Choose spawn points based on player location (Y coordinate determines north vs south)
            if (playerHeli != null && playerHeli.IsValid() && playerHeli.Position.Y > 2000f)
            {
                // Northern spawns (Fort Zancudo, Sandy Shores area) - spawn in waters northwest of Fort Zancudo
                Game.LogTrivial("Using NORTHERN boat spawn points (near Fort Zancudo)");
                baseSpawnPoints = new Vector3[]
                {
                    // Waters northwest of Fort Zancudo
                    new Vector3(-3400f, 2500f, 0.5f),
                    new Vector3(-3500f, 2600f, 0.5f),
                    new Vector3(-3300f, 2450f, 0.5f),
                    new Vector3(-3600f, 2700f, 0.5f),
                    new Vector3(-3250f, 2400f, 0.5f),
                    new Vector3(-3450f, 2550f, 0.5f),
                    new Vector3(-3550f, 2650f, 0.5f),
                    new Vector3(-3350f, 2500f, 0.5f),
                    new Vector3(-3650f, 2750f, 0.5f),
                    new Vector3(-3280f, 2480f, 0.5f),
                    new Vector3(-3480f, 2580f, 0.5f),
                    new Vector3(-3580f, 2680f, 0.5f),
                    new Vector3(-3380f, 2530f, 0.5f),
                    new Vector3(-3520f, 2620f, 0.5f),
                    new Vector3(-3420f, 2520f, 0.5f)
                };
            }
            else
            {
                // Southern spawns (Beach, Airport area)
                Game.LogTrivial("Using SOUTHERN boat spawn points (near Vespucci Beach/LSIA)");
                baseSpawnPoints = new Vector3[]
                {
                    // Further out from shore, better spread
                    new Vector3(-2150f, -1500f, 0.5f),
                    new Vector3(-2300f, -1600f, 0.5f),
                    new Vector3(-2100f, -1400f, 0.5f),
                    new Vector3(-2400f, -1700f, 0.5f),
                    new Vector3(-2050f, -1350f, 0.5f),
                    new Vector3(-2250f, -1550f, 0.5f),
                    new Vector3(-2200f, -1650f, 0.5f),
                    new Vector3(-2350f, -1750f, 0.5f),
                    new Vector3(-2000f, -1300f, 0.5f),
                    new Vector3(-2280f, -1580f, 0.5f),
                    new Vector3(-2450f, -1800f, 0.5f),
                    new Vector3(-2180f, -1480f, 0.5f),
                    new Vector3(-2120f, -1420f, 0.5f),
                    new Vector3(-2380f, -1680f, 0.5f),
                    new Vector3(-2080f, -1380f, 0.5f)
                };
            }

            for (int i = 0; i < numBoats && i < baseSpawnPoints.Length; i++)
            {
                try
                {
                    // Mix of boat types
                    string boatModel = (i % 3 == 0) ? "SEASHARK" : "DINGHY";
                    Vehicle boat = new Vehicle(boatModel, baseSpawnPoints[i]);
                    boat.IsPersistent = true;
                    boat.IsInvincible = false;

                    if (boat && boat.IsValid())
                    {
                        // Create driver
                        Ped driver = boat.CreateRandomDriver();
                        driver.IsPersistent = true;

                        if (driver && driver.IsValid())
                        {
                            driver.RelationshipGroup = "HATES_PLAYER";
                            driver.Tasks.CruiseWithVehicle(25f, VehicleDrivingFlags.Emergency);
                        }

                        // Add drug props
                        AddDrugPropsToBoat(boat);

                        // Create blip - FORCE RED COLOR
                        Blip blip = boat.AttachBlip();
                        blip.Color = System.Drawing.Color.Red;
                        blip.Sprite = BlipSprite.Enemy;
                        blip.Name = $"Hostile Vessel";
                        blip.Scale = 0.8f;

                        // Force alpha to full visibility
                        NativeFunction.Natives.SET_BLIP_ALPHA(blip, 255);

                        narcoBoats.Add(boat);
                        boatBlips.Add(blip);
                    }
                }
                catch (Exception ex)
                {
                    Game.LogTrivial($"Boat spawn error: {ex.Message}");
                }
            }

            Game.LogTrivial($"Successfully spawned {narcoBoats.Count} boats for wave {currentWave}");
        }

        private static void AddDrugPropsToBoat(Vehicle boat)
        {
            try
            {
                // Safety check - make sure boat is still valid
                if (boat == null || !boat.IsValid())
                {
                    Game.LogTrivial("AddDrugPropsToBoat: Boat is null or invalid, skipping props");
                    return;
                }

                int numProps = random.Next(minDrugProps, maxDrugProps + 1);

                Model[] drugModels = new Model[]
                {
                    new Model("bkr_prop_coke_cutblock_01"),
                    new Model("prop_cs_package_01"),
                    new Model("prop_box_guncase_03a")
                };

                for (int i = 0; i < numProps; i++)
                {
                    Model propModel = drugModels[random.Next(drugModels.Length)];

                    float stackHeight = (i % 2) * 0.4f;

                    Vector3 offset = new Vector3(
                        (float)(random.NextDouble() - 0.5) * 0.8f,
                        (float)(random.NextDouble() - 0.7) * 1.2f,
                        0.5f + stackHeight
                    );

                    // Additional safety checks for boat vectors
                    if (boat.ForwardVector == null || boat.RightVector == null || boat.UpVector == null)
                    {
                        Game.LogTrivial($"AddDrugPropsToBoat: Boat vectors are null for prop {i}, skipping remaining props");
                        break;
                    }

                    Vector3 propPos = boat.Position + boat.ForwardVector * offset.Y + boat.RightVector * offset.X + boat.UpVector * offset.Z;

                    Rage.Object drugProp = new Rage.Object(propModel, propPos);

                    if (drugProp && drugProp.IsValid())
                    {
                        NativeFunction.Natives.ATTACH_ENTITY_TO_ENTITY(drugProp, boat, 0, offset.X, offset.Y, offset.Z, 0f, 0f, 0f, false, false, false, false, 2, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"AddDrugPropsToBoat error: {ex.Message}");
            }
        }

        private static void EndMission(bool success)
        {
            missionActive = false;

            if (success)
            {
                TimeSpan finalTime = DateTime.Now - missionStartTime;
                string timeString = $"{(int)finalTime.TotalMinutes:D2}:{finalTime.Seconds:D2}";

                missionsDone++;

                // Track best records
                if (currentWave - 1 > bestWaveRecord)
                {
                    bestWaveRecord = currentWave - 1;
                }
                if (finalTime < bestTime)
                {
                    bestTime = finalTime;
                }

                // Calculate performance rating
                string rating = "~w~";
                double killsPerMinute = totalKills / finalTime.TotalMinutes;
                if (killsPerMinute > 4) rating = "~g~EXCELLENT";
                else if (killsPerMinute > 3) rating = "~b~GOOD";
                else if (killsPerMinute > 2) rating = "~y~ADEQUATE";
                else rating = "~o~NEEDS IMPROVEMENT";

                Game.DisplayNotification($"~g~MISSION COMPLETE~w~~n~All hostile vessels neutralized.~n~~n~~b~MISSION STATS:~w~~n~Waves: ~y~{currentWave - 1}~w~ | Kills: ~g~{totalKills}~w~~n~Time: ~b~{timeString}~w~~n~Rating: {rating}~w~~n~~n~~w~Mission success, Mr. Secretary.~n~~n~~b~Career Stats:~w~~n~Missions: {missionsDone} | Best Waves: {bestWaveRecord}~n~~n~~y~Press {missionHotkey} for another operation");
            }

            // Cleanup
            foreach (var blip in boatBlips)
            {
                if (blip && blip.IsValid()) blip.Delete();
            }

            foreach (var boat in narcoBoats)
            {
                if (boat && boat.IsValid()) boat.Dismiss();
            }

            if (playerHeli && playerHeli.IsValid())
            {
                playerHeli.Dismiss();
            }

            narcoBoats.Clear();
            boatBlips.Clear();
            boatsDestroyed = 0;
            Game.MaxWantedLevel = 5;
        }

        private static void PlayRandomRadioChatter()
        {
            // Don't play if audio disabled or no files loaded
            if (!enableAudio || !audioLoaded || audioFiles.Count == 0)
            {
                return;
            }

            try
            {
                // Get random audio file
                string selectedFile = audioFiles[random.Next(audioFiles.Count)];
                string fileName = Path.GetFileName(selectedFile);

                Game.LogTrivial($"Playing audio: {fileName}");

                // Use NAudio in a background fiber
                GameFiber.StartNew(() =>
                {
                    try
                    {
                        // Stop any currently playing audio
                        if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            outputDevice.Stop();
                        }

                        // Create new audio file reader
                        var audioFile = new AudioFileReader(selectedFile);
                        audioFile.Volume = audioVolume;

                        // Create or reuse output device
                        if (outputDevice == null)
                        {
                            outputDevice = new WaveOutEvent();
                        }

                        // Initialize and play
                        outputDevice.Init(audioFile);
                        outputDevice.Play();

                        // Clean up when done (in background)
                        GameFiber.StartNew(() =>
                        {
                            while (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                                GameFiber.Sleep(100);
                            }

                            // Dispose audio file reader
                            audioFile?.Dispose();
                        });
                    }
                    catch (Exception ex)
                    {
                        Game.LogTrivial($"NAudio playback error: {ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"Audio system error: {ex.Message}");
            }
        }
    }
}
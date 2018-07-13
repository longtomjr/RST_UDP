/// <copyright>
/// The data structure and enums used was taken from the official API header file.
/// All the data here is copyright to their respective owners.
/// </copyright>
namespace Pcars2UDP
{
    using System;

    /// <summary>
    /// Game state of a viewed participant.
    /// </summary>
    public enum GameState
    {
        GAME_EXITED = 0,
        GAME_FRONT_END,
        GAME_INGAME_PLAYING,
        GAME_INGAME_PAUSED,
        GAME_INGAME_INMENU_TIME_TICKING,
        GAME_INGAME_RESTARTING,
        GAME_INGAME_REPLAY,
        GAME_FRONT_END_REPLAY
    }

    /// <summary>
    /// State of a game session.
    /// </summary>
    public enum SessionState
    {
        SESSION_INVALID = 0,
        SESSION_PRACTICE,
        SESSION_TEST,
        SESSION_QUALIFY,
        SESSION_FORMATION_LAP,
        SESSION_RACE,
        SESSION_TIME_ATTACK
    }

    /// <summary>
    /// State of a race in the game session.
    /// </summary>
    public enum RaceState
    {
        RACESTATE_INVALID,
        RACESTATE_NOT_STARTED,
        RACESTATE_RACING,
        RACESTATE_FINISHED,
        RACESTATE_DISQUALIFIED,
        RACESTATE_RETIRED,
        RACESTATE_DNF
    }

    /// <summary>
    /// Flag color of a given flag.
    /// </summary>
    public enum FlagColors
    {
        FLAG_COLOUR_NONE = 0,             // Not used for actual flags, only for some query functions
        FLAG_COLOUR_GREEN,                // End of danger zone, or race started
        FLAG_COLOUR_BLUE,                 // Faster car wants to overtake the participant
        FLAG_COLOUR_WHITE_SLOW_CAR,       // Slow car in area
        FLAG_COLOUR_WHITE_FINAL_LAP,      // Final Lap
        FLAG_COLOUR_RED,                  // Huge collisions where one or more cars become wrecked and block the track
        FLAG_COLOUR_YELLOW,               // Danger on the racing surface itself
        FLAG_COLOUR_DOUBLE_YELLOW,        // Danger that wholly or partly blocks the racing surface
        FLAG_COLOUR_BLACK_AND_WHITE,      // Unsportsmanlike conduct
        FLAG_COLOUR_BLACK_ORANGE_CIRCLE,  // Mechanical Failure
        FLAG_COLOUR_BLACK,                // Participant disqualified
        FLAG_COLOUR_CHEQUERED,            // Chequered flag
    }

    /// <summary>
    /// Reason a flag was given.
    /// </summary>
    public enum FlagReason
    {
        FLAG_REASON_NONE = 0,
        FLAG_REASON_SOLO_CRASH,
        FLAG_REASON_VEHICLE_CRASH,
        FLAG_REASON_VEHICLE_OBSTRUCTION
    }

    /// <summary>
    /// Pit mode of a participant.
    /// </summary>
    public enum PitMode
    {
        PIT_MODE_NONE = 0,
        PIT_MODE_DRIVING_INTO_PITS,
        PIT_MODE_IN_PIT,
        PIT_MODE_DRIVING_OUT_OF_PITS,
        PIT_MODE_IN_GARAGE,
        PIT_MODE_DRIVING_OUT_OF_GARAGE
    }

    /// <summary>
    /// Pit stop schedule of a participant.
    /// </summary>
    public enum PitStopSchedule
    {
        PIT_SCHEDULE_NONE = 0,            // Nothing scheduled
        PIT_SCHEDULE_PLAYER_REQUESTED,    // Used for standard pit sequence - requested by player
        PIT_SCHEDULE_ENGINEER_REQUESTED,  // Used for standard pit sequence - requested by engineer
        PIT_SCHEDULE_DAMAGE_REQUESTED,    // Used for standard pit sequence - requested by engineer for damage
        PIT_SCHEDULE_MANDATORY,           // Used for standard pit sequence - requested by engineer from career enforced lap number
        PIT_SCHEDULE_DRIVE_THROUGH,       // Used for drive-through penalty
        PIT_SCHEDULE_STOP_GO,             // Used for stop-go penalty
        PIT_SCHEDULE_PITSPOT_OCCUPIED,    // Used for drive-through when pitspot is occupied
    }

    /// <summary>
    /// Flags related to a vehicle's properties.
    /// </summary>
    public enum CarFlags
    {
        CAR_HEADLIGHT = (1 << 0),
        CAR_ENGINE_ACTIVE = (1 << 1),
        CAR_ENGINE_WARNING = (1 << 2),
        CAR_SPEED_LIMITER = (1 << 3),
        CAR_ABS = (1 << 4),
        CAR_HANDBRAKE = (1 << 5)
    }

    /// <summary>
    /// Flags related to a tyre's properties.
    /// </summary>
    public enum TyreFlags
    {
        TYRE_ATTACHED = (1 << 0),
        TYRE_INFLATED = (1 << 1),
        TYRE_IS_ON_GROUND = (1 << 2)
    }

    /// <summary>
    /// Terain meterial related to a given track. (TODO: Unsure what it represents).
    /// </summary>
    public enum TerrainMetrials
    {
        TERRAIN_ROAD = 0,
        TERRAIN_LOW_GRIP_ROAD,
        TERRAIN_BUMPY_ROAD1,
        TERRAIN_BUMPY_ROAD2,
        TERRAIN_BUMPY_ROAD3,
        TERRAIN_MARBLES,
        TERRAIN_GRASSY_BERMS,
        TERRAIN_GRASS,
        TERRAIN_GRAVEL,
        TERRAIN_BUMPY_GRAVEL,
        TERRAIN_RUMBLE_STRIPS,
        TERRAIN_DRAINS,
        TERRAIN_TYREWALLS,
        TERRAIN_CEMENTWALLS,
        TERRAIN_GUARDRAILS,
        TERRAIN_SAND,
        TERRAIN_BUMPY_SAND,
        TERRAIN_DIRT,
        TERRAIN_BUMPY_DIRT,
        TERRAIN_DIRT_ROAD,
        TERRAIN_BUMPY_DIRT_ROAD,
        TERRAIN_PAVEMENT,
        TERRAIN_DIRT_BANK,
        TERRAIN_WOOD,
        TERRAIN_DRY_VERGE,
        TERRAIN_EXIT_RUMBLE_STRIPS,
        TERRAIN_GRASSCRETE,
        TERRAIN_LONG_GRASS,
        TERRAIN_SLOPE_GRASS,
        TERRAIN_COBBLES,
        TERRAIN_SAND_ROAD,
        TERRAIN_BAKED_CLAY,
        TERRAIN_ASTROTURF,
        TERRAIN_SNOWHALF,
        TERRAIN_SNOWFULL,
        TERRAIN_DAMAGED_ROAD1,
        TERRAIN_TRAIN_TRACK_ROAD,
        TERRAIN_BUMPYCOBBLES,
        TERRAIN_ARIES_ONLY,
        TERRAIN_ORION_ONLY,
        TERRAIN_B1RUMBLES,
        TERRAIN_B2RUMBLES,
        TERRAIN_ROUGH_SAND_MEDIUM,
        TERRAIN_ROUGH_SAND_HEAVY,
        TERRAIN_SNOWWALLS,
        TERRAIN_ICE_ROAD,
        TERRAIN_RUNOFF_ROAD,
        TERRAIN_ILLEGAL_STRIP,
        TERRAIN_PAINT_CONCRETE,
        TERRAIN_PAINT_CONCRETE_ILLEGAL,
        TERRAIN_RALLY_TARMAC
    }

    /// <summary>
    /// State of a crash.
    /// </summary>
    public enum CrashDamageState
    {
        CRASH_DAMAGE_NONE = 0,
        CRASH_DAMAGE_OFFTRACK,
        CRASH_DAMAGE_LARGE_PROP,
        CRASH_DAMAGE_SPINNING,
        CRASH_DAMAGE_ROLLING
    }
}

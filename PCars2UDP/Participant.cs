namespace PCars2UDP
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a race participant's data.
    /// </summary>
    [Serializable]
    public class Participant
    {
        private byte racePosition;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        private byte[] _vehicleName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        private byte[] _className;
        private byte _highestFlag;
        private byte _highestFlagReason;
        private byte _highestFlagColor;
        private byte _pitModeSchedule;
        private byte _pitMode;

        private byte _pitSchedule;


        /// <summary>
        /// Gets the Position of the participant's car in the world represented by a 3d point.
        /// </summary>
        public Point3D WorldPosition { get; set; } = new Point3D();

        /// <summary>
        /// Gets the quantized heading for the participant (-PI .. +PI).
        /// </summary>
        public short OrientationHeading { get; set; }

        /// <summary>
        /// Gets the quantized pitch for the participant (-PI / 2 .. +PI / 2).
        /// </summary>
        public short OrientationPitch { get; set; }

        /// <summary>
        /// Gets the quantized bank for the participant (-PI .. +PI).
        /// </summary>
        public short OrientationBank { get; set; }

        /// <summary>
        /// Gets the current lap's distance done by the participant.
        /// TODO: Get unit of mesurement.
        /// Units: Meters ?? - what is it really?
        /// Unset: 0 .
        /// </summary>
        public ushort LapDistance { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the participant is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets the position of the participant in the race.
        /// Unset = 0 .
        /// </summary>
        public byte RacePosition
        {
            get
            {
                return racePosition;
            }
            set
            {
                // Gets the top bit to check if the race is active
                IsActive = (value & (1 << 7)) != 0;
                // Clear the top bit and to get race position
                racePosition &= byte.MaxValue ^ (1 << 7);
            }
        }

        /// <summary>
        /// Gets the sector that the participant is in.
        /// </summary>
        public int Sector { get; set; }

        /// <summary>
        /// TODO.
        /// </summary>
        public byte HighestFlag
        {
            get
            {
                return _highestFlag;
            }
            set
            {
                // GameState = (byte)(rawGameState & 15); // Last 4 bits
                // SessionState = (byte)(rawGameState >> 4); 
                _highestFlag = value;
                HighestFlagReason = (byte)(value >> 4); // first 4 bits
                HighestFlagColor = (byte)(value & 15);
            }
        }

        public byte HighestFlagColor
        {
            get
            {
                return _highestFlagColor;
            }
            private set
            {
                _highestFlagColor = value;
            }

        }

        public byte HighestFlagReason
        {
            get
            {
                return _highestFlagReason;
            }
            private set
            {
                _highestFlagReason = value;
            }
        }

        public byte PitModeSchedule
        {
            get
            {
                return _pitModeSchedule;
            }
            set
            {
                _pitModeSchedule = value;
                PitMode = (byte)(value & 15);
                PitSchedule = (byte)(value >> 4);
            }
        }

        public byte PitSchedule
        {
            get { return _pitSchedule; }
            private set { _pitSchedule = value; }
        }


        public byte PitMode
        {
            get { return _pitMode; }
            private set { _pitMode = value; }
        }

        public UInt16 CarIndex { get; set; }
        public byte RaceState { get; set; }
        public byte CurrentLap { get; set; }
        public Single CurrentTime { get; set; }
        public Single CurrentSectorTime { get; set; }

        // Vehicle Info
        public ushort VehicleIndex { get; set; }
        public uint VehicleClass { get; set; }
        public byte[] VehicleName { get => _vehicleName; set => _vehicleName = value; }

        // Class Info
        public uint ClassIndex { get; set; }
        public byte[] ClassName { get => _className; set => _className = value; }

        // Participant Stats Info
        public float FastestLapTime { get; set; }
        public float LastLapTime { get; set; }
        public float LastSectorTime { get; set; }
        public float FastestSector1Time { get; set; }
        public float FastestSector2Time { get; set; }
        public float FastestSector3Time { get; set; }
        public uint OnlineRep { get; set; }
        public ushort MPParticipantIndex { get; set; }
        



    }
}

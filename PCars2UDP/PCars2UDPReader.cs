using PCars2UDP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Text;
using System.Runtime.InteropServices;

namespace PCars2UDP
{
    [Serializable]
    [XmlRoot]
    public class PCars2UDPReader
    {
        [XmlIgnore]
        public PCars2Listener Listener { get => _listener; set => _listener = value; }
        [XmlIgnore]
        private IPEndPoint _groupEP;
        [XmlIgnore]
        private PCars2Listener _listener;

        public static Encoding ENCODING;

        static PCars2UDPReader()
        {
            ENCODING = Encoding.Default;
        }

        public PCars2UDPReader()
        {



            Listener = new PCars2Listener();
            Participants = InitializeArray<Participant>(32);
        }

        public PCars2UDPReader(PCars2Listener pc2listener)
        {
            Listener = pc2listener;
            Participants = InitializeArray<Participant>(32);
        }

        public void ReadPackets()
        {
            byte[] UDPpacket = Listener.Receive(ref _groupEP);
            Stream stream = new MemoryStream(UDPpacket);
            var binaryReader = new BinaryReader(stream);

            ReadBaseUDP(stream, binaryReader);
            if (PacketType == 0)
            {
                ReadTelemetryData(stream, binaryReader);
            }
            else if (PacketType == 3)
            {
                ReadTimings(stream, binaryReader);
            }
            else if (PacketType == 8)
            {
                ReadParticipantVehicles(stream, binaryReader);
            }
            else if (PacketType == 4)
            {
                ReadGameState(stream, binaryReader);
            }
            else if (PacketType == 1)
            {
                ReadRaceData(stream, binaryReader);
            }

        }

        public void ReadRaceData(Stream stream, BinaryReader binaryReader)
        {
            stream.Position = 12;

            WorldFastestLapTime = binaryReader.ReadSingle();
            PersonalFastestLap = binaryReader.ReadSingle();

            PersonalFastestSector1Time = binaryReader.ReadSingle();
            PersonalFastestSector2Time = binaryReader.ReadSingle();
            PersonalFastestSector3Time = binaryReader.ReadSingle();

            WorldFastestSector1Time = binaryReader.ReadSingle();
            WorldFastestSector2Time = binaryReader.ReadSingle();
            WorldFastestSector3Time = binaryReader.ReadSingle();

            TrackLength = binaryReader.ReadSingle();

            TrackLocation = binaryReader.ReadBytes(64);
            TrackVariation = binaryReader.ReadBytes(64);

            TranslatedTrackLocation = binaryReader.ReadBytes(64);
            TranslatedTrackVariation = binaryReader.ReadBytes(64);


            LapsTimeInEvent = binaryReader.ReadUInt16();

            EnforcedPitStopLap = binaryReader.ReadSByte();
        }

        /// <summary>
        /// NOTE: TODO: Taken from cc
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static String getNameFromBytes(byte[] bytes)
        {
            byte zero = (byte)0;
            // if the byte array is empty, or the first char is null, return empty string
            if (bytes == null || bytes.Length == 0 || (bytes.Length > 0 && bytes[0] == zero))
            {
                return "";
            }
            int numBytesToDecode = Array.IndexOf(bytes, zero, 0);

            if (numBytesToDecode == -1)
            {
                // no nulls, we want the whole array
                numBytesToDecode = bytes.Length;
            }
            return ENCODING.GetString(bytes, 0, numBytesToDecode);
        }


        public void ReadBaseUDP(Stream stream, BinaryReader binaryReader)
        {
            stream.Position = 0;
            PacketNumber = binaryReader.ReadUInt32();
            CategoryPacketNumber = binaryReader.ReadUInt32();
            PartialPacketIndex = binaryReader.ReadByte();
            PartialPacketNumber = binaryReader.ReadByte();
            PacketType = binaryReader.ReadByte();
            PacketVersion = binaryReader.ReadByte();
        }

        public void ReadTelemetryData(Stream stream, BinaryReader binaryReader)
        {
            stream.Position = 12;

            ViewedParticipantIndex = binaryReader.ReadSByte();
            UnfilteredThrottle = binaryReader.ReadByte();
            UnfilteredBrake = binaryReader.ReadByte();
            UnfilteredSteering = binaryReader.ReadSByte();
            UnfilteredClutch = binaryReader.ReadByte();
            CarFlags = binaryReader.ReadByte();
            OilTempCelsius = binaryReader.ReadInt16();
            OilPressureKPa = binaryReader.ReadUInt16();
            WaterTempCelsius = binaryReader.ReadInt16();
            WaterPressureKpa = binaryReader.ReadUInt16();
            FuelPressureKpa = binaryReader.ReadUInt16();
            FuelCapacity = binaryReader.ReadByte();
            Brake = binaryReader.ReadByte();
            Throttle = binaryReader.ReadByte();
            Clutch = binaryReader.ReadByte();
            FuelLevel = binaryReader.ReadSingle();
            Speed = binaryReader.ReadSingle();
            Rpm = binaryReader.ReadUInt16();
            MaxRpm = binaryReader.ReadUInt16();
            Steering = binaryReader.ReadSByte();
            GearNumGears = binaryReader.ReadByte();
            BoostAmount = binaryReader.ReadByte();
            CrashState = binaryReader.ReadByte();
            OdometerKM = binaryReader.ReadSingle();

            Orientation[0] = binaryReader.ReadSingle();
            Orientation[1] = binaryReader.ReadSingle();
            Orientation[2] = binaryReader.ReadSingle();

            LocalVelocity[0] = binaryReader.ReadSingle();
            LocalVelocity[1] = binaryReader.ReadSingle();
            LocalVelocity[2] = binaryReader.ReadSingle();

            WorldVelocity[0] = binaryReader.ReadSingle();
            WorldVelocity[1] = binaryReader.ReadSingle();
            WorldVelocity[2] = binaryReader.ReadSingle();

            AngularVelocity[0] = binaryReader.ReadSingle();
            AngularVelocity[1] = binaryReader.ReadSingle();
            AngularVelocity[2] = binaryReader.ReadSingle();

            LocalAcceleration[0] = binaryReader.ReadSingle();
            LocalAcceleration[1] = binaryReader.ReadSingle();
            LocalAcceleration[2] = binaryReader.ReadSingle();

            WorldAcceleration[0] = binaryReader.ReadSingle();
            WorldAcceleration[1] = binaryReader.ReadSingle();
            WorldAcceleration[2] = binaryReader.ReadSingle();

            ExtentsCentre[0] = binaryReader.ReadSingle();
            ExtentsCentre[1] = binaryReader.ReadSingle();
            ExtentsCentre[2] = binaryReader.ReadSingle();

            TyreFlags[0] = binaryReader.ReadByte();
            TyreFlags[1] = binaryReader.ReadByte();
            TyreFlags[2] = binaryReader.ReadByte();
            TyreFlags[3] = binaryReader.ReadByte();

            Terrain[0] = binaryReader.ReadByte();
            Terrain[1] = binaryReader.ReadByte();
            Terrain[2] = binaryReader.ReadByte();
            Terrain[3] = binaryReader.ReadByte();

            TyreY[0] = binaryReader.ReadSingle();
            TyreY[1] = binaryReader.ReadSingle();
            TyreY[2] = binaryReader.ReadSingle();
            TyreY[3] = binaryReader.ReadSingle();

            TyreRPS[0] = binaryReader.ReadSingle();
            TyreRPS[1] = binaryReader.ReadSingle();
            TyreRPS[2] = binaryReader.ReadSingle();
            TyreRPS[3] = binaryReader.ReadSingle();

            TyreTemp[0] = binaryReader.ReadByte();
            TyreTemp[1] = binaryReader.ReadByte();
            TyreTemp[2] = binaryReader.ReadByte();
            TyreTemp[3] = binaryReader.ReadByte();

            TyreHeightAboveGround[0] = binaryReader.ReadSingle();
            TyreHeightAboveGround[1] = binaryReader.ReadSingle();
            TyreHeightAboveGround[2] = binaryReader.ReadSingle();
            TyreHeightAboveGround[3] = binaryReader.ReadSingle();

            TyreWear[0] = binaryReader.ReadByte();
            TyreWear[1] = binaryReader.ReadByte();
            TyreWear[2] = binaryReader.ReadByte();
            TyreWear[3] = binaryReader.ReadByte();

            BrakeDamage[0] = binaryReader.ReadByte();
            BrakeDamage[1] = binaryReader.ReadByte();
            BrakeDamage[2] = binaryReader.ReadByte();
            BrakeDamage[3] = binaryReader.ReadByte();

            SuspensionDamage[0] = binaryReader.ReadByte();
            SuspensionDamage[1] = binaryReader.ReadByte();
            SuspensionDamage[2] = binaryReader.ReadByte();
            SuspensionDamage[3] = binaryReader.ReadByte();

            BrakeTempCelsius[0] = binaryReader.ReadInt16();
            BrakeTempCelsius[1] = binaryReader.ReadInt16();
            BrakeTempCelsius[2] = binaryReader.ReadInt16();
            BrakeTempCelsius[3] = binaryReader.ReadInt16();

            TyreTreadTemp[0] = binaryReader.ReadUInt16();
            TyreTreadTemp[1] = binaryReader.ReadUInt16();
            TyreTreadTemp[2] = binaryReader.ReadUInt16();
            TyreTreadTemp[3] = binaryReader.ReadUInt16();

            TyreLayerTemp[0] = binaryReader.ReadUInt16();
            TyreLayerTemp[1] = binaryReader.ReadUInt16();
            TyreLayerTemp[2] = binaryReader.ReadUInt16();
            TyreLayerTemp[3] = binaryReader.ReadUInt16();

            TyreCarcassTemp[0] = binaryReader.ReadUInt16();
            TyreCarcassTemp[1] = binaryReader.ReadUInt16();
            TyreCarcassTemp[2] = binaryReader.ReadUInt16();
            TyreCarcassTemp[3] = binaryReader.ReadUInt16();

            TyreRimTemp[0] = binaryReader.ReadUInt16();
            TyreRimTemp[1] = binaryReader.ReadUInt16();
            TyreRimTemp[2] = binaryReader.ReadUInt16();
            TyreRimTemp[3] = binaryReader.ReadUInt16();

            TyreInternalAirTemp[0] = binaryReader.ReadUInt16();
            TyreInternalAirTemp[1] = binaryReader.ReadUInt16();
            TyreInternalAirTemp[2] = binaryReader.ReadUInt16();
            TyreInternalAirTemp[3] = binaryReader.ReadUInt16();

            TyreTempLeft[0] = binaryReader.ReadUInt16();
            TyreTempLeft[1] = binaryReader.ReadUInt16();
            TyreTempLeft[2] = binaryReader.ReadUInt16();
            TyreTempLeft[3] = binaryReader.ReadUInt16();

            TyreTempCenter[0] = binaryReader.ReadUInt16();
            TyreTempCenter[1] = binaryReader.ReadUInt16();
            TyreTempCenter[2] = binaryReader.ReadUInt16();
            TyreTempCenter[3] = binaryReader.ReadUInt16();

            TyreTempRight[0] = binaryReader.ReadUInt16();
            TyreTempRight[1] = binaryReader.ReadUInt16();
            TyreTempRight[2] = binaryReader.ReadUInt16();
            TyreTempRight[3] = binaryReader.ReadUInt16();

            WheelLocalPositionY[0] = binaryReader.ReadSingle();
            WheelLocalPositionY[1] = binaryReader.ReadSingle();
            WheelLocalPositionY[2] = binaryReader.ReadSingle();
            WheelLocalPositionY[3] = binaryReader.ReadSingle();

            RideHeight[0] = binaryReader.ReadSingle();
            RideHeight[1] = binaryReader.ReadSingle();
            RideHeight[2] = binaryReader.ReadSingle();
            RideHeight[3] = binaryReader.ReadSingle();

            SuspensionTravel[0] = binaryReader.ReadSingle();
            SuspensionTravel[1] = binaryReader.ReadSingle();
            SuspensionTravel[2] = binaryReader.ReadSingle();
            SuspensionTravel[3] = binaryReader.ReadSingle();

            SuspensionVelocity[0] = binaryReader.ReadSingle();
            SuspensionVelocity[1] = binaryReader.ReadSingle();
            SuspensionVelocity[2] = binaryReader.ReadSingle();
            SuspensionVelocity[3] = binaryReader.ReadSingle();

            SuspensionRideHeight[0] = binaryReader.ReadUInt16();
            SuspensionRideHeight[1] = binaryReader.ReadUInt16();
            SuspensionRideHeight[2] = binaryReader.ReadUInt16();
            SuspensionRideHeight[3] = binaryReader.ReadUInt16();

            AirPressure[0] = binaryReader.ReadUInt16();
            AirPressure[1] = binaryReader.ReadUInt16();
            AirPressure[2] = binaryReader.ReadUInt16();
            AirPressure[3] = binaryReader.ReadUInt16();

            EngineSpeed = binaryReader.ReadSingle();
            EngineTorque = binaryReader.ReadSingle();

            Wings[0] = binaryReader.ReadByte();
            Wings[1] = binaryReader.ReadByte();
        }

        Dictionary<uint, byte[]> Classes = new Dictionary<uint, byte[]>();

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        private byte[] _trackLocation;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        private byte[] _trackVariation;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        private byte[] _translatedTrackLocation;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        private byte[] _translatedTrackVariation;

        private int counter = 0;

        public void ReadParticipantVehicles(Stream stream, BinaryReader binaryReader)
        {

            if (stream.Length == 1164)
            {
                stream.Position = 12;
                for (int i = 0; i < 16; i++)
                {
                    Participants[i + (counter * 16)].VehicleIndex = binaryReader.ReadUInt16();

                    Participants[i + (counter * 16)].VehicleClass = binaryReader.ReadUInt32();
                    Participants[i + (counter * 16)].VehicleName = binaryReader.ReadBytes(64);
                    binaryReader.ReadBytes(2);

                }
                this.counter++;
            }
            else
            {
                stream.Position = 12;
                for (int i = 0; i < 60; i++)
                {
                    uint classIndex = binaryReader.ReadUInt32();
                    byte[] className = binaryReader.ReadBytes(20);
                    if (!Classes.ContainsKey(classIndex))
                    {
                        Classes.Add(classIndex, className);
                    }
                }
                this.counter = 0;
            }



        }

        public void ReadGameState(Stream stream, BinaryReader binaryReader)
        {
            stream.Position = 14;
            byte rawGameState = binaryReader.ReadByte();
            GameState = (byte)(rawGameState & 15); // Last 4 bits
            SessionState = (byte)(rawGameState >> 4); // first 4 bits
            AmbientTemperature = binaryReader.ReadSByte();
            TrackTemperature = binaryReader.ReadSByte();
            RainDensity = binaryReader.ReadByte();
            SnowDensity = binaryReader.ReadByte();
            WindSpeed = binaryReader.ReadSByte();
            WindDirectionX = binaryReader.ReadSByte();
            WindDirectionY = binaryReader.ReadSByte();

            binaryReader.ReadBytes(2); // read the padded bytes
        }

        public void ReadTimings(Stream stream, BinaryReader binaryReader)
        {
            stream.Position = 12;
            NumberParticipants = binaryReader.ReadSByte();
            ParticipantsChangedTimestamp = binaryReader.ReadUInt32();
            EventTimeRemaining = binaryReader.ReadSingle();
            SplitTimeAhead = binaryReader.ReadSingle();
            SplitTimeBehind = binaryReader.ReadSingle();
            SplitTime = binaryReader.ReadSingle();

            foreach (Participant p in Participants)
            {
                p.WorldPosition.X = binaryReader.ReadInt16();  //WorldPosition 
                p.WorldPosition.Y = binaryReader.ReadInt16();  //WorldPosition
                p.WorldPosition.Z = binaryReader.ReadInt16();  //WorldPosition
                p.OrientationHeading = binaryReader.ReadInt16();  //Orientation
                p.OrientationPitch = binaryReader.ReadInt16(); //Orientation 
                p.OrientationBank = binaryReader.ReadInt16(); //Orientation 
                p.LapDistance = binaryReader.ReadUInt16();  //sCurrentLapDistance
                p.RacePosition = (byte)(binaryReader.ReadByte() - 128);  //sRacePosition
                byte Sector_ALL = binaryReader.ReadByte();
                var Sector_Extracted = Sector_ALL & 0x0F;
                p.Sector = Sector_Extracted + 1;   //sSector
                p.HighestFlag = binaryReader.ReadByte();  //sHighestFlag
                p.PitModeSchedule = binaryReader.ReadByte(); //sPitModeSchedule
                p.CarIndex = binaryReader.ReadUInt16();//sCarIndex
                p.RaceState = binaryReader.ReadByte(); //sRaceState
                p.CurrentLap = binaryReader.ReadByte(); //sCurrentLap
                p.CurrentTime = binaryReader.ReadSingle(); //sCurrentTime
                p.CurrentSectorTime = binaryReader.ReadSingle();  //sCurrentSectorTime
                stream.Position = stream.Position + 2;
            }

        }

        static private T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }


        public void close_UDP_Connection()
        {
            Listener.Close();
        }

        public UInt32 PacketNumber { get; set; }

        public UInt32 CategoryPacketNumber { get; set; }

        public byte PartialPacketIndex { get; set; }

        public byte PartialPacketNumber { get; set; }

        public byte PacketType { get; set; }

        public byte PacketVersion { get; set; }

        public sbyte ViewedParticipantIndex { get; set; }

        public byte UnfilteredThrottle { get; set; }

        public byte UnfilteredBrake { get; set; }

        public sbyte UnfilteredSteering { get; set; }

        public byte UnfilteredClutch { get; set; }

        public byte CarFlags { get; set; }

        public Int16 OilTempCelsius { get; set; }

        public UInt16 OilPressureKPa { get; set; }

        public Int16 WaterTempCelsius { get; set; }

        public UInt16 WaterPressureKpa { get; set; }

        public UInt16 FuelPressureKpa { get; set; }

        public byte FuelCapacity { get; set; }

        public byte Brake { get; set; }

        public byte Throttle { get; set; }

        public byte Clutch { get; set; }

        public float FuelLevel { get; set; }

        public float Speed { get; set; }

        public UInt16 Rpm { get; set; }

        public UInt16 MaxRpm { get; set; }

        public sbyte Steering { get; set; }

        public byte GearNumGears { get; set; }
        public byte BoostAmount { get; set; }

        public byte CrashState { get; set; }

        public float OdometerKM { get; set; }

        public float[] Orientation { get; set; } = new float[3];
        public float[] LocalVelocity { get; set; } = new float[3];
        public float[] WorldVelocity { get; set; } = new float[3];
        public float[] AngularVelocity { get; set; } = new float[3];
        public float[] LocalAcceleration { get; set; } = new float[3];
        public float[] WorldAcceleration { get; set; } = new float[3];
        public float[] ExtentsCentre { get; set; } = new float[3];
        public byte[] TyreFlags { get; set; } = new byte[4];
        public byte[] Terrain { get; set; } = new byte[4];
        public float[] TyreY { get; set; } = new float[4];
        public float[] TyreRPS { get; set; } = new float[4];
        public byte[] TyreTemp { get; set; } = new byte[4];
        public float[] TyreHeightAboveGround { get; set; } = new float[4];
        public byte[] TyreWear { get; set; } = new byte[4];
        public byte[] BrakeDamage { get; set; } = new byte[4];
        public byte[] SuspensionDamage { get; set; } = new byte[4];
        public Int16[] BrakeTempCelsius { get; set; } = new Int16[4];
        public UInt16[] TyreTreadTemp { get; set; } = new UInt16[4];
        public UInt16[] TyreLayerTemp { get; set; } = new UInt16[4];
        public UInt16[] TyreCarcassTemp { get; set; } = new UInt16[4];
        public UInt16[] TyreRimTemp { get; set; } = new UInt16[4];
        public UInt16[] TyreInternalAirTemp { get; set; } = new UInt16[4];
        public UInt16[] TyreTempLeft { get; set; } = new UInt16[4];

        public UInt16[] TyreTempCenter { get; set; } = new UInt16[4];
        public UInt16[] TyreTempRight { get; set; } = new UInt16[4];
        public float[] WheelLocalPositionY { get; set; } = new float[4];
        public float[] RideHeight { get; set; } = new float[4];
        public float[] SuspensionTravel { get; set; } = new float[4];
        public float[] SuspensionVelocity { get; set; } = new float[4];
        public UInt16[] SuspensionRideHeight { get; set; } = new UInt16[4];

        public UInt16[] AirPressure { get; set; } = new UInt16[4];

        public float EngineSpeed { get; set; }

        public float EngineTorque { get; set; }

        public byte[] Wings { get; set; } = new byte[2];

        public sbyte NumberParticipants { get; set; }

        public UInt32 ParticipantsChangedTimestamp { get; set; }

        public float EventTimeRemaining { get; set; }

        public float SplitTimeAhead { get; set; }

        public float SplitTimeBehind { get; set; }

        public float SplitTime { get; set; }

        public Participant[] Participants { get; set; } = InitializeArray<Participant>(32);

        public float WorldFastestLapTime { get; set; }

        public float PersonalFastestLap { get; set; }

        public float PersonalFastestSector1Time { get; set; }

        public float PersonalFastestSector2Time { get; set; }

        public float PersonalFastestSector3Time { get; set; }

        public float WorldFastestSector1Time { get; set; }

        public float WorldFastestSector2Time { get; set; }

        public float WorldFastestSector3Time { get; set; }

        public float TrackLength { get; set; }

        public byte[] TrackLocation { get => _trackLocation; set => _trackLocation = value; }

        public byte[] TrackVariation { get => _trackVariation; set => _trackVariation = value; }

        public byte[] TranslatedTrackLocation { get => _translatedTrackLocation; set => _translatedTrackLocation = value; }

        public byte[] TranslatedTrackVariation { get => _translatedTrackVariation; set => _translatedTrackVariation = value; }

        public ushort LapsTimeInEvent { get; set; }

        public SByte EnforcedPitStopLap { get; set; }

        public ushort BuildVersionNumber { get; set; }

        public byte GameState { get; set; }

        public byte SessionState { get; set; }

        public sbyte AmbientTemperature { get; set; }

        public sbyte TrackTemperature { get; set; }

        public byte RainDensity { get; set; }

        public byte SnowDensity { get; set; }

        public sbyte WindSpeed { get; set; }

        public sbyte WindDirectionX { get; set; }

        public sbyte WindDirectionY { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_ExternalSharp
{
    public class Offsets
    {
        public static class client_dll
        {
            // https://github.com/a2x/cs2-dumper/blob/main/output/offsets.cs
            public static ulong dwEntityList = 0x18C1DB8;
            public static ulong dwLocalPlayerController = 0x1911578;
            public static ulong dwLocalPlayerPawn = 0x17361E8;

            // https://github.com/a2x/cs2-dumper/blob/archive/client.dll.cs
            public static ulong m_iHealth = 0x334; // int32_t
            public static ulong m_flFlashDuration = 0x14CC; // float
            public static ulong m_fFlags = 0x3D4; // uint32_t
            public static ulong m_iTeamNum = 0x3CB; // uint8_t
            public static ulong m_hPawn = 0x604; // CHandle<C_BasePlayerPawn>
            public static ulong m_flDetectedByEnemySensorTime = 0x1440; // GameTime_t

            public static ulong spotted = 0x1698 + 0xC; // m_entitySpottedState + m_bSpottedByMask


            // https://github.com/a2x/cs2-dumper/blob/main/output/buttons.cs
            public static ulong jump = 0x172F570;
        }

        public static class engine2_dll
        {
            public static ulong dwBuildNumber = 0x514574;
            public static ulong dwNetworkGameClient_deltaTick = 0x258;
            public static ulong dwNetworkGameClient = 0x513AC8;
            public static ulong dwNetworkGameClient_getLocalPlayer = 0xF0;
            public static ulong dwNetworkGameClient_maxClients = 0x250;
            public static ulong dwNetworkGameClient_signOnState = 0x240;
            public static ulong dwWindowHeight = 0x5CCCDC;
            public static ulong dwWindowWidth = 0x5CCCD8;
        }

        public static class game_info
        {
            public static ulong buildNumber = 0x36B0; // Game build number
        }

        public static class inputsystem_dll // inputsystem.dll
        { 
            public static ulong dwInputSystem = 0x367A0;
        }

        public static class matchmaking_dll // matchmaking.dll
        { 
            public static ulong dwGameTypes = 0x1D21E0;
            public static ulong dwGameTypes_mapName = 0x1D2300;
        }
    }
}

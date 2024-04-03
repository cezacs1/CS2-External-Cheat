using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CG_ExternalSharp.Offsets;
using static CG_ExternalSharp.WinAPI;

namespace CG_ExternalSharp
{
    public class Program
    {
        public static bool IsAdministrator() { return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator); }

        public static void Error()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("cs2 not found!");
            Thread.Sleep(2500);
            Console.ReadKey();
            Environment.Exit(0);
        }

        // base module pointers
        public static ulong client = 0;
        public static ulong engine2 = 0;

        private static void Main()       // Entry point
        {
            Console.Title = "CheatGlobal ExternalSharp";

            if (!IsAdministrator())
            {
                MessageBox.Show("Start cheat as administor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }

            if (Process.GetProcessesByName("cs2").Length != 0)
            {
                var result = Memory.Initalize("cs2");
                if (result == Memory.Enums.InitResult.OK)
                {
                    client = Memory.GetModuleAddress("client.dll");
                    engine2 = Memory.GetModuleAddress("engine2.dll");
                }
                else Error();
            }
            else Error();

            // starting cheat thread
            Run();
        }

        public static void Run()
        {
            while (true)
            {
                ulong localpawn = Memory.Read<ulong>(client + client_dll.dwLocalPlayerPawn);
                int localhealth = Memory.Read<int>(localpawn + client_dll.m_iHealth);
                //Console.WriteLine("localhealth -> " + localhealth);

                // feature no flash
                Memory.Write<float>(localpawn + client_dll.m_flFlashDuration, 0);

                // feature bhop
                if (GetAsyncKeyState((int)Keys.Space) != 0)
                {
                    Memory.Write<int>(client + client_dll.jump, 65537);
                    Thread.Sleep(1);
                    Memory.Write<int>(client + client_dll.jump, 256);
                }

                // entity list
                long entitylistpointer = Memory.Read<long>(client + client_dll.dwEntityList);
                for (int i = 0; i < 64; i++)
                {
                    long listEntry = Memory.Read<long>((ulong)(entitylistpointer + (8 * (i & 0x7FFF) >> 9) + 16));
                    if (listEntry == 0) continue;

                    ulong player = Memory.Read<ulong>((ulong)(listEntry + 120 * (i & 0x1FF)));
                    if (player == 0) continue;

                    uint playerPawn = Memory.Read<uint>(player + client_dll.m_hPawn);
                    if (playerPawn == 0) continue;

                    long listEntry2 = Memory.Read<long>((ulong)(entitylistpointer + 0x8 * ((playerPawn & 0x7FFF) >> 9) + 16));
                    if (listEntry2 == 0) continue;

                    ulong csPlayerPawn = Memory.Read<ulong>((ulong)(listEntry2 + 120 * (playerPawn & 0x1FF)));
                    if (csPlayerPawn == 0) continue;

                    if (csPlayerPawn == localpawn) continue;

                    // feature glow esp
                    Memory.Write<float>(csPlayerPawn + client_dll.m_flDetectedByEnemySensorTime, 999999f);

                    // feature radar
                    Memory.Write<bool>(csPlayerPawn + client_dll.spotted, true);

                    // another features
                }

                //...

                Thread.Sleep(1); // delay
            }
        }
    }
}

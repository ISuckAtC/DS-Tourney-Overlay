using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace DS3_Tournament_Kit
{
    public struct PlayerInfo
    {
        public int HP;
        public IntPtr HPPtr;
        public int MaxHP;
        public IntPtr MaxHPPtr;
        public string Name;
        public string ChrType;
        public bool Connected;
    }
    public partial class Form1 : Form
    {
        #region DllImports
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        [Out] byte[] lpBuffer,
        int dwSize,
        out IntPtr lpNumberOfBytesRead);

        [DllImport("User32.dll", SetLastError = true)]
        static extern short GetKeyState(int nVirtKey);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern long GetWindowLong(
        IntPtr handle,
        int nIndex
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern long SetWindowLong(
        IntPtr handle,
        int nIndex,
        long dwNewLong
        );
        #endregion

        static byte[] ReadMem(IntPtr baseAdd, int size, int caller = 0)
        {
            byte[] buf = new byte[size];
            IntPtr bRead = new IntPtr();
            DS3Handle = Process.GetProcessesByName("DarkSoulsIII")[0].Handle;
            ReadProcessMemory(Process.GetProcessById(Ds3ProcessId).Handle, baseAdd, buf, size, out bRead);
            lastErr = Marshal.GetLastWin32Error();
            if (lastErr != 0)
            {
                Console.WriteLine("ERROR: " + lastErr + " | caller: " + caller);
                if (lastErr == 6 || lastErr == 299)
                {
                    DS3Handle = Process.GetProcessesByName("DarkSoulsIII")[0].Handle;
                    if (!chill)
                    {
                        Console.WriteLine("Entering chill zone");
                        chill = true;
                        Thread chillout = new Thread(() =>
                        {
                            Thread.Sleep(2000);
                            Console.WriteLine("Exiting chill zone");
                            chill = false;
                        });
                        chillout.Start();
                    }

                }
            }
            return buf;
        }
        static IntPtr PointerOffset(IntPtr ptr, long[] offsets)
        {

            foreach (long offset in offsets)
            {
                ptr = new IntPtr(BitConverter.ToInt64(ReadMem(ptr, 8)) + offset);
            }
            return ptr;
        }
        static void SetBases(Process ds3)
        {
            DS3Handle = ds3.Handle;
            Ds3ProcessId = ds3.Id;
            BaseDS3 = ds3.MainModule.BaseAddress;
            BaseA = new IntPtr(BaseDS3.ToInt64() + 0x4740178);
            BaseB = new IntPtr(BaseDS3.ToInt64() + 0x4768E78);
            BaseC = new IntPtr(BaseDS3.ToInt64() + 0x4743AB0);
            BaseD = new IntPtr(BaseDS3.ToInt64() + 0x4743A80);
            BaseE = new IntPtr(BaseDS3.ToInt64() + 0x473FD08);
            BaseF = new IntPtr(BaseDS3.ToInt64() + 0x473AD78);
            BaseZ = new IntPtr(BaseDS3.ToInt64() + 0x4768F98);
            Param = new IntPtr(BaseDS3.ToInt64() + 0x4782838);
            GameFlagData = new IntPtr(BaseDS3.ToInt64() + 0x473BE28);
            LockBonus_ptr = new IntPtr(BaseDS3.ToInt64() + 0x4766CA0);
            DrawNearOnly_ptr = new IntPtr(BaseDS3.ToInt64() + 0x4766555);
            debug_flags = new IntPtr(BaseDS3.ToInt64() + 0x4768F68);
        }
        static byte[] Snap(byte[] b)
        {
            byte[] o = new byte[b.Length / 2];
            for (int i = 0; i < b.Length; i += 2) o[i / 2] = b[i];
            return o;
        }

        #region BasePointers
        static public IntPtr BaseDS3;
        static public IntPtr DS3Handle;
        static public IntPtr BaseA;
        static public IntPtr BaseB;
        static public IntPtr BaseC;
        static public IntPtr BaseD;
        static public IntPtr BaseE;
        static public IntPtr BaseF;
        static public IntPtr BaseZ;
        static public IntPtr Param;
        static public IntPtr GameFlagData;
        static public IntPtr LockBonus_ptr;
        static public IntPtr DrawNearOnly_ptr;
        static public IntPtr debug_flags;
        #endregion

        static void SetPointers(int index)
        {
            if (index < 0)
            {
                PlayerPointers = new IntPtr[5];
                Players = new PlayerInfo[5];
                for (int i = 0; i < 5; ++i)
                {
                    PlayerPointers[i] = PointerOffset(BaseB, new long[] { 0x40, 0x38 * (i + 1) });
                }
            }
            else
            {
                PlayerPointers[index] = PointerOffset(BaseB, new long[] { 0x40, 0x38 * (index + 1) });
            }
        }

        static public int lastErr;
        static public PlayerInfo[] Players;
        static public IntPtr[] PlayerPointers;

        static Graphics graphics;
        static bool clearGraphics;

        static int Ds3ProcessId;

        System.Windows.Forms.Timer DrawTimer;

        static bool chill;

        public PictureBox HealthBar1, HealthBar2;
        public PictureBox Health1, Health2;
        public TextBox HealthText1, HealthText2;
        public TextBox PlayerText1, PlayerText2;

        public int Player1Index, Player2Index;

        public Form1()
        {
            //Console.WriteLine("DisplayForm PID: " + System.Diagnostics.Process.GetCurrentProcess().Id);
            Program.DisplayForm = this;
            this.Name = "DisplayForm";
            InitializeComponent();

            Process ds3 = Process.GetProcessesByName("DarkSoulsIII")[0];
            SetBases(ds3);

            SetPointers(-1);

            graphics = CreateGraphics();

            Player1Index = -1;
            Player2Index = -1;

            Thread playerData = new Thread(() => WatchPlayerData());

            playerData.Start();


            DrawTimer = new System.Windows.Forms.Timer();
            DrawTimer.Interval = 16;
            DrawTimer.Tick += Draw;
            DrawTimer.Start();
            components.Add(DrawTimer);

            Reload();

            Thread.Sleep(5000);

            long style = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, style | 0x80000 | 0x20);

            this.TopMost = true;

            FormClosing += delegate (object sender, FormClosingEventArgs e)
            {
                Console.WriteLine("Form closing");
                Environment.Exit(0);
            };
        }

        public void Reload()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Size size = this.ClientSize;
                HealthBar1.Location = new Point(Program.Config.HP1x, Program.Config.HP1y);
                HealthBar1.Size = new Size(Program.Config.HP1sx, Program.Config.HP1sy);

                HealthBar2.Location = new Point(size.Width - Program.Config.HP2x - Program.Config.HP2sx, Program.Config.HP2y);
                HealthBar2.Size = new Size(Program.Config.HP2sx, Program.Config.HP2sy);

                Health1.Location = new Point(Program.Config.HP1ix, Program.Config.HP1iy);
                Health1.Size = new Size(Program.Config.HP1isx, Program.Config.HP1isy);

                Health2.Location = new Point(size.Width - Program.Config.HP2ix - Program.Config.HP2isx, Program.Config.HP2iy);
                Health2.Size = new Size(Program.Config.HP2isx, Program.Config.HP2isy);

                HealthText1.Location = new Point(Program.Config.P1hx, Program.Config.P1hy);
                HealthText1.Size = new Size(Program.Config.P1hsx, Program.Config.P1hsy);

                HealthText2.Location = new Point(size.Width - Program.Config.P2hx - Program.Config.P2hsx, Program.Config.P2hy);
                HealthText2.Size = new Size(Program.Config.P2hsx, Program.Config.P2hsy);

                PlayerText1.Location = new Point(Program.Config.P1x, Program.Config.P1y);
                PlayerText1.Size = new Size(Program.Config.P1sx, Program.Config.P1sy);

                PlayerText2.Location = new Point(size.Width - Program.Config.P2x - Program.Config.P2sx, Program.Config.P2y);
                PlayerText2.Size = new Size(Program.Config.P2sx, Program.Config.P2sy);
            }));
        }

        public bool CheckConnection(int index, bool player2)
        {
            SetPointers(index);
            if (BitConverter.ToInt64(ReadMem(PlayerPointers[index], 8, 2)) != 0)
            {
                byte[] nameBytes = ReadMem(PointerOffset(PlayerPointers[index], new long[] { 0x1FA0, 0x88 }), 32);
                string name = Encoding.Unicode.GetString(nameBytes).Split('\0')[0];
                Players[index].Name = name;
                Players[index].HPPtr = PointerOffset(PlayerPointers[index], new long[] { 0x1FA0, 0x18 });
                Players[index].MaxHPPtr = PointerOffset(PlayerPointers[index], new long[] { 0x1FA0, 0x1C });
                Console.WriteLine("\nPlayer [" + Players[index].Name + "] connected!");
                Players[index].Connected = true;

                this.Invoke(new MethodInvoker(delegate ()
                {
                    if (player2)
                    {
                        PlayerText2.Text = name;
                        Player2Index = index;
                    }
                    else
                    {
                        PlayerText1.Text = name;
                        Player1Index = index;
                    }
                }));




                return true;
            }
            if (player2)
            {
                Player2Index = -1;
            }
            else
            {
                Player1Index = -1;
            }
            return false;
        }

        public void Draw(object sender, EventArgs e)
        {
            try
            {
                if (chill) return;

                if (Player1Index >= 0)
                {
                    int health = (int)(((float)Players[Player1Index].HP / Players[Player1Index].MaxHP) * 584);
                    Health1.Size = new Size(health, Health1.Size.Height);
                    HealthText1.Text = Players[Player1Index].HP + " / " + Players[Player1Index].MaxHP;
                }
                if (Player2Index >= 0)
                {
                    int health = (int)(((float)Players[Player2Index].HP / Players[Player2Index].MaxHP) * 584);
                    Health2.Size = new Size(health, Health2.Size.Height);
                    HealthText2.Text = Players[Player2Index].HP + " / " + Players[Player2Index].MaxHP;
                }
            }

            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }
        }

        public void WatchPlayerData()
        {
            try
            {
                while (true)
                {
                    if (chill) continue;
                    if (Player1Index >= 0)
                    {
                        Players[Player1Index].HP = BitConverter.ToInt32(ReadMem(Players[Player1Index].HPPtr, 4, 10 + Player1Index), 0);
                        Players[Player1Index].MaxHP = BitConverter.ToInt32(ReadMem(Players[Player1Index].MaxHPPtr, 4, 10 + Player1Index), 0);
                    }
                    if (Player2Index >= 0)
                    {
                        Players[Player2Index].HP = BitConverter.ToInt32(ReadMem(Players[Player2Index].HPPtr, 4, 20 + Player2Index), 0);
                        Players[Player2Index].MaxHP = BitConverter.ToInt32(ReadMem(Players[Player2Index].MaxHPPtr, 4, 20 + Player2Index), 0);
                    }

                    Thread.Sleep(16);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message + "\n" + e.StackTrace); }
        }

    }
}

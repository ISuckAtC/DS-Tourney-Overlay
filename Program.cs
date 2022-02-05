using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DS3_Tournament_Kit
{
    public struct Config
    {
        public int HP1x, HP1y;
        public int HP1sx, HP1sy;
        public int HP1ix, HP1iy;
        public int HP1isx, HP1isy;
        public int HP2x, HP2y;
        public int HP2sx, HP2sy;
        public int HP2ix, HP2iy;
        public int HP2isx, HP2isy;
        public int P2x, P2y;
        public int P2sx, P2sy;
        public int P2fs;
        public int P1x, P1y;
        public int P1sx, P1sy;
        public int P1fs;

        public int P2hx, P2hy;
        public int P2hsx, P2hsy;
        public int P2hfs;
        public int P1hx, P1hy;
        public int P1hsx, P1hsy;
        public int P1hfs;

        public int P1hcx, P1hcy;
        public int P1hcsx, P1hcsy;
        public int P1hcfs;

        public int P2hcx, P2hcy;
        public int P2hcsx, P2hcsy;
        public int P2hcfs;

        public int Timeout;

        public string HP1Path;
        public string HP2Path;
        public string HPiPath;
        public string HPcPath;

        public string FontPath;
        public string TransparencyKey;

        public Config(string[] config)
        {
            string[] HP1 = config[0].Split(",");
            HP1x = int.Parse(HP1[0]);
            HP1y = int.Parse(HP1[1]);

            string[] HP1s = config[1].Split(",");
            HP1sx = int.Parse(HP1s[0]);
            HP1sy = int.Parse(HP1s[1]);

            string[] HP1i = config[2].Split(",");
            HP1ix = int.Parse(HP1i[0]);
            HP1iy = int.Parse(HP1i[1]);

            string[] HP1is = config[3].Split(",");
            HP1isx = int.Parse(HP1is[0]);
            HP1isy = int.Parse(HP1is[1]);

            string[] HP2 = config[4].Split(",");
            HP2x = int.Parse(HP2[0]);
            HP2y = int.Parse(HP2[1]);

            string[] HP2s = config[5].Split(",");
            HP2sx = int.Parse(HP2s[0]);
            HP2sy = int.Parse(HP2s[1]);

            string[] HP2i = config[6].Split(",");
            HP2ix = int.Parse(HP2i[0]);
            HP2iy = int.Parse(HP2i[1]);

            string[] HP2is = config[7].Split(",");
            HP2isx = int.Parse(HP2is[0]);
            HP2isy = int.Parse(HP2is[1]);

            string[] P1 = config[8].Split(",");
            P1x = int.Parse(P1[0]);
            P1y = int.Parse(P1[1]);

            string[] P1s = config[9].Split(",");
            P1sx = int.Parse(P1s[0]);
            P1sy = int.Parse(P1s[1]);

            P1fs = int.Parse(config[10].Split(",")[0]);

            string[] P2 = config[11].Split(",");
            P2x = int.Parse(P2[0]);
            P2y = int.Parse(P2[1]);

            string[] P2s = config[12].Split(",");
            P2sx = int.Parse(P2s[0]);
            P2sy = int.Parse(P2s[1]);

            P2fs = int.Parse(config[13].Split(",")[0]);

            string[] P1h = config[14].Split(",");
            P1hx = int.Parse(P1h[0]);
            P1hy = int.Parse(P1h[1]);

            string[] P1hs = config[15].Split(",");
            P1hsx = int.Parse(P1hs[0]);
            P1hsy = int.Parse(P1hs[1]);

            P1hfs = int.Parse(config[16].Split(",")[0]);

            string[] P2h = config[17].Split(",");
            P2hx = int.Parse(P2h[0]);
            P2hy = int.Parse(P2h[1]);

            string[] P2hs = config[18].Split(",");
            P2hsx = int.Parse(P2hs[0]);
            P2hsy = int.Parse(P2hs[1]);

            P2hfs = int.Parse(config[19].Split(",")[0]);

            string[] P1hc = config[20].Split(",");
            P1hcx = int.Parse(P1hc[0]);
            P1hcy = int.Parse(P1hc[1]);

            string[] P1hcs = config[21].Split(",");
            P1hcsx = int.Parse(P1hcs[0]);
            P1hcsy = int.Parse(P1hcs[1]);

            P1hcfs = int.Parse(config[22].Split(",")[0]);

            string[] P2hc = config[23].Split(",");
            P2hcx = int.Parse(P2hc[0]);
            P2hcy = int.Parse(P2hc[1]);

            string[] P2hcs = config[24].Split(",");
            P2hcsx = int.Parse(P2hcs[0]);
            P2hcsy = int.Parse(P2hcs[1]);

            P2hcfs = int.Parse(config[25].Split(",")[0]);

            Timeout = int.Parse(config[26].Split(",")[0]);

            HP1Path = config[27].Split(",")[0];
            HP2Path = config[28].Split(",")[0];
            HPiPath = config[29].Split(",")[0];
            HPcPath = config[30].Split(",")[0];
            FontPath = config[31].Split(",")[0];
            TransparencyKey = config[32].Split(",")[0];
        }
    }
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        public static Form1 DisplayForm;
        public static ControlForm ControlForm;
        public static Config Config;
        public static Font P1HCFont;
        public static Font P2HCFont;
        public static Font P1HFont;
        public static Font P2HFont;
        public static Font P1Font;
        public static Font P2Font;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AttachConsole(-1);
            LoadConfig();
            Console.WriteLine("PID: " + System.Diagnostics.Process.GetCurrentProcess().Id);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
            pfc.AddFontFile(Config.FontPath);
            P1HCFont = new Font(pfc.Families[0], Config.P1hcfs);
            P2HCFont = new Font(pfc.Families[0], Config.P2hcfs);
            P1HFont = new Font(pfc.Families[0], Config.P1hfs);
            P2HFont = new Font(pfc.Families[0], Config.P2hfs);
            P1Font = new Font(pfc.Families[0], Config.P1fs);
            P2Font = new Font(pfc.Families[0], Config.P2fs);

            (new System.Threading.Thread(() =>
            {
                try
                {
                    Application.Run(new Form1());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n\n" + e.StackTrace);
                }
            })).Start();
            Application.Run(new ControlForm());


            Console.WriteLine("GOODBYE");
        }

        static public void ReloadConfig()
        {
            LoadConfig();
            DisplayForm.Reload();
        }

        static void LoadConfig()
        {
            Config = new Config(System.IO.File.ReadAllLines("config.txt"));
        }
    }
}

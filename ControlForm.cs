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
    public partial class ControlForm : Form
    {
        static Graphics graphics;
        static Font drawFont;
        static Font HPBarFont;
        static Brush drawBrush;
        static Point DrawPoint;
        static bool clearGraphics;

        public TextBox Player1Text;
        public TextBox Player2Text;

        public ComboBox Player1Select;
        public ComboBox Player2Select;
        public Button Player1SelectRefresh;
        public Button Player2SelectRefresh;

        public Button ReloadConfig;

        public Button ToggleLock;

        public Form1 DisplayForm;

        private bool displayLocked = true;

        public ControlForm()
        {
            //Console.WriteLine("ControlForm PID: " + System.Diagnostics.Process.GetCurrentProcess().Id);
            InitializeComponent();
            DisplayForm = Program.DisplayForm;
            Program.ControlForm = this;

            graphics = CreateGraphics();
            drawFont = new Font("Arial", 15);
            drawBrush = new SolidBrush(Color.White);

            FormClosing += delegate (object sender, FormClosingEventArgs e)
            {
                Console.WriteLine("Form closing");
                Environment.Exit(0);
            };
        }

        public void ToggleLock_Click(object sender, EventArgs e)
        {
            displayLocked = !displayLocked;
            if (displayLocked)
            {
                ToggleLock.Text = "Unlock Display";
            }
            else
            {
                ToggleLock.Text = "Lock Display";
            }
            DisplayForm.SetStyle(displayLocked);
        }

        public void ReloadConfig_Click(object sender, EventArgs e)
        {
            Program.ReloadConfig();
        }


        public void Player1Selection(object sender, EventArgs e)
        {
            if (Player1Select.SelectedIndex == 0)
            {
                Console.WriteLine("Selecting nothing");
                DisplayForm.Player1Index = -1;
            }
            else
            {
                if (DisplayForm.CheckConnection(Player1Select.SelectedIndex - 1, false))
                {
                    Console.WriteLine("Connected to player: " + Player1Select.SelectedIndex + " succeeded");
                }
                else Console.WriteLine("Connected to player: " + Player1Select.SelectedIndex + " failed");
            }
        }

        public void Player2Selection(object sender, EventArgs e)
        {
            if (Player2Select.SelectedIndex == 0)
            {
                Console.WriteLine("Selecting nothing");
                DisplayForm.Player2Index = -1;
            }
            else
            {
                if (DisplayForm.CheckConnection(Player2Select.SelectedIndex - 1, true))
                {
                    Console.WriteLine("Connected to player: " + Player2Select.SelectedIndex + " succeeded");
                }
                else Console.WriteLine("Connected to player: " + Player2Select.SelectedIndex + " failed");
            }
        }
    }
}
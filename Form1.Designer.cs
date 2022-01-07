using System.Drawing;
using System.Windows.Forms;

namespace DS3_Tournament_Kit
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "DS3 Tournament Display";
            //this.TopMost = true;
            this.TransparencyKey = Color.Blue;
            this.AllowTransparency = true;
            this.BackColor = Color.Blue;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            //this.ClientSize = new System.Drawing.Size(1920, 1080);

            this.HealthBar1 = new PictureBox();
            this.HealthBar1.Name = "HealthBar1";
            this.HealthBar1.Image = Image.FromFile(Program.Config.HPPath);
            this.HealthBar1.Location = new Point(Program.Config.HP1x, Program.Config.HP1y);
            this.HealthBar1.Size = new Size(Program.Config.HP1sx, Program.Config.HP1sy);
            this.HealthBar1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.HealthBar1.BackColor = Color.Transparent;

            this.Health1 = new PictureBox();
            this.Health1.Name = "Health1";
            this.Health1.Image = Image.FromFile(Program.Config.HPiPath);
            this.Health1.Location = new Point(Program.Config.HP1ix, Program.Config.HP1iy);
            this.Health1.Size = new Size(Program.Config.HP1isx, Program.Config.HP1isy);
            this.Health1.SizeMode = PictureBoxSizeMode.StretchImage;

            this.HealthBar2 = new PictureBox();
            this.HealthBar2.Name = "HealthBar2";
            this.HealthBar2.Image = Image.FromFile(Program.Config.HPPath);
            this.HealthBar2.Location = new Point(Program.Config.HP2x, Program.Config.HP2y);
            this.HealthBar2.Size = new Size(Program.Config.HP2sx, Program.Config.HP2sy);
            this.HealthBar2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.HealthBar2.BackColor = Color.Transparent;

            this.Health2 = new PictureBox();
            this.Health2.Name = "Health2";
            this.Health2.Image = Image.FromFile(Program.Config.HPiPath);
            this.Health2.Location = new Point(Program.Config.HP2ix, Program.Config.HP2iy);
            this.Health2.Size = new Size(Program.Config.HP2isx, Program.Config.HP2isy);
            this.Health2.SizeMode = PictureBoxSizeMode.StretchImage;

            this.HealthText1 = new TextBox();
            this.HealthText1.Name = "HealthText1";
            this.HealthText1.Location = new Point(50, 160);
            this.HealthText1.Size = new Size(200, 200);
            this.HealthText1.Text = "PLAYER1HEALTH";
            this.HealthText1.Font = new Font("Arial", 15);
            this.HealthText1.TextAlign = HorizontalAlignment.Center;
            this.HealthText1.BackColor = Color.Black;
            this.HealthText1.ForeColor = Color.White;
            this.HealthText1.ReadOnly = true;

            this.HealthText2 = new TextBox();
            this.HealthText2.Name = "HealthText2";
            this.HealthText2.Location = new Point(50, 160);
            this.HealthText2.Size = new Size(200, 200);
            this.HealthText2.Text = "PLAYER2HEALTH";
            this.HealthText2.Font = new Font("Arial", 15);
            this.HealthText2.TextAlign = HorizontalAlignment.Center;
            this.HealthText2.BackColor = Color.Black;
            this.HealthText2.ForeColor = Color.White;
            this.HealthText2.ReadOnly = true;

            this.PlayerText1 = new TextBox();
            this.PlayerText1.Name = "PlayerText1";
            this.PlayerText1.Location = new Point(Program.Config.P1x, Program.Config.P1y);
            this.PlayerText1.Size = new Size(Program.Config.P1sx, Program.Config.P1sy);
            this.PlayerText1.Text = "PLAYER 1";
            this.PlayerText1.Font = new Font("Arial", Program.Config.P1fs);
            this.PlayerText1.TextAlign = HorizontalAlignment.Center;
            this.PlayerText1.BackColor = Color.Black;
            this.PlayerText1.ForeColor = Color.White;
            this.PlayerText1.ReadOnly = true;

            this.PlayerText2 = new TextBox();
            this.PlayerText2.Name = "PlayerText2";
            this.PlayerText2.Location = new Point(Program.Config.P2x, Program.Config.P2y);
            this.PlayerText2.Size = new Size(Program.Config.P2sx, Program.Config.P2sy);
            this.PlayerText2.Text = "PLAYER 2";
            this.PlayerText2.Font = new Font("Arial", Program.Config.P2fs);
            this.PlayerText2.TextAlign = HorizontalAlignment.Center;
            this.PlayerText2.BackColor = Color.Black;
            this.PlayerText2.ForeColor = Color.White;
            this.PlayerText2.ReadOnly = true;

            this.Controls.Add(this.HealthBar1);
            this.Controls.Add(this.HealthBar2);
            this.Controls.Add(this.Health1);
            this.Controls.Add(this.Health2);
            this.Controls.Add(this.HealthText1);
            this.Controls.Add(this.HealthText2);
            this.Controls.Add(this.PlayerText1);
            this.Controls.Add(this.PlayerText2);


            this.Health1.BringToFront();
            this.Health2.BringToFront();
        }

        #endregion
    }
}


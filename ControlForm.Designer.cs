using System.Drawing;
using System.Windows.Forms;

namespace DS3_Tournament_Kit
{
    partial class ControlForm
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
            this.Text = "DS3 Tournament Control";
            //this.TopMost = true;
            this.TransparencyKey = Color.Blue;
            this.AllowTransparency = true;
            //this.BackColor = Color.Blue;
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;

            this.ClientSize = new System.Drawing.Size(600, 400);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.Player1Text = new TextBox();
            this.Player1Text.Location = new Point(60, 50);
            this.Player1Text.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.Player1Text.Size = new Size(160, 100);
            this.Player1Text.Text = "LEFT PLAYER";
            this.Player1Text.Font = new Font("Arial", 15);
            this.Player1Text.TextAlign = HorizontalAlignment.Center;
            this.Player1Text.BackColor = Color.Black;
            this.Player1Text.ForeColor = Color.White;
            this.Player1Text.ReadOnly = true;

            this.Player1Select = new ComboBox();
            this.Player1Select.Items.AddRange(
                new object[] {"None", 1, 2, 3, 4, 5, 6}
            );
            this.Player1Select.Location = new Point(100, 100);
            this.Player1Select.Size = new Size(80, 400);
            this.Player1Select.DropDownWidth = 80;
            this.Player1Select.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Player1Select.SelectedIndex = 0;
            this.Player1Select.SelectedIndexChanged += new System.EventHandler(Player1Selection);




            this.Player2Text = new TextBox();
            this.Player2Text.Location = new Point(600 - 220, 50);
            this.Player2Text.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.Player2Text.Size = new Size(160, 100);
            this.Player2Text.Text = "RIGHT PLAYER";
            this.Player2Text.Font = new Font("Arial", 15);
            this.Player2Text.TextAlign = HorizontalAlignment.Center;
            this.Player2Text.BackColor = Color.Black;
            this.Player2Text.ForeColor = Color.White;
            this.Player2Text.ReadOnly = true;

            this.Player2Select = new ComboBox();
            this.Player2Select.Items.AddRange(
                new object[] {"None", 1, 2, 3, 4, 5, 6}
            );
            this.Player2Select.Location = new Point(600 - 180, 100);
            this.Player2Select.Size = new Size(80, 400);
            this.Player2Select.DropDownWidth = 80;
            this.Player2Select.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Player2Select.SelectedIndex = 0;
            this.Player2Select.SelectedIndexChanged += new System.EventHandler(Player2Selection);

            this.ReloadConfig = new Button();
            this.ReloadConfig.Location = new Point(250, 300);
            this.ReloadConfig.Size = new Size(100, 50);
            this.ReloadConfig.Text = "Reload Config";
            this.ReloadConfig.Click += new System.EventHandler(ReloadConfig_Click);

            

            this.Controls.Add(this.Player1Select);
            this.Controls.Add(this.Player1Text);
            this.Controls.Add(this.Player2Select);
            this.Controls.Add(this.Player2Text);
            this.Controls.Add(this.ReloadConfig);
        }

        #endregion
    }
}


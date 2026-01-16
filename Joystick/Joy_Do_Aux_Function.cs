using com.drew.metadata;
using MissionPlanner.Controls;
using System;
using System.Windows.Forms;
using static alglib;
using static MissionPlanner.Utilities.DFLog;

namespace MissionPlanner.Joystick
{
    public partial class Joy_Do_Aux_Function : Form
    {
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox comboBox1;
        private MissionPlanner.Controls.MyButton BUT_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        public Joy_Do_Aux_Function(string buttonname)
        {
            InitializeComponent();

            Utilities.ThemeManager.ApplyThemeTo(this);
            this.Tag = buttonname;

            if (MainV2.joystick != null && MainV2.joystick.enabled)
            {
                var config = MainV2.joystick.getButton(int.Parse(buttonname));
                this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
                this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);

                // AUX function number
                numericUpDown1.Value = (decimal)config.p1;

                // Switch position
                if (config.p2 >= 0 && config.p2 <= 2)
                    comboBox1.SelectedIndex = (int)config.p2;
            }
        }

        // NumericUpDown değiştiğinde otomatik kaydet
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int buttonIndex = int.Parse(this.Tag.ToString());
            var config = MainV2.joystick.getButton(buttonIndex);

            config.function = buttonfunction.Do_Aux_Function;
            config.p1 = (float)numericUpDown1.Value;

            MainV2.joystick.setButton(buttonIndex, config);
        }

        // ComboBox değiştiğinde otomatik kaydet
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int buttonIndex = int.Parse(this.Tag.ToString());
            var config = MainV2.joystick.getButton(buttonIndex);

            config.function = buttonfunction.Do_Aux_Function;
            config.p2 = (float)comboBox1.SelectedIndex;

            MainV2.joystick.setButton(buttonIndex, config);
        }

        private void InitializeComponent()
        {
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.BUT_save = new MissionPlanner.Controls.MyButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();

            // numericUpDown1
            this.numericUpDown1.Location = new System.Drawing.Point(150, 20);
            this.numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numericUpDown1.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.Value = new decimal(new int[] { 317, 0, 0, 0 });

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "AUX Function Number:";

            // comboBox1
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
                "Low (0)",
                "Middle (1)",
                "High (2)"
            });
            this.comboBox1.Location = new System.Drawing.Point(150, 50);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(120, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndex = 2; // Default High

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Switch Position:";

            // BUT_save
            this.BUT_save.Location = new System.Drawing.Point(150, 90);
            this.BUT_save.Name = "BUT_save";
            this.BUT_save.Size = new System.Drawing.Size(75, 23);
            this.BUT_save.TabIndex = 4;
            this.BUT_save.Text = "Save";
            this.BUT_save.UseVisualStyleBackColor = true;
            this.BUT_save.Click += new System.EventHandler(this.BUT_save_Click);

            // Joy_Do_Aux_Function
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 130);
            this.Controls.Add(this.BUT_save);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Joy_Do_Aux_Function";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AUX Function Settings";

            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BUT_save_Click(object sender, EventArgs e)
        {
            if (MainV2.joystick != null && MainV2.joystick.enabled)
            {
                var config = MainV2.joystick.getButton(int.Parse(Tag.ToString()));
                config.p1 = (float)numericUpDown1.Value;      // AUX function number
                config.p2 = (float)comboBox1.SelectedIndex;   // Switch position (0, 1, 2)
                MainV2.joystick.setButton(int.Parse(Tag.ToString()), config);
            }
            this.Close();
        }
    }
}
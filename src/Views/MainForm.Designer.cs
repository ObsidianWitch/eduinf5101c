namespace ImageSynthesis.Views {
    partial class MainForm {
        
        private System.Windows.Forms.Button RenderButton;
        private System.Windows.Forms.CheckBox SlowModeCheckbox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.IContainer components = null;
        
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.RenderButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SlowModeCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // RenderButton
            //
            this.RenderButton.Location = new System.Drawing.Point(12, 12);
            this.RenderButton.Name = "RenderButton";
            this.RenderButton.Size = new System.Drawing.Size(75, 23);
            this.RenderButton.TabIndex = 0;
            this.RenderButton.Text = "Render";
            this.RenderButton.UseVisualStyleBackColor = true;
            this.RenderButton.Click += new System.EventHandler(this.RenderButtonClick);
            //
            // pictureBox1
            //
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 38);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 500);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            //
            // SlowModeCheckbox
            //
            this.SlowModeCheckbox.AutoSize = true;
            this.SlowModeCheckbox.Checked = true;
            this.SlowModeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SlowModeCheckbox.Location = new System.Drawing.Point(93, 16);
            this.SlowModeCheckbox.Name = "SlowModeCheckbox";
            this.SlowModeCheckbox.Size = new System.Drawing.Size(78, 17);
            this.SlowModeCheckbox.TabIndex = 4;
            this.SlowModeCheckbox.Text = "Slow mode";
            this.SlowModeCheckbox.UseVisualStyleBackColor = true;
            this.SlowModeCheckbox.CheckedChanged += new System.EventHandler(this.SlowModeToggle);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(821, 547);
            this.Controls.Add(this.SlowModeCheckbox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.RenderButton);
            this.Name = "MainForm";
            this.Text = "INF-5101C - Projet";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

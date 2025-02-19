﻿namespace ImageSynthesis.Views {
    partial class MainForm {
        
        private System.Windows.Forms.Button RenderButton;
        private System.Windows.Forms.CheckBox SlowModeCheckbox;
        private System.Windows.Forms.ComboBox RendererComboBox;
        private System.Windows.Forms.ComboBox SceneComboBox;
        private Canvas Canvas;
        private System.ComponentModel.IContainer components = null;
        
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.RenderButton = new System.Windows.Forms.Button();
            this.SlowModeCheckbox = new System.Windows.Forms.CheckBox();
            this.RendererComboBox = new System.Windows.Forms.ComboBox();
            this.SceneComboBox = new System.Windows.Forms.ComboBox();
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
            // RendererComboBox
            // 
            this.RendererComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RendererComboBox.FormattingEnabled = true;
            this.RendererComboBox.Location = new System.Drawing.Point(177, 12);
            this.RendererComboBox.Name = "RendererComboBox";
            this.RendererComboBox.Size = new System.Drawing.Size(121, 21);
            this.RendererComboBox.TabIndex = 5;
            this.RendererComboBox.SelectedIndexChanged += new System.EventHandler(this.RendererChanged);
            // 
            // SceneComboBox
            // 
            this.SceneComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SceneComboBox.FormattingEnabled = true;
            this.SceneComboBox.Location = new System.Drawing.Point(304, 12);
            this.SceneComboBox.Name = "SceneComboBox";
            this.SceneComboBox.Size = new System.Drawing.Size(121, 21);
            this.SceneComboBox.TabIndex = 6;
            this.SceneComboBox.SelectedIndexChanged += new System.EventHandler(this.SceneChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(821, 547);
            this.Controls.Add(this.SceneComboBox);
            this.Controls.Add(this.RendererComboBox);
            this.Controls.Add(this.SlowModeCheckbox);
            this.Controls.Add(this.RenderButton);
            this.Name = "MainForm";
            this.Text = "INF-5101C - Projet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        private void InitializeCanvas() {
            Canvas = new Canvas(800, 500);
            SuspendLayout();
            Canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            Canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Canvas.Location = new System.Drawing.Point(12, 38);
            Canvas.Name = "Canvas";
            Canvas.TabStop = false;
            
            Controls.Add(Canvas);
            ResumeLayout(false);
            PerformLayout();
        }

    }
}
